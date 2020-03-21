using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Services;
using Moq;
using Infrastructure;

namespace Tests.Services
{
  [TestClass]
  public class CounterServiceTests
  {
    private CounterService service { get; set; }

    [TestMethod]
    public async Task Should_GetCounters()
    {
      var mockResponse = mockCountersResponse();
      
      var mockServiceAgent = Mock.Of<IServiceAgent>();

      Mock.Get(mockServiceAgent)
         .Setup(agent => agent.FetchCounters())
         .Returns(Task.FromResult(mockResponse));
      
      service = new CounterService(TestDoubles.Configuration, mockServiceAgent);

      var result = await service.GetCounters();

      Assert.IsTrue(result.DidSucceed);
    }

    private HttpResponseMessage mockCountersResponse(){
      var counters = "[{\"_id\":\"5d16c0cd11ee4a3d6f44b045\",\"name\":\"alcohol\",\"value\":0,\"skip\":1,\"__v\":0},{\"_id\":\"5d16c0cd11ee4a3d6f44b046\",\"name\":\"tobacco\",\"value\":0,\"skip\":1,\"__v\":0},{\"_id\":\"5d16c0cd11ee4a3d6f44b047\",\"name\":\"firearms\",\"value\":0,\"skip\":1,\"__v\":0}]";
      var response = Mock.Of<HttpResponseMessage>();
      response.StatusCode = HttpStatusCode.OK;
      response.Content = new StringContent(counters);
      return response;
    }
  }
}
