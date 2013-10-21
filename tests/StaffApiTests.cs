using System;
using Xunit;

namespace Vooban.FreshBooks.DotNet.Api.Tests
{    
    public class StaffApiTests
    {
        [Fact]
        public void GetAll_Works_AsExpected()
        {
            var freshbooks = new Lazy<HastyAPI.FreshBooks.FreshBooks>(() => new HastyAPI.FreshBooks.FreshBooks("", ""));
            
            var testedClass = new StaffApi(freshbooks);

            var result = testedClass.GetMultiple();
            Assert.Equal(1, result.TotalItems);
        }
    }
}
