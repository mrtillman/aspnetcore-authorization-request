using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Services;
using Tests;
using Infrastructure;

namespace Tests.Services
{
  [TestClass]
  public class CountersServiceTests
  {
    private CounterService service { get; set; }

    [TestMethod]
    public async Task Should_GetCounters()
    {
      var mockResponse = Mock.SetUp(response =>
      {
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(TestDoubles.Counters);
        return response;
      });
      
      var mockServiceAgent = Moq.Mock.Of<IServiceAgent>();

      Moq.Mock.Get(mockServiceAgent)
         .Setup(agent => agent.FetchCounters())
         .Returns(Task.FromResult(mockResponse));
      
      service = new CounterService(TestDoubles.Configuration, mockServiceAgent);

      var result = await service.GetCounters();

      Assert.IsTrue(result.DidSucceed);
    }
  }
}
