using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vooban.FreshBooks.Staff.Models;
using Vooban.FreshBooks.TimeEntry.Models;

namespace Vooban.FreshBooks.Reports.Timesheet
{
    [DebuggerDisplay("Timesheet for {Employee.FirstName} {Employee.Lastname}")]
    public class RangedTimeSheetDetail
    {
        public DateTime From { get; internal set; }

        public DateTime To { get; internal set; }

        public StaffModel Employee { get; internal set; }

        [DebuggerDisplay("All time entries for a total of {TotalTime} hours")]
        public IEnumerable<TimeEntryModel> AllTimeEntries { get; internal set; }

        [DebuggerDisplay("All the unworked and unpaid time for a total of {TotalIneligibleToOvertimeTime} hours")]
        public IEnumerable<TimeEntryModel> UnpaidTimeOffTimeEntries { get; internal set; }

        [DebuggerDisplay("Sickness time entries for a total of {TotalSicknessTime} hours")]
        public IEnumerable<TimeEntryModel> SicknessTimeEntries { get; internal set; }

        [DebuggerDisplay("Vacations time entries for a total of {TotalVacationsTime} hours")]
        public IEnumerable<TimeEntryModel> VacationsTimeEntries { get; internal set; }

        [DebuggerDisplay("Holiday time entries for a total of {TotalHolidayTime} hours")]
        public IEnumerable<TimeEntryModel> HolidayTimeEntries { get; internal set; }

        [DebuggerDisplay("Banked absence time entries for a total of {TotalBankedTime} hours")]
        public IEnumerable<TimeEntryModel> BankedTimeEntries { get; internal set; }

        [DebuggerDisplay("Banked absence time entries for a total of {TotalBankedTime} hours")]
        public IEnumerable<TimeEntryModel> TrainingTimeEntries { get; internal set; }

        [DebuggerDisplay("Billable time entries for a total of {TotalBillableTime} hours")]
        public IEnumerable<TimeEntryModel> BillableTimeEntries { get; set; }

        [DebuggerDisplay("Unbillable time entries for a total of {TotalUnbillableTime} hours")]
        public IEnumerable<TimeEntryModel> UnbillableTimeEntries { get; set; }

        [DebuggerDisplay("Unbillable time entries for a total of {TotalUnbillableWorkTime} hours")]
        public IEnumerable<TimeEntryModel> UnbillableWorkTimeEntries { get; set; }

        public double TotalTime
        {
            get
            {                
                return AllTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalUnpaidTimeOffTime
        {
            get
            {
                return UnpaidTimeOffTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalSicknessTime
        {
            get
            {
                return SicknessTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalVacationsTime
        {
            get
            {
                return VacationsTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalHolidayTime
        {
            get
            {
                return HolidayTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalBankedTime
        {
            get
            {
                return BankedTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalTrainingTime
        {
            get
            {
                return TrainingTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalBillableTime
        {
            get
            {
                return BillableTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalUnbillableTime
        {
            get
            {
                return UnbillableTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalUnbillableWorkTime
        {
            get
            {
                return UnbillableWorkTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }   
    }

}
