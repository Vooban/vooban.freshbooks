using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using HastyAPI.FreshBooks.Wrapper.Models;

namespace HastyAPI.FreshBooks.Wrapper
{
    static class FreshbooksConvert
    {
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
