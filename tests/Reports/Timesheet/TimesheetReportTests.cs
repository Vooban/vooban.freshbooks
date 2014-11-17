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
    public class TimesheetReportTests
    {
        private static readonly string _username = Environment.GetEnvironmentVariable("FreshbooksUsername");
        private static readonly string _token = Environment.GetEnvironmentVariable("FreshbooksToken");

        [Fact]
        public void Test_timesheet_extraction_works_properly()
        {
            var taskInformation = new TimesheetReportTaskInformation
            {
                HollidayTaskIds = new List<int> { 5824 },
                VacationsTaskIds = new List<int> { 5825 },
                SicknessTaskIds = new List<int> { 5826 },
                UnpaidAbsenceTaskIds = new List<int> { 5827 },
            };

            var testee = new TimesheetReport(_username, _token, taskInformation);

            var result = testee.Execute(new DateTime(2014, 10, 6), new DateTime(2014, 10, 19));

            Debugger.Break();
        }
    }
}
