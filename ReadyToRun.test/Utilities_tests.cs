
using System;
using System.Collections.Generic;
using XLSTAT.Utilitties;
using Xunit;

namespace ReadyToRun.test
{
    public class Utilities_tests
    {
        [Fact]
        public void TestBase26Encode()
        {
            Assert.False(Utilities.Base26Encode(0) != "A", "Base26Encode error");
            Assert.False(Utilities.Base26Encode(1) != "B", "Base26Encode error");
            Assert.False(Utilities.Base26Encode(2) != "C", "Base26Encode error");
            Assert.False(Utilities.Base26Encode(3) != "D", "Base26Encode error");
            Assert.False(Utilities.Base26Encode(26) != "AA", "Base26Encode error");
        }

        [Fact]
        public void TestGetUniqueStringID()
        {
            string res = Utilities.GetUniqueStringID();

            Assert.False(res == "", "GetUniqueStringID error");
        }
    }
}
