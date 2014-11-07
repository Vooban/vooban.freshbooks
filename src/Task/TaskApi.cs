using System;
using System.Collections.Generic;
using Vooban.FreshBooks.Models;
using Vooban.FreshBooks.Project.Models;
using Vooban.FreshBooks.Task.Models;

namespace Vooban.FreshBooks.Task
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class TaskApi 
        : ITaskApi
    {
        #region Private Classes

        public class TaskApiOptions : GenericApiOptions<TaskModel>
        {
            #region Constantes

            public const string COMMAND_LIST = "task.list";
            public const string COMMAND_GET = "task.get";
            public const string COMMAND_DELETE = "task.delete";
            public const string COMMAND_CREATE = "task.create";
            public const string COMMAND_UPDATE = "task.update";

            #endregion

            #region Properties

            /// <summary>
            /// Gets the name of the id property used with this entity
            /// </summary>
            public override string IdProperty
            {
                get { return "task_id"; }
            }

            /// <summary>
            /// Gets the Freshbooks delete command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string DeleteCommand
            {
                get { return COMMAND_DELETE; }
            }

            /// <summary>
            /// Gets the Freshbooks update command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string UpdateCommand
            {
                get { return COMMAND_UPDATE; }
            }

            /// <summary>
            /// Gets the Freshbooks create command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string CreateCommand
            {
                get { return COMMAND_CREATE; }
            }

            /// <summary>
            /// Gets the Freshbooks get command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string GetCommand
            {
                get { return COMMAND_GET; }
            }

            /// <summary>
            /// Gets the Freshbooks list command name
            /// </summary>
            /// <value>If the returned value is null, the delete command will fail with an <see cref="NotSupportedException"/></value>
            public override string ListCommand
            {
                get { return COMMAND_LIST; }
            }

            /// <summary>
            /// Creates an entity of type <see cref="ProjectModel"/> from the Freshbooks dynamic response
            /// </summary>
            /// <returns>The fully loaded entity</returns>
            public override Func<dynamic, TaskModel> FromDynamicModel
            {
                get
                {
                    return d => TaskModel.FromFreshbooksDynamic(d.response.task);
                }
            }

            /// <summary>
            /// Enumerates over the list response of the Freshbooks API
            /// </summary>
            /// <returns>An <see cref="IEnumerable{TimeEntryModel}"/> all loaded correctly</returns>
            public override Func<dynamic, IEnumerable<TaskModel>> BuildEnumerableFromDynamicResult
            {
                get
                {
                    return resultList =>
                    {
                        var result = new List<TaskModel>();

                        foreach (var item in resultList.response.tasks.task)
                            result.Add(TaskModel.FromFreshbooksDynamic(item));

                        return result;
                    };
                }
            }

            public static TaskApiOptions Options { get { return new TaskApiOptions(); } }

            #endregion
        }

        #endregion

        #region Private Members

        private readonly GenericApi<TaskModel, TaskFilter> _api;

        #endregion

        #region Constructors

        public TaskApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks) 
        {
            _api = new GenericApi<TaskModel, TaskFilter>(freshbooks, TaskApiOptions.Options);
        }

        public TaskApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
        {
            _api = new GenericApi<TaskModel, TaskFilter>(freshbooks, TaskApiOptions.Options);
        }

        #endregion

        #region IFullBasicApi

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to create the entry in Freshbooks</param>
        /// <returns>
        /// The <see cref="FreshbooksCreateResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>create</c> operation using an entity with an Id</exception>
        public string Create(TaskModel entity)
        {
            return _api.Create(entity);
      }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to update the entry</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>update</c> operation using an entity without an Id</exception>
        public void Update(TaskModel entity)
        {
            _api.Update(entity);
      }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">The entity to delete.</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>delete</c> operation using an empty Id
        /// or
        /// Cannot call the <c>delete</c> operation using an empty Id identifier</exception>
        public void Delete(TaskModel entity)
        {
            _api.Delete(entity);
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="id">The identifier of the time entry to delete.</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>delete</c> operation using an empty Id
        /// or
        /// Cannot call the <c>delete</c> operation using an empty Id identifier</exception>
        public void Delete(string id)
        {
            _api.Delete(id);
    }

        /// <summary>
        /// Call the api.get method on the Freshbooks API.
        /// </summary>
        /// <param name="id">The staff id that you want to get information for.</param>
        /// <returns>
        /// The <see cref="ProjectModel" /> information for the specified <paramref name="id" />
        /// </returns>
        public TaskModel Get(string id)
        {
            return _api.Get(id);
        }

        /// <summary>
        /// Call the api.list method on the Freshbooks API.
        /// </summary>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{ProjectModel}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage
        /// </exception>
        public FreshbooksPagedResponse<TaskModel> GetList(int page = 1, int itemPerPage = 100)
        {
            return _api.GetList(page, itemPerPage);
        }

        /// <summary>
        /// Get all the items available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the api.list method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<TaskModel> GetAllPages()
        {
            return _api.GetAllPages();
        }

        /// <summary>
        /// Call the api.list method on the Freshbooks API to perform a search
        /// </summary>
        /// <param name="template">The template used to query the time entry API.</param>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{T}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage</exception>
        public FreshbooksPagedResponse<TaskModel> Search(TaskFilter template, int page = 1, int itemPerPage = 100)
        {
            return _api.Search(template, page, itemPerPage);
        }

        /// <summary>
        /// Call the api.list method on the Freshbooks API.
        /// </summary>
        /// <param name="template">The template used to query the time entry API.</param>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{T}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage</exception>
        public IEnumerable<TaskModel> SearchAll(TaskFilter template, int page = 1, int itemPerPage = 100)
        {
            return _api.SearchAll(template, page, itemPerPage);
        }

        #endregion
    }
}