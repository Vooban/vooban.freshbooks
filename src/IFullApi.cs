using FreshBooks.Api.Models;

namespace FreshBooks.Api
{
    public interface IFullApi<T, in TF> : ICrudBasicApi<T>, IReadOnlyBasicApi<T>, ISearchableBasicApi<T, TF>
        where T : FreshbooksModel
        where TF : FreshbooksFilter
    {
        
    }
}