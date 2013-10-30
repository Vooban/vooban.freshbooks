using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;
using Vooban.FreshBooks.DotNet.Api.Models;
using Vooban.FreshBooks.DotNet.Api.Staff.Models;

namespace Vooban.FreshBooks.DotNet.Api.Staff
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class StaffApi : GenericApi<StaffModel, StaffFilter>
    {
        #region Constantes

        public const string COMMAND_STAFF_LIST = "staff.list";
        public const string COMMAND_STAFF_CURRENT = "staff.current";
        public const string COMMAND_STAFF_GET = "staff.get";

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        [InjectionConstructor]
        public StaffApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks)
            :base(freshbooks)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaffApi"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        public StaffApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
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
            get { return "staff_id"; }
        }

        /// <summary>
        /// Gets the Freshbooks delete command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string DeleteCommand
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the Freshbooks update command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string UpdateCommand
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the Freshbooks create command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string CreateCommand
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the Freshbooks get command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string GetCommand
        {
            get { return COMMAND_STAFF_GET; }
        }

        /// <summary>
        /// Gets the Freshbooks list command name
        /// </summary>
        /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
        protected override string ListCommand
        {
            get { return COMMAND_STAFF_LIST; }
        }

        /// <summary>
        /// Creates an entity of type <see cref="StaffModel"/> from the Freshbooks dynamic response
        /// </summary>
        /// <param name="response">The response received from Freshbooks</param>
        /// <returns>The fully loaded entity</returns>
        protected override StaffModel FromDynamicModel(dynamic response)
        {
            return StaffModel.FromFreshbooksDynamic(response.response.staff);
        }

        /// <summary>
        /// Enumerates over the list response of the Freshbooks API
        /// </summary>
        /// <param name="response">The Freshbooks response</param>
        /// <returns>An <see cref="IEnumerable{StaffModel}"/> all loaded correctly</returns>
        protected override IEnumerable<StaffModel> BuildEnumerableFromDynamicResult(dynamic response)
        {
            foreach (var staffMember in response.response.staff_members.member)
                yield return StaffModel.FromFreshbooksDynamic(staffMember);
        }

        #endregion

        #region Specialities

        /// <summary>
        /// Call the <c>staff.current</c> method on the Freshbooks API.
        /// </summary>
        /// <returns>The <see cref="StaffModel"/> information of the client used to communicate with Freshbooks</returns>
        public FreshbooksGetResponse<StaffModel> CallGetCurrent()
        {
            return CallGetMethod(COMMAND_STAFF_CURRENT, p => FromDynamicModel(p));
        }

        #endregion
    }
}