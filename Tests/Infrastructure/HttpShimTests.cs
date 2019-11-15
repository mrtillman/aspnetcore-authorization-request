using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Infrastructure;
using Tests.TestDoubles;
using Domain;

namespace Tests.Infrastructure {
  
  [TestClass]
  public class HttpShimTests
  {
    public HttpShim http { get; set; }

    private readonly HttpResponseMessage mockResponse = Mock.SetUp(res => {
      res.StatusCode = HttpStatusCode.OK;
      return res;
    });

    [TestMethod]
    public async Task Should_Perform_HttpGet(){
      var mockHandler = Mock.HttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      http = new HttpShim(client, Mock.ServerUrls);
      var response = await http.FetchCounters();
      Assert.IsTrue(response.IsSuccessStatusCode);
    }

    [TestMethod]
    public async Task Should_Perform_HttpPost(){
      var mockHandler = Mock.HttpMessageHandler(mockResponse);
      var client = new HttpClient(mockHandler.Object);
      http = new HttpShim(client,Mock.ServerUrls);
      var response = await http.FetchToken(new AuthorizationRequest());
      Assert.IsTrue(response.IsSuccessStatusCode);
    }
  }
}
