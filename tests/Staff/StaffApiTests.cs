using System;
using System.Linq;
using Vooban.FreshBooks.DotNet.Api.Staff;
using Vooban.FreshBooks.DotNet.Api.Staff.Models;
using Xunit;

namespace Vooban.FreshBooks.DotNet.Api.Tests.Staff
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

                var result = testedClass.CallGet("1");
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

                var result = testedClass.CallGetList();
                Assert.NotNull(result);
            }
        }

        public class CallGetAllPages
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new StaffApi(freshbooks);

                var result = testedClass.CallGetAllPages();
                Assert.NotNull(result);
            }
        }

    }
}
