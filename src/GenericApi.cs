using System;
using System.Collections.Generic;
using Vooban.FreshBooks.Models;

namespace Vooban.FreshBooks
{
    /// <summary>
    /// Generic API implementing all the common features of a Freshbooks API
    /// </summary>
    /// <typeparam name="T">The type of model managed by the class instance</typeparam>
    /// <typeparam name="TF">The type of filter used to search Freshbooks for this API</typeparam>
    /// <remarks>The child classes can return null to command the API does not implement</remarks>
    public class GenericApi<T, TF> :
        GenericApiBase<T>, IFullApi<T, TF>
        where T : FreshbooksModel
        where TF : FreshbooksFilter
    {
        #region Private Members

        private readonly GenericApiOptions<T> _options;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericApi{T, TF}" /> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use as a <c>Lazy</c> instance.</param>
        /// <param name="options">The options used by this class.</param>
        public GenericApi(Lazy<HastyAPI.FreshBooks.FreshBooks> freshbooks, GenericApiOptions<T> options)
            : base(freshbooks)
        {
            _options = options;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericApi{T, TF}"/> class.
        /// </summary>
        /// <param name="freshbooks">The freshbooks client to use.</param>
        /// <param name="options">The options used by this class.</param>
        public GenericApi(HastyAPI.FreshBooks.FreshBooks freshbooks, GenericApiOptions<T> options)
            : base(freshbooks)
        {
            _options = options;
        }

        #endregion

        #region IFullBasicApi

        /// <summary>
        /// Call the api.get method on the Freshbooks API.
        /// </summary>
        /// <param name="id">The staff id that you want to get information for.</param>
        /// <returns>
        /// The <see cref="T" /> information for the specified <paramref name="id" />
        /// </returns>
        public T Get(int id)
      {
            if (!string.IsNullOrEmpty(_options.GetCommand))
                return CallGetMethod(_options.GetCommand, p => ((IDictionary<string, object>)p)[_options.IdProperty] = id, r => _options.FromDynamicModel(r));

            throw new NotSupportedException("The [get] operation is not supported");
        }

        /// <summary>
        /// Call the api.list method on the Freshbooks API.
        /// </summary>
        /// <param name="page">The page you want to get.</param>
        /// <param name="itemPerPage">The number of item per page to get.</param>
        /// <returns>
        /// The whole <see cref="FreshbooksPagedResponse{StaffModel}" /> containing paging information and result for the requested page.
        /// </returns>
        /// <exception cref="System.ArgumentException">
        /// Please ask for at least 1 item per page otherwise this call is irrelevant.;itemPerPage
        /// or
        /// The max number of items per page supported by Freshbooks is 100.;itemPerPage
        /// </exception>
        public FreshbooksPagedResponse<T> GetList(int page = 1, int itemPerPage = 100)
      {
            if (!string.IsNullOrEmpty(_options.ListCommand))
                return CallGetListMethod(_options.ListCommand, r => _options.BuildEnumerableFromDynamicResult(r), null, page, itemPerPage);

            throw new NotSupportedException("The [list] operation is not supported");
        }

        /// <summary>
        /// Get all the items available on Freshbooks with a single API call.
        /// </summary>
        /// <remarks>
        /// This method call the api.list method for each available pages and gather all that information into a single list
        /// </remarks>
        /// <returns>The entire content available on Freshbooks</returns>
        public IEnumerable<T> GetAllPages()
      {
            if (!string.IsNullOrEmpty(_options.ListCommand))
                return CallGetAllPagesMethod(_options.ListCommand, r => _options.BuildEnumerableFromDynamicResult(r));

            throw new NotSupportedException("The [list] operation is not supported");
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to create the entry in Freshbooks</param>
        /// <returns>
        /// The <see cref="FreshbooksCreateResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>create</c> operation using an entity with an Id</exception>
        public string Create(T entity)
        {
            if (!string.IsNullOrEmpty(_options.CreateCommand))
                return CallCreateMethod(_options.CreateCommand, entity, createResponse => (string)((IDictionary<string, object>)createResponse.response)[_options.IdProperty]);

            throw new NotSupportedException("The [create] operation is not supported");
        }

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to update the entry</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>update</c> operation using an entity without an Id</exception>
        public void Update(T entity)
        {
            if (!string.IsNullOrEmpty(_options.UpdateCommand))
                CallUpdateMethod(_options.UpdateCommand, entity);

            throw new NotSupportedException("The [update] operation is not supported");
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
        public void Delete(T entity)
        {
            if (!string.IsNullOrEmpty(_options.DeleteCommand))
                CallDeleteMethod(_options.DeleteCommand, _options.IdProperty, entity.Id.Value);

            throw new NotSupportedException("The [delete] operation is not supported");
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
        public void Delete(int id)
      {
            if (!string.IsNullOrEmpty(_options.DeleteCommand))
                CallDeleteMethod(_options.DeleteCommand, _options.IdProperty, id);

        throw new NotSupportedException("The [delete] operation is not supported");
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
        public FreshbooksPagedResponse<T> Search(TF template, int page = 1, int itemPerPage = 100)
      {
            if (!string.IsNullOrEmpty(_options.ListCommand))
                return CallSearchMethod(_options.ListCommand, r => _options.BuildEnumerableFromDynamicResult(r), template, page, itemPerPage);

            throw new NotSupportedException("The [search] operation is not supported");
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
        public IEnumerable<T> SearchAll(TF template, int page = 1, int itemPerPage = 100)
      {
            if (!string.IsNullOrEmpty(_options.ListCommand))
                return CallSearchAllMethod(_options.ListCommand, r => _options.BuildEnumerableFromDynamicResult(r), template);

            throw new NotSupportedException("The [search all] operation is not supported");
        }

        #endregion
    }
}