using HastyAPI;
using FreshBooks.Api.Models;

namespace FreshBooks.Api.Task.Models
{
    public class TaskFilter : FreshbooksFilter
    {
        #region Properties

        /// <summary>
        /// Gets the projects's unique identifier to which this time entry is associated
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets the task's unique identifier to which this time entry is associated
        /// </summary>
        public string TaskId { get; set; }

        #endregion

        #region Instance Method

        /// <summary>
        /// Converts a strongly-typed .NET object into the corresponding dynamic Freshbooks object
        /// </summary>
        /// <returns>The dynamic object to be sent to Freshbooks</returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new FriendlyDynamic();

            if (!string.IsNullOrEmpty(ClientId)) result.client_id = ClientId;
            if (!string.IsNullOrEmpty(TaskId)) result.task_id = TaskId;

            return result;
        }

        #endregion
    }
}