using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vooban.FreshBooks.TimeEntry.Models;

namespace Vooban.FreshBooks.Reports.Timesheet
{
    public class RangedTimeSheet
    {
        public RangedTimeSheet(DateTime from, DateTime to, IEnumerable<RangedTimeSheetDetail> employeeTimeSheets)
        {
            From = from;
            To = to;
            EmployeeTimeSheets = employeeTimeSheets;
        }

        public DateTime From { get; private set; }

        public DateTime To { get; private set; }

        public IEnumerable<RangedTimeSheetDetail> EmployeeTimeSheets { get; set; }

        public IEnumerable<TimeEntryModel> AllEmployeesAllTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.AllTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesIneligibleToOvertimeTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.IneligibleToOvertimeTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesPayableLabourTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.PayableLabourTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesSicknessTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.SicknessTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesVacationsTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.VacationsTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesHollidayTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.HollidayTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesUnpaidAbsenceTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.UnpaidAbsenceTimeEntries);
            }
        }

        public double TotalTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalTime);
            }
        }

        public double TotalIneligibleToOvertimeTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalIneligibleToOvertimeTime);
            }
        }

        public double TotalEligibleToOvertimeTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalPayableLabourTime);
            }
        }

        public double TotalSicknessTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalSicknessTime);
            }
        }

        public double TotalVacationsTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalVacationsTime);
            }
        }

        public double TotalHollidayTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalHollidayTime);
            }
        }

        public double TotalUnpaidAbsenceTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalUnpaidAbsenceTime);
            }
        }
    }
    
}
