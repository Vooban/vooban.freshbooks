using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api
{
    public interface IFullBasicApi<T, in TF> : ICrudBasicApi<T>, IReadOnlyBasicApi<T>, ISearchableBasicApi<T, TF> 
        where T : FreshbooksModel
        where TF: FreshbooksFilter
    {
        
    }

    public interface ICrudBasicApi<in T>
        where T : FreshbooksModel
    {       
        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to create the entry in Freshbooks</param>
        /// <returns>
        /// The <see cref="FreshbooksCreateResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>create</c> operation using an entity with an Id</exception>
        FreshbooksCreateResponse CallCreate(T entity);

        /// <summary>
        /// Creates a new entry in Freshbooks
        /// </summary>
        /// <param name="entity">Then entity used to update the entry</param>
        /// <returns>
        /// The <see cref="FreshbooksResponse" /> correctly populated with Freshbooks official response
        /// </returns>
        /// <exception cref="System.InvalidOperationException">Cannot call the <c>update</c> operation using an entity without an Id</exception>
        FreshbooksResponse CallUpdate(T entity);

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
        FreshbooksResponse CallDelete(T entity);

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
        FreshbooksResponse CallDelete(string id);
    }
}