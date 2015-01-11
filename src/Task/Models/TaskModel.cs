using System;
using System.Diagnostics;
using System.Dynamic;
using Vooban.FreshBooks.Models;

namespace Vooban.FreshBooks.Task.Models
{
    /// <summary>
    /// Represents a Freshbooks TimeEntry (time_entry)
    /// </summary>
    /// <remarks>
    ///  <task>  
    ///   <task_id>211</task_id>  
    ///   <name>Research</name>  
    ///   <description></description>  
    ///   <billable>1</billable>  
    ///   <rate>180</rate>  
    ///  </task>
    /// </remarks>
    [DebuggerDisplay("{Id}-{Name}")]
    public class TaskModel : FreshbooksModel
    {
        #region Properties

        /// <summary>
        /// Gets the number of hours worked on this project-task combination for this user
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the description attached to the project
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not this task is billable
        /// </summary>
        public bool? Billable { get; set; }

        /// <summary>
        /// Gets the rate at which people will bill on this project
        /// </summary>
        public double? Rate { get; set; }

        #endregion

        #region Instance Method

        /// <summary>
        /// Converts a strongly-typed .NET object into the corresponding dynamic Freshbooks object
        /// </summary>
        /// <returns>The dynamic object to be sent to Freshbooks</returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new ExpandoObject();

            result.task_id = Id;
            if (!string.IsNullOrEmpty(Name)) result.name = Name;
            if (!string.IsNullOrEmpty(Description)) result.description = Description;

            if (Rate.HasValue) result.rate = FreshbooksConvert.FromDouble(Rate);
            if (Billable.HasValue) result.billable = FreshbooksConvert.FromBoolean(Billable);

            return result;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Creates an instance of a <see cref="TaskModel"/> from a dynamic Freshbooks object
        /// </summary>
        /// <param name="entry">The xml received from Freshbooks as a dynamic object.</param>
        /// <returns>The corresponding <see cref="Task"/></returns>
        public static TaskModel FromFreshbooksDynamic(dynamic entry)
        {
            return new TaskModel
            {
                Id = FreshbooksConvert.ToInt32(entry.task_id),
                Name = entry.name,
                Description = entry.description,
                Rate = FreshbooksConvert.ToDouble(entry.rate),
                Billable = FreshbooksConvert.ToBoolean(entry.billable)
            };
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the name of the Freshbooks entity related to this model
        /// </summary>
        public override string FreshbooksEntityName
        {
            get { return "task"; }
        }

        #endregion
    }
}
