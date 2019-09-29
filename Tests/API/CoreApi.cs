using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuthDemo.API;

namespace AuthDemo.Tests
{
    [TestClass]
    public class CoreApiTests
    {
        public CoreApiTests(){}

        private CoreApi coreApi { get; set; }

        [TestMethod]
        public void Should_Work()
        {
            Assert.IsTrue(true);
        }
    }
}
