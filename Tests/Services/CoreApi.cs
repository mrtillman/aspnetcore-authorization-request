using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Services;
using Tests.TestDoubles;

namespace Tests.Services
{
  [TestClass]
  public class CoreApiTests
  {
    private CoreApi coreApi { get; set; }

    [TestMethod]
    public async Task Should_Get_Counters()
    {
      var mockResponse = Mock.SetUp(response =>
      {
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(Stub.JSON.Counters);
        return response;
      });

      var mockHandler = Mock.HttpMessageHandler(mockResponse);

      var httpClient = new HttpClient(mockHandler.Object);

      coreApi = new CoreApi(Mock.Configuration, Mock.ServerUrls, httpClient);

      var result = await coreApi.GetCounters();

      Assert.IsTrue(result.DidSucceed);
    }
  }
}
