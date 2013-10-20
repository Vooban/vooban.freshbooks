using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HastyAPI.FreshBooks.Wrapper.Models
{
    /// <summary>
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
    ///  </summary>
    public class Staff : FreshbooksModelBase
    {
        public string Username { get; set; }

        public string FirstName { get; set; }

        public string Lastname { get; set; }

        public string Email { get; set; }

        public string BusinessPhone { get; set; }

        public string MobilePhone { get; set; }

        public double? Rate { get; set; }

        public DateTime? LastLogin { get; set; }

        public int NumberOfLogins { get; set; }

        public DateTime? SignupDate { get; set; }

        public Address HomeAddress { get; set; }
    }
}
