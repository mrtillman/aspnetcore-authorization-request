using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Services;
using Tests.TestDoubles;
using Infrastructure;
using Domain;
using Common;

namespace Tests.Services
{
  [TestClass]
  public class SecureServiceTests
  {
    [TestInitialize]
    public void TestStartup()
    {
      var mockResponse = Mock.SetUp(response =>
      {
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(Stub.JSON.AuthorizationResponse);
        return response;
      });

      var mockHttpShim = Moq.Mock.Of<IHttpShim>(Moq.MockBehavior.Strict);
      
      Moq.Mock.Get(mockHttpShim)
         .Setup(http => http.FetchToken(Moq.It.IsAny<AuthorizationRequest>()))
         .Returns(Task.FromResult(mockResponse));

      Moq.Mock.Get(mockHttpShim)
         .Setup(http => http.RenewToken(Moq.It.IsAny<AuthorizationRequest>()))
         .Returns(Task.FromResult(mockResponse));
      
      secureService = new SecureService(Mock.Configuration, Mock.ServerUrls, mockHttpShim);
    }
    private SecureService secureService { get; set; }

    private readonly AuthorizationUrlRegex authUrlRegex = new AuthorizationUrlRegex();

    [TestMethod]
    public void Should_Get_AuthorizationUrl()
    {
      Assert.IsTrue(authUrlRegex.IsMatch(secureService.AuthorizationUrl));
    }

    [TestMethod]
    public async Task Should_Get_Token(){
      NameValueCollection querystring = HttpUtility.ParseQueryString(secureService.AuthorizationUrl);
      var state = querystring["state"];
      var AuthorizationResponse = await secureService.GetToken("code", state);
      Assert.IsNotNull(AuthorizationResponse.Value);
    }

    [TestMethod]
    public async Task Should_Renew_Token(){
      NameValueCollection querystring = HttpUtility.ParseQueryString(secureService.AuthorizationUrl);
      var state = querystring["state"];
      var AuthorizationResponse = await secureService.RenewToken("refr3sh-tok3n");
      Assert.IsNotNull(AuthorizationResponse.Value);
    }
  }
}
