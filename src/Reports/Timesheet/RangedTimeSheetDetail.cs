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
        public StaffModel Employee { get; internal set; }

        [DebuggerDisplay("All time entries for a total of {TotalTime} hours")]
        public IEnumerable<TimeEntryModel> AllTimeEntries { get; internal set; }

        [DebuggerDisplay("Ineligible to overtime time entries for a total of {TotalIneligibleToOvertimeTime} hours")]
        public IEnumerable<TimeEntryModel> IneligibleToOvertimeTimeEntries { get; internal set; }

        [DebuggerDisplay("Payable labour time entries for a total of {TotalPayableLabourTime} hours")]
        public IEnumerable<TimeEntryModel> PayableLabourTimeEntries { get; internal set; }

        [DebuggerDisplay("Sickness time entries for a total of {TotalSicknessTime} hours")]
        public IEnumerable<TimeEntryModel> SicknessTimeEntries { get; internal set; }

        [DebuggerDisplay("Vacations time entries for a total of {TotalVacationsTime} hours")]
        public IEnumerable<TimeEntryModel> VacationsTimeEntries { get; internal set; }

        [DebuggerDisplay("Holliday time entries for a total of {TotalHollidayTime} hours")]
        public IEnumerable<TimeEntryModel> HollidayTimeEntries { get; internal set; }

        [DebuggerDisplay("Unpaid absence time entries for a total of {TotalUnpaidAbsenceTime} hours")]
        public IEnumerable<TimeEntryModel> UnpaidAbsenceTimeEntries { get; internal set; }

        public double TotalTime
        {
            get
            {                
                return AllTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalIneligibleToOvertimeTime
        {
            get
            {
                return IneligibleToOvertimeTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalPayableLabourTime
        {
            get
            {
                return PayableLabourTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
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

        public double TotalHollidayTime
        {
            get
            {
                return HollidayTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }

        public double TotalUnpaidAbsenceTime
        {
            get
            {
                return UnpaidAbsenceTimeEntries.Where(s => s.Hours.HasValue).Sum(s => s.Hours.Value);
            }
        }
    }

}
