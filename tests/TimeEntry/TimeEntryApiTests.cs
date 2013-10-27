using System;
using Vooban.FreshBooks.DotNet.Api.TimeEntry;
using Vooban.FreshBooks.DotNet.Api.TimeEntry.Models;
using Xunit;

namespace Vooban.FreshBooks.DotNet.Api.Tests.TimeEntry
{    
    public class TimeEntryApiTests
    {
        private static readonly string _username = Environment.GetEnvironmentVariable("FreshbooksUsername");
        private static readonly string _token = Environment.GetEnvironmentVariable("FreshbooksToken");

        public class CallSearch
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new TimeEntryApi(freshbooks);

                var result = testedClass.CallSearch(new TimeEntryFilter() { ProjectId = "3228" });
                Assert.NotNull(result);
                Assert.True(result.Success, "The Freshbooks response indicated a fail");
            }
        }

        public class CallGetList
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new TimeEntryApi(freshbooks);

                var result = testedClass.CallGetList();
                Assert.NotNull(result);
                Assert.True(result.Success);
            }
        }

        public class CallGetMethod
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new TimeEntryApi(freshbooks);

                var result = testedClass.CallGet("581167");
                Assert.NotNull(result);
            }
        }


    }
}
