using System.Collections.Generic;

namespace Vooban.FreshBooks.DotNet.Api.Models
{
    public class FreshbooksPagedResponse : FreshbooksResponse
    {
        public int Page { get; set; }

        public int ItemPerPage { get; set; }

        public int TotalPages { get; set; }

        public int TotalItems { get; set; }

        public FreshbooksPagedResponse(FreshbooksPagedResponse inner)
        {           
            Status = inner.Status;
        }

        internal FreshbooksPagedResponse()
        { }
    }

    public class FreshbooksPagedResponse<T> : FreshbooksPagedResponse
    {
        public IEnumerable<T> Result { get; set; }

        public FreshbooksPagedResponse(FreshbooksPagedResponse inner) : base(inner)
        {
            Page = inner.Page;
            ItemPerPage = inner.ItemPerPage;
            TotalPages = inner.TotalPages;
            TotalItems = inner.TotalItems;
        }
    }
}