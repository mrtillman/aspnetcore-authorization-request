using System.Net;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Domain;
using Infrastructure;

namespace Services
{
  public class CoreApi : BaseApi, ICoreApi
  {

    public CoreApi(
      IConfiguration Configuration,
      IServerUrls ServerUrls,
      IHttpShim HttpShim)
      : base(Configuration, HttpShim)
    {
      serverUrls = ServerUrls;
    }

    private IServerUrls serverUrls { get; set; }

    public string Token { 
      get => httpShim.Token;
      set { httpShim.Token = value; } 
    }

    public async Task<Result<List<Counter>>> GetCounters()
    {
      
      httpShim.BaseURL = serverUrls.API;

      var response = await httpShim.Get("v1/counters");

      if (response.StatusCode != HttpStatusCode.OK)
      {
        return Result<List<Counter>>.Fail(response.ReasonPhrase);
      }

      var countersResponse = await DeserializeResponseStringAs<List<Counter>>(response);

      return Result<List<Counter>>.Ok(countersResponse);
    }

  }
}