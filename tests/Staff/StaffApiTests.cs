﻿using System;
using System.Linq;
using Newtonsoft.Json;
using Vooban.FreshBooks.Staff;
using Vooban.FreshBooks.Staff.Models;
using Xunit;

namespace Vooban.FreshBooks.Tests.Staff
{    
    public class StaffApiTests
    {
        private static readonly string _username = Environment.GetEnvironmentVariable("FreshbooksUsername");
        private static readonly string _token = Environment.GetEnvironmentVariable("FreshbooksToken");

        public class CallGetCurrent
        {       
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));
                
                var testedClass = new StaffApi(freshbooks);

                var result = testedClass.CallGetCurrent();
                Assert.NotNull(result);
            }
        }
        
        public class CallGetMethod
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new StaffApi(freshbooks);

                var result = testedClass.Get(1);
                Assert.NotNull(result);
            }
        }

        public class CallGetList
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new StaffApi(freshbooks);

                var result = testedClass.GetList();
                Assert.NotNull(result);

                Console.Write(JsonConvert.SerializeObject(result));
            }
        }

        public class CallSearch
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new StaffApi(freshbooks);

                var result = testedClass.SearchAll(new StaffFilter() { Email = "kevin.moore@vooban.com"});
                Assert.NotNull(result);

                Console.Write(JsonConvert.SerializeObject(result));
            }
        }

        public class CallGetAllPages
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new StaffApi(freshbooks);

                var result = testedClass.GetAllPages();
                Assert.NotNull(result);                
            }
        }

    }
}
