using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;
using Vooban.FreshBooks.DotNet.Api.Staff.Models;

namespace Vooban.FreshBooks.DotNet.Api.Staff
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class StaffApi : BaseApi<StaffModel>
    {
        #region Constantes

        public const string COMMAND_STAFF_LIST = "staff.list";
        public const string COMMAND_STAFF_CURRENT = "staff.current";
        public const string COMMAND_STAFF_GET = "staff.get";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        public StaffApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
            :base(freshbooks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        public StaffApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
            : base(freshbooks)
        {
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Call the <c>staff.current</c> method on the Freshbooks API.
        /// </summary>
        /// <returns>The <see cref="StaffModel"/> information of the client used to communicate with Freshbooks</returns>
        public FreshbooksGetResponse<StaffModel> CallGetCurrent()
        {
            return CallGetMethod(COMMAND_STAFF_CURRENT, p => CreateStaffFromFreshbooksData(p.response.staff));           
        }

        /// <summary>
        /// Call the <c>staff.get</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="staffId">The staff id that you want to get information for.</param>
        /// <returns>
        /// The <see cref="StaffModel" /> information for the specified <paramref name="staffId" />
        /// </returns>
        public FreshbooksGetResponse<StaffModel> CallGet(string staffId)
        {
            return CallGetMethod(COMMAND_STAFF_GET, p => p.staff_id = staffId, r => CreateStaffFromFreshbooksData(r.response.staff));           
        }

        /// <summary>
        /// Call the <c>staff.list</c> method on the Freshbooks API.
        /// </summary>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{StaffModel}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage
        /// </exception>
        public FreshbooksPagedResponse<StaffModel> CallGetList(int page = 1, int itemPerPage = 100)
        {
            return CallGetListMethod(COMMAND_STAFF_LIST, r => BuildEnumerableFromDynamicResult(r), null, page, itemPerPage);          
        }

        /// <summary>
        /// Get all the staff member available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the <c>staff.list</c> method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<FreshbooksPagedResponse<StaffModel>> CallGetAllPages()
        {
            return CallGetAllPagesMethod(COMMAND_STAFF_LIST, r => BuildEnumerableFromDynamicResult(r));            
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Build an <see cref="IEnumerable{T}"/> from the dynamic Freshbooks response.
        /// </summary>
        /// <param name="resultStaff">The Freshbooks response as a dynamic object</param>
        /// <returns>An <see cref="IEnumerable{StaffModel}"/> based on the Freshbooks response.</returns>
        private static IEnumerable<StaffModel> BuildEnumerableFromDynamicResult(dynamic resultStaff)
        {
            foreach (var staffMember in resultStaff.response.staff_members.member)
                yield return CreateStaffFromFreshbooksData(staffMember);
        }

        /// <summary>
        /// Map the Freshbooks dynamic object to the <see cref="StaffModel"/> class.
        /// </summary>
        /// <param name="staffMember">The dynamic object loaded from the Freshbooks response</param>
        /// <returns>The <see cref="StaffModel"/> instance correctly filled with the Freshbooks response</returns>
        private static StaffModel CreateStaffFromFreshbooksData(dynamic staffMember)
        {
            return new StaffModel
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
                HomeAddress = CreateAddressFromDynamicObject(staffMember)
            };
        }

        /// <summary>
        /// Creates an address object from a Freshbooks dynamic response
        /// </summary>
        /// <param name="value">Le object from which we extract the address</param>
        /// <returns>The correctly formatted address</returns>
        private static AddressModel CreateAddressFromDynamicObject(dynamic value)
        {
            return new AddressModel
            {
                Street1 = value.street1,
                Street2 = value.street2,
                City = value.city,
                State = value.state,
                Country = value.country,
                Code = value.code
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