using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Services;
using Tests.TestDoubles;
using Infrastructure;

namespace Tests.Services
{
  [TestClass]
  public class CountersServiceTests
  {
    private CountersService service { get; set; }

    [TestMethod]
    public async Task Should_Get_Counters()
    {
      var mockResponse = Mock.SetUp(response =>
      {
        response.StatusCode = HttpStatusCode.OK;
        response.Content = new StringContent(Stub.JSON.Counters);
        return response;
      });
      
      var mockServiceAgent = Moq.Mock.Of<IServiceAgent>();

      Moq.Mock.Get(mockServiceAgent)
         .Setup(agent => agent.FetchCounters())
         .Returns(Task.FromResult(mockResponse));
      
      service = new CountersService(Mock.Configuration, mockServiceAgent);

      var result = await service.GetCounters();

      Assert.IsTrue(result.DidSucceed);
    }
  }
}
