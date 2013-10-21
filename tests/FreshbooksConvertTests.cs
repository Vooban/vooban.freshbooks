using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Vooban.FreshBooks.DotNet.Api.Tests
{
    public class FreshbooksConvertTests
    {
        public class ToBoolean
        {
             [Fact]
             public void NullValueReturnsFalse()
             {
                 Assert.False(FreshbooksConvert.ToBoolean(null));
             }
        }
    }
}
