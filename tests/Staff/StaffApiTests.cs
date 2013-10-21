using System;
using Vooban.FreshBooks.DotNet.Api.Staff;
using Xunit;

namespace Vooban.FreshBooks.DotNet.Api.Tests.Staff
{    
    public class StaffApiTests
    {
        [Fact]
        public void GetAll_Works_AsExpected()
        {
            var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks("", ""));
            
            var testedClass = new StaffApi(freshbooks);

            var result = testedClass.CallGetList();
            Assert.Equal(1, result.TotalItems);
        }
    }
}
