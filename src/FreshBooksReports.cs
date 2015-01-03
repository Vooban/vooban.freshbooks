using System;
using Vooban.FreshBooks.Reports.Timesheet;

namespace Vooban.FreshBooks
{
    public class FreshBooksReports
    {
        private readonly Lazy<TimesheetReport> _timesheetReport;

        private FreshBooksReports(string username, string token, TimesheetReportTaskInformation taskInformations)
        {
            _timesheetReport = new Lazy<TimesheetReport>(() => new TimesheetReport(username, token, taskInformations));
        }

        public TimesheetReport Timesheet
        {
            get { return _timesheetReport.Value; }
        }
    }
}