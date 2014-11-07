using Vooban.FreshBooks.Models;

namespace Vooban.FreshBooks
{
    public interface IFullApi<T, in TF> : ICrudBasicApi<T>, IReadOnlyBasicApi<T>, ISearchableBasicApi<T, TF>
        where T : FreshbooksModel
        where TF : FreshbooksFilter
    {
        
    }
}