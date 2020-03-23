using Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Common {

  [TestClass]
  public class AuthorizationUrlRegexTests {

    private AuthorizationUrlRegex regex = new AuthorizationUrlRegex();

    [TestMethod]
    public void IsMatch_WhenAuthorizationUrlInvalid_ShouldFail(){
      var url = "";
      Assert.IsFalse(regex.IsMatch(url));
    }

    [TestMethod]
    public void IsMatch_WhenAuthorizationUrlValid_ShouldSucceed(){
      var url = "http://localhost:5000/connect/authorize?response_type=code&client_id={client-id}&redirect_uri={redirect_uri}&scope=openid offline_access&state={state}";
      Assert.IsTrue(regex.IsMatch(url));
    }
  }
}
