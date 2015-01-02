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
            UnpaidAbsenceTaskIds = new List<int>();
            BankedTimeTaskIds = new List<int>();
            TrainingTaskIds = new List<int>();
        }

        public IEnumerable<int> AllTaskIds { get; set; }

        public IList<int> SicknessTaskIds { get; set; }

        public IList<int> VacationsTaskIds { get; set; }

        public IList<int> HollidayTaskIds { get; set; }

        public IList<int> UnpaidAbsenceTaskIds { get; set; }

        public IList<int> BankedTimeTaskIds { get; set; }

        public IList<int> TrainingTaskIds { get; set; }

        public IEnumerable<int> PaidTimeOffTaskIds
        {
            get
            {
                return SicknessTaskIds.Concat(VacationsTaskIds).Concat(HollidayTaskIds).Concat(UnpaidAbsenceTaskIds).Concat(BankedTimeTaskIds).Concat(TrainingTaskIds);
            }
        }

        public IEnumerable<int> UnpaidTimeOffTaskIds
        {
            get
            {
                return UnpaidAbsenceTaskIds;
            }
        }

        public IEnumerable<int> PayableLabourTaskId
        {
            get
            {
                if (AllTaskIds != null)
                    return AllTaskIds.Where(t => !UnpaidTimeOffTaskIds.Contains(t));
                else
                    throw new InvalidOperationException("You cannot ask for the whole list of eligible time to overtime without having provided the entire list of task in your freshbooks system first");
            }
        }
    }

}
