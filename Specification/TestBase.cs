using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Xunit.Gherkin.Quick;
using Moq;
using Domain;
using Common;
using Services;

namespace Specification {

  public abstract class TestBase : Feature, IDisposable
  {
      
      public static readonly string _token = "TokenValue";

      public static ICountersService MockCountersService() {
          var countersResult = Result<List<Counter>>.Ok(new List<Counter>());
          var mockCountersService = Mock.Of<ICountersService>();
            Mock.Get(mockCountersService).SetupSet(service => service.Token = _token).Verifiable();
            Mock.Get(mockCountersService).SetupGet(service => service.Token).Returns(_token);
            Mock.Get(mockCountersService)
                .Setup(service => service.GetCounters())
                .Returns(Task.FromResult(countersResult));
            return mockCountersService;
      }
      public void Dispose()
      {
          // Do "global" teardown here; Called after every test method.
      }

  }
}
