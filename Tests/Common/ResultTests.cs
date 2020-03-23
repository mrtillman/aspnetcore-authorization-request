using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common{

  [TestClass]
  public class ResultTests {

    public Result<bool> result { get; set; }

    [TestMethod]
    public void Ok_Should_Succeed(){
      var result = Result.Ok(true);

      Assert.IsTrue(result.DidSucceed);
    }

    [TestMethod]
    public void Ok_Should_HaveValue(){
      var result = Result.Ok(true);

      Assert.IsTrue(result.Value);
    }

    [TestMethod]
    public void Fail_Should_Fail(){
      var result = Result.Fail<bool>("no luck");
      
      Assert.IsTrue(result.DidFail);
    }

    [TestMethod]
    public void Fail_Should_HaveErrorMessage(){
      var result = Result.Fail<bool>("try again");
      
      Assert.IsNotNull(result.ErrorMessage);
    }
  }
}
