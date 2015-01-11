using System;
using System.Diagnostics;
using System.Dynamic;
using HastyAPI;
using Vooban.FreshBooks.Models;

namespace Vooban.FreshBooks.TimeEntry.Models
{
    /// <summary>
    /// Represents a Freshbooks TimeEntry (time_entry)
    /// </summary>
    /// <remarks>
    ///   <time_entry>  
    ///     <time_entry_id>211</time_entry_id>  
    ///     <staff_id>1</staff_id>  
    ///     <project_id>1</project_id>  
    ///     <task_id>1</task_id>  
    ///     <hours>2</hours>  
    ///     <date>2009-03-13</date>  
    ///     <notes>Sample Notes</notes>  
    ///     <billed>0</billed>   <!-- 1 or 0 (Read Only) -->  
    ///   </time_entry> 
    /// </remarks>
    [DebuggerDisplay("{Id} - Logged by {StaffId} on project {ProjectId} and task {TaskId}")]
    public class TimeEntryModel : FreshbooksModel
    {
        #region Properties

        /// <summary>
        /// Gets the staff member's unique identifier to which this time entry is associated
        /// </summary>
        public int StaffId { get; set; }

        /// <summary>
        /// Gets the projects's unique identifier to which this time entry is associated
        /// </summary>
        public int ProjectId { get; set; }

        /// <summary>
        /// Gets the task's unique identifier to which this time entry is associated
        /// </summary>
        public int TaskId { get; set; }

        /// <summary>
        /// Gets the number of hours worked on this project-task combination for this user
        /// </summary>
        public double? Hours { get; set; }

        /// <summary>
        /// Gets the date at which this time entry was create
        /// </summary>
        public DateTime? Date { get; set; }

        /// <summary>
        /// Gets the notes associated with the time entry
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Gets a value indicating whether or not the hours on this time entry were billed or not.
        /// </summary>
        public bool? Billed { get; set; }

        #endregion

        #region Instance Method

        /// <summary>
        /// Converts a strongly-typed .NET object into the corresponding dynamic Freshbooks object
        /// </summary>
        /// <returns>The dynamic object to be sent to Freshbooks</returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new ExpandoObject();

            result.time_entry_id = Id;
            result.staff_id = StaffId;
            result.project_id = ProjectId;
            result.task_id = TaskId;
            if (!string.IsNullOrEmpty(Notes)) result.notes = Notes;
            if (Hours.HasValue) result.hours = FreshbooksConvert.FromDouble(Hours);
            if (Date.HasValue) result.date = FreshbooksConvert.FromDateTime(Date);

            return result;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Creates an instance of a <see cref="TimeEntryModel"/> from a dynamic Freshbooks object
        /// </summary>
        /// <param name="entry">The xml received from Freshbooks as a dynamic object.</param>
        /// <returns>The corresponding <see cref="TimeEntryModel"/></returns>
        public static TimeEntryModel FromFreshbooksDynamic(dynamic entry)
        {
            return new TimeEntryModel
            {
                Id = FreshbooksConvert.ToInt32(entry.time_entry_id),
                StaffId = FreshbooksConvert.ToInt32(entry.staff_id),
                ProjectId = FreshbooksConvert.ToInt32(entry.project_id),
                TaskId = FreshbooksConvert.ToInt32(entry.task_id),
                Notes = entry.notes,
                Hours = FreshbooksConvert.ToDouble(entry.hours),
                Date = FreshbooksConvert.ToDateTime(entry.date),
                Billed = FreshbooksConvert.ToBoolean(entry.billed),
            };
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the name of the Freshbooks entity related to this model
        /// </summary>
        public override string FreshbooksEntityName
        {
            get { return "time_entry"; }
        }

        #endregion
    }
}
