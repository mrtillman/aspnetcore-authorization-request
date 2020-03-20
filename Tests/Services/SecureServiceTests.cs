using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Services;
using Tests;
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
      var mockResponse = mockAuthorizationResponse();

      var mockServiceAgent = Moq.Mock.Of<IServiceAgent>(Moq.MockBehavior.Strict);
      
      Moq.Mock.Get(mockServiceAgent)
         .Setup(agent => agent.FetchToken(Moq.It.IsAny<AuthorizationRequest>()))
         .Returns(Task.FromResult(mockResponse));

      Moq.Mock.Get(mockServiceAgent)
         .Setup(agent => agent.RenewToken(Moq.It.IsAny<AuthorizationRequest>()))
         .Returns(Task.FromResult(mockResponse));
      
      secureService = new SecureService(TestDoubles.Configuration, TestDoubles.ServerUrls, mockServiceAgent);
    }
    private SecureService secureService { get; set; }

    private readonly AuthorizationUrlRegex authUrlRegex = new AuthorizationUrlRegex();

    [TestMethod]
    public void Should_GetAuthorizationUrl()
    {
      Assert.IsTrue(authUrlRegex.IsMatch(secureService.AuthorizationUrl));
    }

    [TestMethod]
    public async Task Should_GetToken(){
      NameValueCollection querystring = HttpUtility.ParseQueryString(secureService.AuthorizationUrl);
      var state = querystring["state"];
      var AuthorizationResponse = await secureService.GetToken("code", state);
      Assert.IsNotNull(AuthorizationResponse.Value);
    }

    [TestMethod]
    public async Task Should_RenewToken(){
      var AuthorizationResponse = await secureService.RenewToken("refr3sh-tok3n");
      Assert.IsNotNull(AuthorizationResponse.Value);
    }

    private HttpResponseMessage mockAuthorizationResponse(){
      var authorizationResponse = "{\"id_token\":\"id_token\", \"access_token\":\"access_token\", \"expires_in\":86400, \"token_type\":\"Bearer\",  \"scope\":\"openid\"}";
      var response = Moq.Mock.Of<HttpResponseMessage>(Moq.MockBehavior.Strict);
      response.StatusCode = HttpStatusCode.OK;
      response.Content = new StringContent(authorizationResponse);
      return response;
    }
  }
}
