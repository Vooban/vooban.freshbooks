namespace Vooban.FreshBooks.Models
{
    public abstract class FreshbooksObject
    {
        /// <summary>
        /// Converts this instance to a Freshbooks compatible dynamic instance
        /// </summary>
        /// <returns>The dynamic instance representing the Freshbooks object</returns>
        public abstract dynamic ToFreshbooksDynamic();
    }
}