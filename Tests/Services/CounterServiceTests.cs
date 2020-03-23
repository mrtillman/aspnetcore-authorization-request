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
    public async Task GetCounters_Should_Succeed()
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
      var response = Mock.Of<HttpResponseMessage>();
      response.StatusCode = HttpStatusCode.OK;
      response.Content = new StringContent("[]");
      return response;
    }
  }
}
