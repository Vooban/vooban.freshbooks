using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Vooban.FreshBooks.Staff;
using Vooban.FreshBooks.TimeEntry;
using Vooban.FreshBooks.TimeEntry.Models;
using Xunit;
using Vooban.FreshBooks.Task;
using System.Diagnostics;
using Vooban.FreshBooks.Project;

namespace Vooban.FreshBooks.Tests.TimeEntry
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
                
                var result = testedClass.Search(new TimeEntryFilter { ProjectId = "3228" });
                Assert.NotNull(result);
                Assert.True(result.Success, "The Freshbooks response indicated a fail");
            }
        }

        public class CallCreate
        {
            [Fact]            
          public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));
                var testedClass = new TimeEntryApi(freshbooks);

                Assert.Throws<InvalidOperationException>(() => testedClass.Create(new TimeEntryModel { TaskId = 999999, Hours = 25d}));
            }
 }

        public class CallGetList
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new TimeEntryApi(freshbooks);

                var result = testedClass.GetList();
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

                var result = testedClass.Get(581167);
                Assert.NotNull(result);
            }
        }


    }
}
