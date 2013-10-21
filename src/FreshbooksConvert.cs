using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HastyAPI;
using Vooban.FreshBooks.DotNet.Api.Models;

namespace Vooban.FreshBooks.DotNet.Api
{
    /// <summary>
    /// Helper class to facilitate conversion from Freshbooks to the .NET world
    /// </summary>
    static class FreshbooksConvert
    {
        /// <summary>
        /// Converts the string value representing a boolean in Freshbooks to a real <c>boolean</c> value.
        /// </summary>
        /// <param name="value">The Freshbooks value, which is 1 for <c>true</c> and 0 for <c>false</c>.</param>
        /// <returns>The boolean value corresponding to the Freshbooks string</returns>
        public static bool ToBoolean(string value)
        {
            return !string.IsNullOrEmpty(value) && value == "1";
        }

        public static double? ToDouble(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToDouble(value, CultureInfo.InvariantCulture);
            }

            return null;
        }

        public static int ToInt32(object value)
        {
            if (value == null)
                throw new InvalidOperationException("value cannot be null");

            return ToInt32(value.ToString());
        }

        public static int ToInt32(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return Convert.ToInt32(value, CultureInfo.InvariantCulture);
            }

            throw new InvalidOperationException("value cannot be null or empty");
        }

        public static double? ToPercentage(string value)
        {
            var result = ToDouble(value);

            if (result != null)
                return result/100;

            return null;
        }

        public static DateTime? ToDateTime(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            }

            return null;
        }

        public static FreshbooksResponse<T> ToResponse<T>(dynamic value)
        {
            return new FreshbooksResponse<T> {
                Status = value.response.status == "ok"
            };
        }

        public static FreshbooksPagedResponse ToPagedResponse(dynamic value)
        {
            var responseFriendly = value.response as FriendlyDynamic;

            if (responseFriendly != null)
            {
                var keyValuePairsResponse = new Dictionary<string, object>(responseFriendly).ToList();
                var innerResponse = keyValuePairsResponse[2].Value as FriendlyDynamic;

                if (innerResponse != null)
                {
                    var pagingInfo = new Dictionary<string, object>(innerResponse);

                    return new FreshbooksPagedResponse
                    {
                        Status = value.response.status == "ok",
                        Page = ToInt32(pagingInfo["page"]),
                        ItemPerPage = ToInt32(pagingInfo["per_page"]),
                        TotalPages = ToInt32(pagingInfo["pages"]),
                        TotalItems = ToInt32(pagingInfo["total"])
                    };
                }
            }

            return null;
        }

        public static FreshbooksPagedResponse<T> ToPagedResponse<T>(dynamic value) 
        {
            return new FreshbooksPagedResponse<T>(ToPagedResponse(value));           
        }

    }
}
