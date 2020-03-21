using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit.Gherkin.Quick;
using Moq;
using Domain;
using Common;
using Services;

namespace Specification {

  public abstract class World : Feature, IDisposable
  {
      
      public static readonly string _token = "TokenValue";

      public static ICounterService MockCounterService() {
          var countersResult = Result.Ok(new List<Counter>());
          var mockCounterService = Mock.Of<ICounterService>();
            Mock.Get(mockCounterService).SetupSet(service => service.Token = _token).Verifiable();
            Mock.Get(mockCounterService).SetupGet(service => service.Token).Returns(_token);
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