using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class StaffApi
    {
        #region Constantes

        public const string COMMAND_STAFF_LIST = "staff.list";
        public const string COMMAND_STAFF_CURRENT = "staff.current";
        public const string COMMAND_STAFF_GET = "staff.get";

        #endregion

        #region Private members

        /// <summary>
        /// This holds the Freshbooks clients that will be used to communicate with Freshbooks
        /// </summary>
        private readonly Lazy<HastyAPI.FreshBooks.FreshBooks> _freshbooks;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        public StaffApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
        {
            _freshbooks = freshbooks;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        public StaffApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
        {
            _freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(()=>freshbooks);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Call the <c>staff.current</c> method on the Freshbooks API.
        /// </summary>
        /// <returns>The <see cref="Staff"/> information of the client used to communicate with Freshbooks</returns>
        public FreshbooksResponse<Staff> CallGetCurrent()
        {
            var currentStaffMember = _freshbooks.Value.Call(COMMAND_STAFF_CURRENT);

            var response = FreshbooksConvert.ToResponse<Staff>(currentStaffMember);

            return (FreshbooksResponse<Staff>)response.WithResult(CreateStaffFromFreshbooksData(currentStaffMember));
        }

        /// <summary>
        /// Call the <c>staff.get</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="staffId">The staff id that you want to get information for.</param>
        /// <returns>
        /// The <see cref="Staff" /> information for the specified <paramref name="staffId" />
        /// </returns>
        public FreshbooksResponse<Staff> CallGet(string staffId)
        {
            var currentStaffMember = _freshbooks.Value.Call(COMMAND_STAFF_GET, p => p.staff_id = staffId);

            var response = FreshbooksConvert.ToResponse<Staff>(currentStaffMember);

            return (FreshbooksResponse<Staff>)response.WithResult(CreateStaffFromFreshbooksData(currentStaffMember));
        }

        /// <summary>
        /// Call the <c>staff.list</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{Staff}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage
        /// </exception>
        public FreshbooksPagedResponse<Staff> CallGetList(int page = 1, int itemPerPage = 100)
        {
            if (itemPerPage < 1)
                throw new ArgumentException("Please ask for at least 1 item per page otherwise this call is irrelevant.", "itemPerPage");

            if (itemPerPage > 100)
                throw new ArgumentException("The max number of items per page supported by Freshbooks is 100.", "itemPerPage");

            var resultStaff = _freshbooks.Value.Call(COMMAND_STAFF_LIST, p =>{
                p.page = page;
                p.per_page = itemPerPage;
            });

            var response = (FreshbooksPagedResponse<Staff>) FreshbooksConvert.ToPagedResponse<Staff>(resultStaff);

            return response.WithResult((IEnumerable<Staff>)BuildEnumerableFromDynamicResult(resultStaff));
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <c>staff.list</c> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<FreshbooksPagedResponse<Staff>> CallGetAllPages()
        {
            var result = new List<FreshbooksPagedResponse<Staff>>();
            var response = CallGetList(1, 100);
            if (response.Status)
            {
                // Add items obtained from the first page
                result.Add(response);

                // Add items for remaining pages                
                for (int i = 2; i <= response.TotalPages; i++)
                {
                    response = CallGetList(i, 100);
                    result.Add(response);
                }
            }
            else
                throw new InvalidProgramException(string.Format("Freshbooks API failed with status code : {0}", response.Status));

            return result;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Build an <see cref="IEnumerable{T}"/> from the dynamic Freshbooks response.
        /// </summary>
        /// <param name="resultStaff">The Freshbooks response as a dynamic object</param>
        /// <returns>An <see cref="IEnumerable{Staff}"/> based on the Freshbooks response.</returns>
        private static IEnumerable<Staff> BuildEnumerableFromDynamicResult(dynamic resultStaff)
        {
            foreach (var staffMember in resultStaff.response.staff_members.member)
                yield return CreateStaffFromFreshbooksData(staffMember);
        }

        /// <summary>
        /// Map the Freshbooks dynamic object to the <see cref="Staff"/> class.
        /// </summary>
        /// <param name="staffMember">The dynamic object loaded from the Freshbooks response</param>
        /// <returns>The <see cref="Staff"/> instance correctly filled with the Freshbooks response</returns>
        private static Staff CreateStaffFromFreshbooksData(dynamic staffMember)
        {
            return new Staff
            {
                Id = staffMember.staff_id,
                Username = staffMember.username,
                FirstName = staffMember.first_name,
                Lastname = staffMember.last_name,                    
                Email = staffMember.email,
                BusinessPhone = staffMember.business_phone,
                MobilePhone = staffMember.mobile_phone,
                Rate = FreshbooksConvert.ToDouble(staffMember.rate),
                LastLogin = FreshbooksConvert.ToDateTime(staffMember.last_login),
                NumberOfLogins = FreshbooksConvert.ToInt32(staffMember.number_of_logins),
                SignupDate = FreshbooksConvert.ToDateTime(staffMember.signup_date),
                HomeAddress = new Address
                {
                    Street1 = staffMember.street1,
                    Street2 = staffMember.street2,
                    City = staffMember.city,
                    State = staffMember.state,
                    Country = staffMember.country,
                    Code = staffMember.code
                }
            };
        }

        #endregion
    }
}

//class Program
//{
//    static void Main(string[] args)
//    {
//        var fb = new FreshBooks("", "");
//        var resultStaff = fb.Call("staff.list");

//        var staffList = new Dictionary<string, string>();
//        foreach (var staff in resultStaff.response.staff_members.member)
//        {
//            staffList.Add(staff.staff_id, staff.email);
//        }

//        var resultProjects = fb.Call("project.list");
//        var projectList = new Dictionary<string, string>();
//        foreach (var project in resultProjects.response.projects.project)
//        {
//            projectList.Add(project.project_id, project.name);
//        }

//        var result = fb.Call("invoice.list");

//        var invoices = new Dictionary<string, List<TimeEntry>>();
//        foreach (var invoice in result.response.invoices.invoice)
//        {
//            var builder = new StringBuilder();

//            builder.AppendFormat("Invoice #{0} - {1}", invoice.invoice_id, invoice.status);

//            if (invoice.lines != null)
//            {
//                foreach (var line in invoice.lines.line)
//                {
//                    builder.AppendLine(string.Format("{0} - {1}$ [{2}]", line.name, line.amount, line.type));

//                    var timeEntries = new List<TimeEntry>();
//                    if (line.time_entries != null)
//                    {
//                        foreach (var time in line.time_entries.time_entry)
//                        {
//                            var timeEntryId = time.time_entry_id;
//                            var timeEntryResult = fb.Call("time_entry.get", x => x.time_entry_id = timeEntryId).response.time_entry;

//                            timeEntries.Add(new TimeEntry
//                            {
//                                Billed = timeEntryResult.billed == "1",
//                                Date = Convert.ToDateTime(timeEntryResult.date),
//                                Hours = Convert.ToDecimal(timeEntryResult.hours, CultureInfo.InvariantCulture),
//                                Email = staffList[timeEntryResult.staff_id],
//                                Project = projectList[timeEntryResult.project_id]
//                            });
//                        }

//                        if (invoices.ContainsKey(invoice.invoice_id))
//                        {
//                            invoices[invoice.invoice_id].AddRange(timeEntries);
//                        }
//                        else
//                        {
//                            invoices.Add(invoice.invoice_id, timeEntries);
//                        }
//                    }
//                }
//            }

//            Console.WriteLine(builder.ToString());
//        }
//    }
//}

//public class TimeEntry
//{
//    public string Email { get; set; }

//    public decimal Hours { get; set; }

//    public DateTime Date { get; set; }

//    public bool Billed { get; set; }

//    public string Project { get; set; }

//    public string Notes { get; set; }
//}