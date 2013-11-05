using System;
using System.Collections.Generic;
using FreshBooks.Api.Models;

namespace FreshBooks.Api
{
    public class GenericApiOptions<T> where T: FreshbooksModel
    {
        /// <summary>
        /// Gets the name of the id property used with this entity
        /// </summary>
        public virtual string IdProperty { get; set; }

        /// <summary>
        /// Gets the Freshbooks delete command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        public virtual string DeleteCommand { get; set; }

        /// <summary>
        /// Gets the Freshbooks update command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        public virtual string UpdateCommand { get; set; }

        /// <summary>
        /// Gets the Freshbooks create command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        public virtual string CreateCommand { get; set; }

        /// <summary>
        /// Gets the Freshbooks get command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        public virtual string GetCommand { get; set; }

        /// <summary>
        /// Gets the Freshbooks list command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        public virtual string ListCommand { get; set; }

        /// <summary>
        /// Creates an entity of type <see cref="T"/> from the Freshbooks dynamic response
        /// </summary>
        /// <returns>The fully loaded entity</returns>
        public virtual Func<dynamic, T> FromDynamicModel { get; set; }

        /// <summary>
        /// Enumerates over the list response of the Freshbooks API
        /// </summary>
        /// <returns>An <see cref="IEnumerable{TimeEntryModel}"/> all loaded correctly</returns>
        public virtual Func<dynamic, IEnumerable<T>> BuildEnumerableFromDynamicResult { get; set; }
    }
}