namespace Vooban.FreshBooks.DotNet.Api.Models
{
    public class FreshbooksResponse
    {
        public bool Status { get; set; }
    }

    public class FreshbooksResponse<T> : FreshbooksResponse
    {
        public T Result { get; set; }

        public FreshbooksResponse<T> WithResult(T result)
        {
            Result = result;
            return this;
        }
    }
}
