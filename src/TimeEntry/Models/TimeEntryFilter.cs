using System;
using HastyAPI;
using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api.TimeEntry.Models
{
    public class TimeEntryFilter : FreshbooksFilter
    {
        #region Properties

        /// <summary>
        /// Gets the projects's unique identifier to which this time entry is associated
        /// </summary>
        public string ProjectId { get; internal set; }

        /// <summary>
        /// Gets the task's unique identifier to which this time entry is associated
        /// </summary>
        public string TaskId { get; internal set; }

        /// <summary>
        /// Gets the date at which this time entry was create
        /// </summary>
        public DateTime? DateFrom { get; internal set; }

        /// <summary>
        /// Gets the date at which this time entry was create
        /// </summary>
        public DateTime? DateTo { get; internal set; }

        #endregion

        #region Instance Method

        /// <summary>
        /// Converts a strongly-typed .NET object into the corresponding dynamic Freshbooks object
        /// </summary>
        /// <returns>The dynamic object to be sent to Freshbooks</returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new FriendlyDynamic();

            if (!string.IsNullOrEmpty(ProjectId)) result.project_id = ProjectId;
            if (!string.IsNullOrEmpty(TaskId)) result.task_id = TaskId;
            if (DateFrom.HasValue) result.date = FreshbooksConvert.FromDateTime(DateFrom);
            if (DateTo.HasValue) result.date = FreshbooksConvert.FromDateTime(DateTo);

            return result;
        }

        #endregion
    }
}