using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuthDemo.API;

namespace AuthDemo.Tests
{
    [TestClass]
    public class SecureApiTests
    {
        public SecureApiTests(){}

        private SecureApi secureApi { get; set; }

        [TestMethod]
        public void Should_Work()
        {
            Assert.IsTrue(true);
        }
    }
}
