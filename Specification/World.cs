using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit.Gherkin.Quick;
using Moq;
using Domain;
using Common;
using Services;
using Tests;

namespace Specification {
  public abstract class World : Feature, IDisposable
  {
      public static ICounterService MockCounterService() {
          var countersResult = Result.Ok(new List<Counter>());
          var mockCounterService = Mock.Of<ICounterService>();
            Mock.Get(mockCounterService).SetupSet(service => service.Token = TestDoubles.Token).Verifiable();
            Mock.Get(mockCounterService).SetupGet(service => service.Token).Returns(TestDoubles.Token);
            Mock.Get(mockCounterService)
                .Setup(service => service.GetCounters())
                .Returns(Task.FromResult(countersResult));
            return mockCounterService;
      }
      public void Dispose()
      {
          // Do "global" teardown here; Called after every test method.
      }

  }
}