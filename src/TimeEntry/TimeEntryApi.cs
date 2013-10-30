using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Staff;
using Vooban.FreshBooks.DotNet.Api.TimeEntry.Models;

namespace Vooban.FreshBooks.DotNet.Api.TimeEntry
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class TimeEntryApi : GenericApi<TimeEntryModel, TimeEntryFilter>
    {
        #region Constantes

        public const string COMMAND_TIMEENTRY_LIST = "time_entry.list";
        public const string COMMAND_TIMEENTRY_GET = "time_entry.get";
        public const string COMMAND_TIMEENTRY_DELETE = "time_entry.delete";
        public const string COMMAND_TIMEENTRY_CREATE = "time_entry.create";
        public const string COMMAND_TIMEENTRY_UPDATE = "time_entry.update";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        public TimeEntryApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
            :base(freshbooks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        public TimeEntryApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
            : base(freshbooks)
        {
        }

        #endregion

        #region GenericApi Overrides

        /// <summary>
        /// Gets the name of the id property used with this entity
        /// </summary>
        protected override string IdProperty
        {
            get { return "time_entry_id"; }
        }

        /// <summary>
        /// Gets the Freshbooks delete command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string DeleteCommand
        {
            get { return COMMAND_TIMEENTRY_DELETE; }
        }

        /// <summary>
        /// Gets the Freshbooks update command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string UpdateCommand
        {
            get { return COMMAND_TIMEENTRY_UPDATE; }
        }

        /// <summary>
        /// Gets the Freshbooks create command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string CreateCommand
        {
            get { return COMMAND_TIMEENTRY_CREATE; }
        }

        /// <summary>
        /// Gets the Freshbooks get command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string GetCommand
        {
            get { return COMMAND_TIMEENTRY_GET; }
        }

        /// <summary>
        /// Gets the Freshbooks list command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string ListCommand
        {
            get { return COMMAND_TIMEENTRY_LIST; }
        }

        /// <summary>
        /// Creates an entity of type <see cref="TimeEntryModel"/> from the Freshbooks dynamic response
        /// </summary>
        /// <param name="response">The response received from Freshbooks</param>
        /// <returns>The fully loaded entity</returns>
        protected override TimeEntryModel FromDynamicModel(dynamic response)
        {
            return TimeEntryModel.FromFreshbooksDynamic(response.response.time_entry);
        }

        /// <summary>
        /// Enumerates over the list response of the Freshbooks API
        /// </summary>
        /// <param name="resultList">The Freshbooks response</param>
        /// <returns>An <see cref="IEnumerable{TimeEntryModel}"/> all loaded correctly</returns>
        protected override IEnumerable<TimeEntryModel> BuildEnumerableFromDynamicResult(dynamic resultList)
        {
            foreach (var item in resultList.response.time_entries.time_entry)
                yield return TimeEntryModel.FromFreshbooksDynamic(item);
        }

        #endregion
    }

}