using NUnit.Framework;
using System.Collections.Generic;

namespace ZhuoYue.Components.Core.NTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var s1 = new string[] { "1", "2" };
            var r1 = StringHelper.Join(",", s1);
            Assert.True(r1 == "1,2");

            var s2 = new List<string>() { "1", "2" };
            var r2 = StringHelper.Join(",", s2);
            Assert.True(r2 == "1,2");


            var s3 = new List<string>() { "1", "2" };
            var r3 = StringHelper.Join(",", s3, 4, null, "6");
            Assert.True(r3 == "1,2,4,6");

            var s4 = new List<object>() { null, null };
            var r4 = StringHelper.Join(",", s4);
            Assert.True(r3 == "1,2,4,6");

        }
    }
}