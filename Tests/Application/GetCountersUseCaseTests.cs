using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Application;
using Common;
using Domain;
using Services;

namespace Tests.Application
{
  [TestClass]
  public class GetCountersUseCaseTests
  {
    public GetCountersUseCaseTests()
    {
        mockCounterService = new Mock<ICounterService>();
        Result<List<Counter>> mockResult = Result.Ok(new List<Counter>());
        mockCounterService.SetupSet(service => service.Token = TestDoubles.Token)
                          .Verifiable();
        mockCounterService.SetupGet(service => service.Token)
                          .Returns(TestDoubles.Token)
                          .Verifiable();
        mockCounterService.Setup(service => service.GetCounters())
                          .Returns(Task.FromResult(mockResult));
    }
    private Mock<ICounterService> mockCounterService { get; set; }
    private GetCountersUseCase getCountersUseCase { get; set; }

    [TestMethod]
    public async Task Execute_Should_GetCounters() {
      getCountersUseCase = new GetCountersUseCase(mockCounterService.Object);
      getCountersUseCase.Token = TestDoubles.Token;

      await getCountersUseCase.Execute();
      
      mockCounterService.Verify(service => service.GetCounters(), Times.Once);
    }

    [TestMethod]
    public void Token_Should_GetToken() {
      getCountersUseCase = new GetCountersUseCase(mockCounterService.Object);
      
      var token = getCountersUseCase.Token;
      
      mockCounterService.Verify(service => service.Token, Times.Once);
    }
  }
}
