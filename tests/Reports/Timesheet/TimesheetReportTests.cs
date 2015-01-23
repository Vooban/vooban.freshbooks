using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vooban.FreshBooks.Reports.Timesheet;
using Xunit;

namespace Vooban.PayEngine.Logic.Tests
{
    public static class TaskIds
    {
        public const int BANKED = 8976;
        public const int HOLIDAY = 5824;
        public const int TRAINING = 9088;
        public const int SICKNESS = 5826;
        public const int UNPAID = 5827;
        public const int VACATIONS = 5825;
    }

    public class TimesheetReportTests
    {
        private static readonly string _username = Environment.GetEnvironmentVariable("FreshbooksUsername");
        private static readonly string _token = Environment.GetEnvironmentVariable("FreshbooksToken");

        [Fact]
        public void Test_timesheet_extraction_works_properly()
        {
            var taskInformation = new TimesheetReportTaskInformation
            {
                HolidayTaskIds = new List<int> { TaskIds.HOLIDAY },
                VacationsTaskIds = new List<int> { TaskIds.VACATIONS },
                SicknessTaskIds = new List<int> { TaskIds.SICKNESS },
                UnpaidTimeOffTaskIds = new List<int> { TaskIds.UNPAID },
                BankedTimeTaskIds = new List<int> { TaskIds.BANKED },
                TrainingTaskIds = new List<int> { TaskIds.TRAINING },
            };

            var testee = new TimesheetReport(_username, _token, taskInformation);

            var result = testee.Execute(new DateTime(2014, 08, 1), new DateTime(2014, 12, 31));

            foreach (var line in result.EmployeeTimeSheets)
            {
                Debug.WriteLine("{0} {1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}", line.Employee.FirstName, line.Employee.Lastname, line.TotalTime, line.TotalUnpaidTimeOffTime, line.TotalVacationsTime, line.TotalUnpaidTimeOffTime, line.TotalHolidayTime, line.TotalBankedTime, line.TotalSicknessTime, line.TotalTrainingTime);
            }
        }
    }
}
