using System;
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

        public FreshbooksPagedResponse(){}
    }

    public class FreshbooksPagedResponse<T> : FreshbooksPagedResponse
    {
        public IEnumerable<T> Result { get; set; }

        public FreshbooksPagedResponse() {}

        public FreshbooksPagedResponse(FreshbooksPagedResponse inner) : base(inner)
        {
            Page = inner.Page;
            ItemPerPage = inner.ItemPerPage;
            TotalPages = inner.TotalPages;
            TotalItems = inner.TotalItems;
        }

        public FreshbooksPagedResponse<T> WithResult(Func<IEnumerable<T>> resultBuilder)
        {
            Result = resultBuilder();
            return this;
        }

        public FreshbooksPagedResponse<T> WithResult(IEnumerable<T> result)
        {
            Result = result;
            return this;
        }
    }
}