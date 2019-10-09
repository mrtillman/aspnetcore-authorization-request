using System;
using AuthDemo.TestDoubles;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AuthDemo.API;
using System.Net;
using System.Net.Http;

namespace AuthDemo.Tests
{
  [TestClass]
  public class SecureApiTests
  {
    [TestInitialize]
    public void initSecureApi()
    {
      var mockResponse = Mock.SetUp(response =>
      {
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(Stub.JSON.Counters);
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
  }
}
