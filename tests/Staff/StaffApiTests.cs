using System;
using Vooban.FreshBooks.DotNet.Api.Staff;
using Xunit;

namespace Vooban.FreshBooks.DotNet.Api.Tests.Staff
{    
    public class StaffApiTests
    {
        private const string USERNAME = "";
        private const string TOKEN = "";

        public class CallGetCurrent
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(USERNAME, TOKEN));
                
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
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(USERNAME, TOKEN));

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
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(USERNAME, TOKEN));

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
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(USERNAME, TOKEN));

                var testedClass = new StaffApi(freshbooks);

                var result = testedClass.CallGetAllPages();
                Assert.NotNull(result);
            }
        }
    }
}
