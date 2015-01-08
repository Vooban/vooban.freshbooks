using System;
using System.Collections.Generic;
using System.Linq;

namespace Vooban.FreshBooks.Reports.Timesheet
{
    public class TimesheetReportTaskInformation
    {
        public TimesheetReportTaskInformation()
        {
            SicknessTaskIds = new List<int>();
            VacationsTaskIds = new List<int>();
            HollidayTaskIds = new List<int>();
            BankedTimeTaskIds = new List<int>();
            TrainingTaskIds = new List<int>();
            UnpaidTimeOffTaskIds = new List<int>();
        }

        public IEnumerable<int> AllTaskIds { get; set; }

        public IList<int> SicknessTaskIds { get; set; }

        public IList<int> VacationsTaskIds { get; set; }

        public IList<int> HollidayTaskIds { get; set; }

        public IList<int> TrainingTaskIds { get; set; }

        public IList<int> BankedTimeTaskIds { get; set; }

        public IEnumerable<int> UnpaidTimeOffTaskIds{ get; set; }
    }

}
