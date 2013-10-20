using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HastyAPI.FreshBooks.Wrapper.Tests
{    
    public class StaffApiTests
    {
        [Fact]
        public void GetAll_Works_AsExpected()
        {
            var freshbooks = new Lazy<FreshBooks>(() => new FreshBooks("", ""));
            
            var testedClass = new StaffApi(freshbooks);

            var result = testedClass.GetMultiple();
            Assert.Equal(1, result.TotalItems);
        }
    }
}
