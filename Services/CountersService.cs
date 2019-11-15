using System.Net;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Domain;
using Infrastructure;

namespace Services
{
  public class CountersService : BaseService, ICountersService
  {

    public CountersService(
      IConfiguration Configuration, IHttpShim HttpShim)
      : base(Configuration, HttpShim) { }

    public string Token { 
      get => http.Token;
      set { http.Token = value; } 
    }

    public async Task<Result<List<Counter>>> GetCounters()
    {
      var response = await http.FetchCounters();

      if (response.StatusCode != HttpStatusCode.OK)
      {
        return Result<List<Counter>>.Fail(response.ReasonPhrase);
      }

      var countersResponse = await DeserializeResponseStringAs<List<Counter>>(response);

      return Result<List<Counter>>.Ok(countersResponse);
    }

  }
}