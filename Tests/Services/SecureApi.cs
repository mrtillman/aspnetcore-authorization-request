using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Collections.Specialized;
using System.Threading.Tasks;
using Services;
using Tests.TestDoubles;

namespace Tests.Services
{
  [TestClass]
  public class SecureApiTests
  {
    [TestInitialize]
    public void TestStartup()
    {
      var mockResponse = Mock.SetUp(response =>
      {
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(Stub.JSON.AuthResponse);
        return response;
      });

      var mockHandler = Mock.HttpMessageHandler(mockResponse);

      var httpClient = new HttpClient(mockHandler.Object);
      secureApi = new SecureApi(Mock.Configuration, Mock.ServerUrls, httpClient);
    }
    private SecureApi secureApi { get; set; }

    [TestMethod]
    public void Should_Get_AuthorizationUrl()
    {
      Assert.IsTrue(authUrlRegex.IsMatch(secureApi.AuthorizationUrl));
    }

    [TestMethod]
    public async Task Should_Get_Token(){
      NameValueCollection querystring = HttpUtility.ParseQueryString(secureApi.AuthorizationUrl);
      var state = querystring["state"];
      var authResponse = await secureApi.GetToken("code", state);
      Assert.IsNotNull(authResponse.Value.access_token);
    }

    #region authUrlRegex
    private Regex authUrlRegex
    {
      get => new Regex($"^((http|https)://({hostNames})/connect/authorize\\?{requestParameters})(.*)$");
    }

    private string hostNames
    {
      get => String.Join("|", new String[]{
                "localhost:5000",
                "secure.counter-culture.io",
                "counter-culture:5000"
            });
    }
    private string requestParameters
    {
      get => String.Join("", new String[]{
                "(?=.*(response_type=(.*)))",
                "(?=.*(&client_id=(.*)))",
                "(?=.*(&redirect_uri=(.*)))",
                "(?=.*(&scope=(.*)))",
                "(?=.*(&state=(.*)))",
            });
    }
    #endregion
  }
}
