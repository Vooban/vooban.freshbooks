using System;
using System.Collections.Generic;
using FreshBooks.Api.Models;
using FreshBooks.Api.Project.Models;

namespace FreshBooks.Api.Project
{
    /// <summary>
    /// This class provide core methods and returns Freshbooks response objects, if you have to  work with Freshbooks responses statuses.
    /// </summary>
    public class ProjectApi 
        : IProjectApi
    {
        #region Private Classes

        public class ProjectApiOptions : GenericApiOptions<ProjectModel>
        {
            #region Constantes

            public const string COMMAND_LIST = "project.list";
            public const string COMMAND_GET = "project.get";
            public const string COMMAND_DELETE = "project.delete";
            public const string COMMAND_CREATE = "project.create";
            public const string COMMAND_UPDATE = "project.update";

            #endregion

            #region Properties

            /// <summary>
            /// Gets the name of the id property used with this entity
            /// </summary>
            public override string IdProperty
            {
                get { return "project_id"; }
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
            public override Func<dynamic, ProjectModel> FromDynamicModel
            {
                get
                {
                    return d => ProjectModel.FromFreshbooksDynamic(d.response.project);
                }
            }

            /// <summary>
            /// Enumerates over the list response of the Freshbooks API
            /// </summary>
            /// <returns>An <see cref="IEnumerable{TimeEntryModel}"/> all loaded correctly</returns>
            public override Func<dynamic, IEnumerable<ProjectModel>> BuildEnumerableFromDynamicResult
            {
                get
                {
                    return resultList =>
                    {
                        var result = new List<ProjectModel>();

                        foreach (var item in resultList.response.projects.project)
                            result.Add(ProjectModel.FromFreshbooksDynamic(item));

                        return result;
                    };
                }
            }

            public static ProjectApiOptions Options { get { return new ProjectApiOptions(); } }

            #endregion
        }

        #endregion

        #region Private Members

        private readonly GenericApi<ProjectModel, ProjectFilter> _api;

        #endregion

        #region Constructors

        public ProjectApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks) 
        {
            _api = new GenericApi<ProjectModel, ProjectFilter>(freshbooks, ProjectApiOptions.Options);
        }

        public ProjectApi(HastyAPI.FreshBooks.FreshBooks freshbooks)
        {
            _api = new GenericApi<ProjectModel, ProjectFilter>(freshbooks, ProjectApiOptions.Options);
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
        public string Create(ProjectModel entity)
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
        public void Update(ProjectModel entity)
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
        public void Delete(ProjectModel entity)
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
        public ProjectModel Get(string id)
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
        public FreshbooksPagedResponse<ProjectModel> GetList(int page = 1, int itemPerPage = 100)
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
        public IEnumerable<ProjectModel> GetAllPages()
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
        public FreshbooksPagedResponse<ProjectModel> Search(ProjectFilter template, int page = 1, int itemPerPage = 100)
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
        public IEnumerable<ProjectModel> SearchAll(ProjectFilter template, int page = 1, int itemPerPage = 100)
        {
            return _api.SearchAll(template, page, itemPerPage);
        }

        #endregion
    }
}