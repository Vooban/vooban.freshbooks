using System;
using Vooban.FreshBooks.Project;
using Vooban.FreshBooks.Project.Models;
using Xunit;

namespace Vooban.FreshBooks.Tests.Project
{    
    public class ProjectApiTests
    {
        private static readonly string _username = Environment.GetEnvironmentVariable("FreshbooksUsername");
        private static readonly string _token = Environment.GetEnvironmentVariable("FreshbooksToken");

        public class CallSearch
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new ProjectApi(freshbooks);

                var result = testedClass.Search(new ProjectFilter { TaskId = "8" });
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
                var testedClass = new ProjectApi(freshbooks);

                Assert.Throws<InvalidOperationException>(() => testedClass.Create(new ProjectModel { Name = "Failing test"}));
            }
 }

        public class CallGetList
        {
            [Fact]
            public void WorksAsExpected()
            {
                var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks(_username, _token));

                var testedClass = new ProjectApi(freshbooks);

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

                var testedClass = new ProjectApi(freshbooks);

                var result = testedClass.Get(3);
                Assert.NotNull(result);
            }
        }


    }
}
