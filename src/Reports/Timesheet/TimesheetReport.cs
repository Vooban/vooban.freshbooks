using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vooban.FreshBooks.Project;
using Vooban.FreshBooks.Project.Models;
using Vooban.FreshBooks.Staff;
using Vooban.FreshBooks.Staff.Models;
using Vooban.FreshBooks.Task;
using Vooban.FreshBooks.Task.Models;
using Vooban.FreshBooks.TimeEntry;
using Vooban.FreshBooks.TimeEntry.Models;

namespace Vooban.FreshBooks.Reports.Timesheet
{
    public class TimesheetReport : FreshbooksReportBase
    {
        private readonly TimesheetReportTaskInformation _taskInformations;

        public TimesheetReport(string username, string token, TimesheetReportTaskInformation taskInformations) :
            base(username, token)
        {
            _taskInformations = taskInformations;

            if (_taskInformations.AllTaskIds == null)
                _taskInformations.AllTaskIds = Tasks.Select(s => s.Id.Value);
        }

        public RangedTimeSheet Execute(DateTime from, DateTime to)
        {
             var timesheetDetails = new List<RangedTimeSheetDetail>();
             var result = TimeEntryService.SearchAll(new TimeEntryFilter { DateFrom = from, DateTo = to });

            var timeEntriesByStaffMember = result.GroupBy(g => g.StaffId).OrderBy(o => o.Key).ToDictionary(k => k.Key, v => v.Select(s => s));
           
            foreach (var staffId in timeEntriesByStaffMember.Keys)
            {
                var currentStaff = StaffMembers.SingleOrDefault(s => s.Id == staffId);
                if (currentStaff != null)
                {
                    var currentStaffTimeEntries = timeEntriesByStaffMember[currentStaff.Id.Value];

                    var currentStaffTimeSheetDetails = new RangedTimeSheetDetail() {
                        Employee = currentStaff,
                        AllTimeEntries=currentStaffTimeEntries,
                        PayableLabourTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.PayableLabourTaskId.Contains(w.TaskId)),
                        IneligibleToOvertimeTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.IneligibleToOvertimeTaskId.Contains(w.TaskId)),
                        HollidayTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.HollidayTaskIds.Contains(w.TaskId)),
                        SicknessTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.SicknessTaskIds.Contains(w.TaskId)),
                        UnpaidAbsenceTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.UnpaidAbsenceTaskIds.Contains(w.TaskId)),
                        VacationsTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.VacationsTaskIds.Contains(w.TaskId)),
                    };
                    
                    timesheetDetails.Add(currentStaffTimeSheetDetails);
                }
                else
                {
                    // Find a way to manage contractors
                    Debug.WriteLine("Unrecognized staff id '{0}'. It is probably a contractor, used to contractor API to get his information if needed", staffId);
                }
            }

            return new RangedTimeSheet(from, to, timesheetDetails);
        }       
    }
}
