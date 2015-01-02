using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
                if (currentStaff != null && currentStaff.Id.HasValue)
                {
                    var currentStaffTimeEntries = timeEntriesByStaffMember[currentStaff.Id.Value].ToList();

                    var currentStaffTimeSheetDetails = new RangedTimeSheetDetail {
                        Employee = currentStaff,
                        AllTimeEntries=currentStaffTimeEntries,                       
                        HollidayTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.HollidayTaskIds.Contains(w.TaskId)),
                        SicknessTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.SicknessTaskIds.Contains(w.TaskId)),
                        VacationsTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.VacationsTaskIds.Contains(w.TaskId)),
                        TrainingTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.TrainingTaskIds.Contains(w.TaskId)),
                        BankedTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.BankedTimeTaskIds.Contains(w.TaskId)),
                        PayableTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.PayableLabourTaskId.Contains(w.TaskId)),
                        PaidTimeOffTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.PaidTimeOffTaskIds.Contains(w.TaskId)),
                        UnpaidTimeOffTimeEntries = currentStaffTimeEntries.Where(w => _taskInformations.UnpaidTimeOffTaskIds.Contains(w.TaskId)),
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
