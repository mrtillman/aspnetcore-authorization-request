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
    public GetCountersUseCase getCountersUseCase { get; set; }

    [TestMethod]
    public async Task Should_GetCounters() {
      var token = "TokenValue";
      var mockCountersService = Moq.Mock.Of<ICounterService>(Moq.MockBehavior.Strict);
      Result<List<Counter>> mockResult = Result<List<Counter>>.Ok(new List<Counter>());
      Moq.Mock.Get(mockCountersService).SetupSet(service => service.Token = token).Verifiable();
      Moq.Mock.Get(mockCountersService).SetupGet(service => service.Token).Returns(token);
      Moq.Mock.Get(mockCountersService)
          .Setup(service => service.GetCounters())
          .Returns(Task.FromResult(mockResult));
      getCountersUseCase = new GetCountersUseCase(mockCountersService);
      getCountersUseCase.Token = token;
      var result = await getCountersUseCase.Execute();
      Assert.IsTrue(result.DidSucceed);
    }
  }
}