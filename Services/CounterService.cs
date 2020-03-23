using System.Net;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Domain;
using Infrastructure;

namespace Services
{
  public class CounterService : BaseService, ICounterService
  {

    public CounterService(
      IConfiguration Configuration, IServiceAgent ServiceAgent)
      : base(Configuration, ServiceAgent) { }

    public string Token { 
      get => agent.Token;
      set { agent.Token = value; } 
    }

    public async Task<Result<List<Counter>>> GetCounters()
    {
      var response = await agent.FetchCounters();

      if (response.StatusCode != HttpStatusCode.OK)
      {
        return Result.Fail<List<Counter>>(response.ReasonPhrase);
      }

      var countersResponse = await DeserializeResponseStringAs<List<Counter>>(response);

      return Result.Ok(countersResponse);
    }

  }
}