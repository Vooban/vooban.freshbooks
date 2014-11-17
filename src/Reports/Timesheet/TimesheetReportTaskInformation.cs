using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        }

        public IEnumerable<int> AllTaskIds { get; set; }

        public IList<int> SicknessTaskIds { get; set; }

        public IList<int> VacationsTaskIds { get; set; }

        public IList<int> HollidayTaskIds { get; set; }

        public IList<int> UnpaidAbsenceTaskIds { get; set; }

        public IEnumerable<int> IneligibleToOvertimeTaskId
        {
            get
            {
                return SicknessTaskIds.Concat(VacationsTaskIds).Concat(HollidayTaskIds).Concat(UnpaidAbsenceTaskIds);
            }
        }

        public IEnumerable<int> PayableLabourTaskId
        {
            get
            {
                if (AllTaskIds != null)
                    return AllTaskIds.Where(t => !IneligibleToOvertimeTaskId.Contains(t));
                else
                    throw new InvalidOperationException("You cannot ask for the whole list of eligible time to overtime without having provided the entire list of task in your freshbooks system first");
            }
        }
    }

}
