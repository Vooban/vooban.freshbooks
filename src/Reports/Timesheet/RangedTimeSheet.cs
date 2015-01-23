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

        public IEnumerable<TimeEntryModel> AllEmployeesUnpaidTimeOffTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.UnpaidTimeOffTimeEntries);
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

        public IEnumerable<TimeEntryModel> AllEmployeesHolidayTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.HolidayTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesBankedTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.BankedTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesTrainingTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.TrainingTimeEntries);
            }
        }

        public IEnumerable<TimeEntryModel> AllEmployeesBillableTimeEntries
        {
            get
            {
                return EmployeeTimeSheets.SelectMany(s => s.BillableTimeEntries);
            }
        }

        public double TotalTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalTime);
            }
        }

        public double TotalUnpaidTimeOffTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalUnpaidTimeOffTime);
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

        public double TotalHolidayTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalHolidayTime);
            }
        }

        public double TotalBankedTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalBankedTime);
            }
        }

        public double TotalTrainingTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalTrainingTime);
            }
        }

        public double TotalBillableTime
        {
            get
            {
                return EmployeeTimeSheets.Sum(s => s.TotalBillableTime);
            }
        }
    }
    
}
