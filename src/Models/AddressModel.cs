namespace Vooban.FreshBooks.DotNet.Api.Models
{
    /// <summary>
    /// This class model represent an address in the Freshbooks world. It can represent any address in Freshbooks
    /// </summary>
    public class AddressModel
    {
        /// <summary>
        /// Gets the first line of the address
        /// </summary>
        public string Street1{ get; internal set; }

        /// <summary>
        /// Gets the second line of the address
        /// </summary>
        public string Street2 { get; internal set; }

        /// <summary>
        /// Gets the name of the city
        /// </summary>
        public string City { get; internal set; }

        /// <summary>
        /// Gets the state or province
        /// </summary>
        public string State { get; internal set; }

        /// <summary>
        /// Gets the country
        /// </summary>
        public string Country { get; internal set; }

        /// <summary>
        /// Gets the country code
        /// </summary>
        public string Code { get; internal set; }
    }
}