using System;
using System.Collections.Generic;
using Xunit;

namespace SimpleDB.Tests.Document
{
    public class Bson_Type_Tests
    {
        [Fact]
        public void ViewTypes()
        {
            int a = 0b1100;
            int b = 0b1010;
            int result = a & b; // result = 0b1000

            var a1 = 0b1100;
            var b1 = 0b1100;
            var c1 = 0b10101;
            var c2 = c1 << 2;
            var c3 = c1 >> 2;

            var randomString = "jaison blah blah".GetHashCode();
            var msg = randomString >> 24;
            var msg1 = randomString >> 16;
            var msg23 = randomString >> 8;

        }
    }
}