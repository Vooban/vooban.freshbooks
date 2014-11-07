using System.Collections.Generic;
using System.Dynamic;
using HastyAPI;
using Vooban.FreshBooks.Models;
using Vooban.FreshBooks.Staff.Models;
using Vooban.FreshBooks.Task.Models;
using System.Linq;

namespace Vooban.FreshBooks.Project.Models
{
    /// <summary>
    /// Represents a Freshbooks TimeEntry (time_entry)
    /// </summary>
    /// <remarks>
    ///  <project>  
    ///     <project_id>6</project_id>  
    ///     <name>Super Fun Project</name>  
    ///     <description></description>  
    ///     <rate>11000</rate>  
    ///     <bill_method>flat-rate</bill_method>  
    ///     <client_id>119</client_id>  
    ///     <hour_budget>42</hour_budget>  
    ///     <tasks>  
    ///       <task>  
    ///         <task_id>5</task_id>  
    ///       </task>  
    ///     </tasks>  
    ///     <staff>  
    ///        <staff>  
    ///         <staff_id>1</staff_id>  
    ///       </staff>  
    ///     </staff> 
    ///     <budget>  
    ///         <hours>00.00</hours>  
    ///     </budget>  
    ///   </project>  
    /// </remarks>
    public class ProjectModel : FreshbooksModel
    {
        #region Properties

        /// <summary>
        /// Gets the number of hours worked on this project-task combination for this user
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// Gets the description attached to the project
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Gets a value indicating whether or not the hours on this time entry were billed or not.
        /// </summary>
        /// <remarks>
        /// Replace with an enumeration
        /// </remarks>
        public string BillMethod { get; internal set; }

        /// <summary>
        /// Gets the unique client identifier to which this project belongs.
        /// </summary>
        public string ClientId { get; internal set; }

        /// <summary>
        /// Gets the rate at which people will bill on this project
        /// </summary>
        public double? Rate { get; internal set; }

        /// <summary>
        /// Gets the number of hour that can be billed on this project
        /// </summary>
        public double? HourBudget { get; internal set; }

        /// <summary>
        /// Gets the task that are attached to this project
        /// </summary>
        public IEnumerable<string> TaskIds { get; internal set; }

        /// <summary>
        /// Gets the staff member attached to this project
        /// </summary>
        public IEnumerable<string> StaffIds { get; internal set; }

        #endregion

        #region Instance Method

        /// <summary>
        /// Converts a strongly-typed .NET object into the corresponding dynamic Freshbooks object
        /// </summary>
        /// <returns>The dynamic object to be sent to Freshbooks</returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new ExpandoObject();

            if (!string.IsNullOrEmpty(Id)) result.project_id = Id;
            if (!string.IsNullOrEmpty(Name)) result.name = Name;
            if (!string.IsNullOrEmpty(Description)) result.description = Description;
            if (!string.IsNullOrEmpty(BillMethod)) result.bill_method = BillMethod;
            if (!string.IsNullOrEmpty(ClientId)) result.client_id = ClientId;

            if (Rate.HasValue) result.rate = FreshbooksConvert.FromDouble(Rate);
            if (HourBudget.HasValue) result.hour_budget = FreshbooksConvert.FromDouble(HourBudget);

            // TODO : Staff and Tasks

            return result;
        }

        #endregion

        #region Static Methods

        /// <summary>
        /// Creates an instance of a <see cref="ProjectModel"/> from a dynamic Freshbooks object
        /// </summary>
        /// <param name="entry">The xml received from Freshbooks as a dynamic object.</param>
        /// <returns>The corresponding <see cref="ProjectModel"/></returns>
        public static ProjectModel FromFreshbooksDynamic(dynamic entry)
        {
            var result =  new ProjectModel
            {
                Id = entry.project_id,
                Name = entry.name,
                Description = entry.description,
                BillMethod = entry.bill_method,
                ClientId = entry.client_id,
                Rate = FreshbooksConvert.ToDouble(entry.rate),
                HourBudget = FreshbooksConvert.ToDouble(entry.hour_budget)
            };

            var tasks  = new List<string>();

            if (entry.tasks != null)
            {
                if (entry.tasks.task is List<object>)
                {
                    foreach (var item in entry.tasks.task)
                        tasks.Add(item.task_id);
                }
                else
                {
                    foreach (var item in entry.tasks)                    
                        tasks.Add(Helpers.ToDynamic(item).task.task_id);                   
                }
                              
                result.TaskIds = tasks;
            }

            var members = new List<string>();

            if (entry.staff != null)
            {
                if (entry.tasks.task is List<object>)
                {
                    foreach (var item in entry.staff.staff)
                        members.Add(item.staff_id);
                }
                else
                {
                    foreach (var item in entry.staff.staff)
                        tasks.Add(Helpers.ToDynamic(item).staff_id);
                }
   
                result.StaffIds = members;
            }

            return result;
        }

        #endregion

        #region Overrides

        /// <summary>
        /// Gets the name of the Freshbooks entity related to this model
        /// </summary>
        public override string FreshbooksEntityName
        {
            get { return "project"; }
        }

        #endregion
    }
}
