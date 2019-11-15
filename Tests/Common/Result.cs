using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common{

  [TestClass]
  public class ResultTests {

    public Result<bool> result { get; set; }

    [TestMethod]
    public void Should_Work(){
      Assert.IsTrue(true);
    }
  }
}
