using Vooban.FreshBooks.Invoices.Models;

namespace Vooban.FreshBooks.Invoices
{
    public interface IInvoiceApi
        : IReadOnlyBasicApi<InvoiceModel>, ISearchableBasicApi<InvoiceModel, InvoiceFilter>
    {
    
    }
}