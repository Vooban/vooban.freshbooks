using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api
{
    public interface IFullBasicApi<T, TF> : ICrudBasicApi<T>, IReadOnlyBasicApi<T>, ISearchableBasicApi<T, TF>
        where T : FreshbooksModel
        where TF : FreshbooksFilter
    {
        
    }
}