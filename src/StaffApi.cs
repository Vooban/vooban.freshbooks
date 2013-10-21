using System;
using System.Collections.Generic;
using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api
{
    public class StaffApi
    {
        private const string COMMAND_STAFF_LIST = "staff.list";

        private readonly Lazy<HastyAPI.FreshBooks.FreshBooks> _freshbooks;

        public StaffApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
        {
            _freshbooks = freshbooks;
        }

        public Staff GetCurrent()
        {
            return null;
        }

        public Staff Get(string staffId)
        {
            return null;
        }

        public FreshbooksPagedResponse<Staff> GetMultiple(int page = 1, int itemPerPage = 100)
        {
            var resultStaff = _freshbooks.Value.Call(COMMAND_STAFF_LIST, p =>{
                p.page = page;
                p.per_page = itemPerPage;
            });

            FreshbooksPagedResponse<Staff> response = FreshbooksConvert.ToPagedResponse<Staff>(resultStaff);

            response.Result = BuildEnumerableStaff(resultStaff);

            return response;
        }

        private static IEnumerable<Staff> BuildEnumerableStaff(dynamic resultStaff)
        {
            foreach (var staffMember in resultStaff.response.staff_members.member)
                yield return CreateStaffFromFreshbooksData(staffMember);
        }

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