using System;
using System.Collections.Generic;
using HastyAPI;
using Newtonsoft.Json;
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
        public int? NumberOfLogins { get; internal set; }

        /// <summary>
        /// Get the date at which this staff member account was created.
        /// </summary>
        public DateTime? SignupDate { get; internal set; }

        /// <summary>
        /// Gets the the staff member's home address as configured in Freshbooks
        /// </summary>
        public StaffAddressModel HomeAddress { get; internal set; }

        /// <summary>
        /// Gets the list of projects identifiers to which this person is associated
        /// </summary>
        public IEnumerable<String> ProjectIds { get; internal set; }

        /// <summary>
        /// Converts this instance to a Freshbooks compatible dynamic instance
        /// </summary>
        /// <returns>
        /// The dynamic instance representing the Freshbooks object
        /// </returns>
        public override dynamic ToFreshbooksDynamic()
        {
            dynamic result = new FriendlyDynamic();

            if (!string.IsNullOrEmpty(Id)) result.staff_id = Id;
            if (!string.IsNullOrEmpty(Username)) result.username = Username;
            if (!string.IsNullOrEmpty(FirstName)) result.first_name = FirstName;
            if (!string.IsNullOrEmpty(Lastname)) result.last_name = Lastname;
            if (!string.IsNullOrEmpty(Email)) result.email = Email;
            if (!string.IsNullOrEmpty(BusinessPhone)) result.business_phone = BusinessPhone;
            if (!string.IsNullOrEmpty(MobilePhone)) result.mobile_phone = MobilePhone;
            if (Rate.HasValue) result.rate = FreshbooksConvert.FromDouble(Rate);
            if (LastLogin.HasValue) result.last_login = FreshbooksConvert.FromDateTime(LastLogin);
            if (NumberOfLogins.HasValue) result.number_of_logins = FreshbooksConvert.FromInteger(NumberOfLogins);
            if (SignupDate.HasValue) result.signup_date = FreshbooksConvert.FromDateTime(SignupDate);
           
            if (HomeAddress != null)
            {
                if (!string.IsNullOrEmpty(HomeAddress.Street1)) result.street1 = HomeAddress.Street1;
                if (!string.IsNullOrEmpty(HomeAddress.Street2)) result.street2 = HomeAddress.Street2;
                if (!string.IsNullOrEmpty(HomeAddress.City)) result.city = HomeAddress.City;
                if (!string.IsNullOrEmpty(HomeAddress.Street1)) result.state = HomeAddress.State;
                if (!string.IsNullOrEmpty(HomeAddress.Country)) result.country = HomeAddress.Country;
                if (!string.IsNullOrEmpty(HomeAddress.Code)) result.code = HomeAddress.Code;               
            }

            return result;
        }

        /// <summary>
        /// Map the Freshbooks dynamic object to the <see cref="StaffModel"/> class.
        /// </summary>
        /// <param name="staffMember">The dynamic object loaded from the Freshbooks response</param>
        /// <returns>The <see cref="StaffModel"/> instance correctly filled with the Freshbooks response</returns>
        public static StaffModel FromFreshbooksDynamic(dynamic staffMember)
        {
            var projectIds = new List<String>();

            if (staffMember.projects != null && staffMember.projects.project != null)
            {
                foreach (var item in staffMember.projects.project)
                {
                    projectIds.Add(item.Key == "project_id" ? item.Value : item.project_id);
                }
            }

            return new StaffModel
            {
                Id = staffMember.staff_id,
                Username = staffMember.username,
                FirstName = staffMember.first_name,
                Lastname = staffMember.last_name,
                Email = staffMember.email,
                BusinessPhone = staffMember.business_phone,
                MobilePhone = staffMember.mobile_phone,
                Rate = FreshbooksConvert.ToDouble(staffMember.rate),
                LastLogin = FreshbooksConvert.ToDateTime(staffMember.last_login),
                NumberOfLogins = FreshbooksConvert.ToInt32(staffMember.number_of_logins),
                SignupDate = FreshbooksConvert.ToDateTime(staffMember.signup_date),
                HomeAddress = StaffAddressModel.FromFreshbooksDynamic(staffMember),
                ProjectIds = projectIds
            };
        }

       
    }
}
