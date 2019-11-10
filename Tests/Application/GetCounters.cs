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
  public class GetCountersTests
  {
    public GetCountersUseCase getCountersUseCase { get; set; }

    [TestMethod]
    public async Task Should_Get_Counters() {
      var mockCoreApi = Mock.Of<ICoreApi>();
      Result<List<Counter>> mockResult = Result<List<Counter>>.Ok(new List<Counter>());
      Mock.Get(mockCoreApi)
          .Setup(api => api.GetCounters())
          .Returns(Task.FromResult(mockResult));
      getCountersUseCase = new GetCountersUseCase(mockCoreApi);
      var result = await getCountersUseCase.Execute();
      Assert.IsTrue(result.DidSucceed);
    }
  }
}