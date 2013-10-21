using System;
using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api.Staff.Models
{
    /// <summary>
    /// This class is the strongly-typed representation of the Freshbooks <c>Staff</c> entity
    /// </summary>
    /// <remarks>
    /// Here is the Freshbooks staff response
    ///   <staff>  
    ///      <staff_id>1</staff_id>  
    ///      <username>John</username>  
    ///      <first_name>John</first_name>  
    ///      <last_name>Smith</last_name>  
    ///      <email>John@example.org</email>  
    ///      <business_phone></business_phone>  
    ///      <mobile_phone></mobile_phone>  
    ///      <rate>0</rate>  
    ///      <last_login>2010-07-12 13:55:58</last_login>  
    ///      <number_of_logins>700</number_of_logins>  
    ///      <signup_date>2010-07-07 10:38:57</signup_date>  
    ///      <street1></street1>  
    ///      <street2></street2>  
    ///      <city></city>  
    ///      <state></state>  
    ///       <country></country>  
    ///      <code></code>  
    ///    </staff>  
    ///  </remarks>
    public class StaffModel : FreshbooksModel
    {
        /// <summary>
        /// Gets the username of the staff member, that is the username that is used to connect to Freshbooks.
        /// </summary>
        public string Username { get; internal set; }

        /// <summary>
        /// Gets the first name of the staff member as entered in Freshbooks.
        /// </summary>
        public string FirstName { get; internal set; }

        /// <summary>
        /// Gets the last name of the staff member as entered in Freshbooks.
        /// </summary>
        public string Lastname { get; internal set; }

        /// <summary>
        /// Gets the email address of the staff member as entered in Freshbooks or the address used to perform SSO with Freshbooks
        /// </summary>
        public string Email { get; internal set; }

        /// <summary>
        /// Gets the the staff member's business phone as entered in Freshbooks.
        /// </summary>
        public string BusinessPhone { get; internal set; }

        /// <summary>
        /// Gets the the staff member's mobile phone as entered in Freshbooks.
        /// </summary>
        public string MobilePhone { get; internal set; }

        /// <summary>
        /// Gets the the staff member's default rate for project with staff rate configuration
        /// </summary>
        /// <remarks>This value can be null if not specified in Freshbooks.</remarks>
        public double? Rate { get; internal set; }

        /// <summary>
        /// Gets the the staff member's last login date into the Freshbooks application
        /// </summary>
        public DateTime? LastLogin { get; internal set; }

        /// <summary>
        /// Gets the the number of time this staff member's logged into the application
        /// </summary>
        public int NumberOfLogins { get; internal set; }

        /// <summary>
        /// Get the date at which this staff member account was created.
        /// </summary>
        public DateTime? SignupDate { get; internal set; }

        /// <summary>
        /// Gets the the staff member's home address as configured in Freshbooks
        /// </summary>
        public AddressModel HomeAddress { get; internal set; }
    }
}
