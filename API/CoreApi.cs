using System;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using AuthDemo.Constants;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class CoreApi : BaseApi {

  public CoreApi (
    IConfiguration Configuration,
    IServerUrls ServerUrls,
    HttpClient Client)
    : base(Configuration, Client) { 
      serverUrls = ServerUrls;
  }

  private IServerUrls serverUrls { get; set; }

  public string Token { get; set; }
  
  public async Task<Result<List<Counter>>> GetCounters() {
    var requestUri = $"{serverUrls.API}/v1/counters";
    client.DefaultRequestHeaders.Add("authorization", $"bearer {Token}");
    var response = await client.GetAsync(requestUri);

    if(response.StatusCode != HttpStatusCode.OK){
      return Result<List<Counter>>.Fail(response.ReasonPhrase);
    }

    var countersResponse = await DeserializeResponseStringAs<List<Counter>>(response);

    return Result<List<Counter>>.Ok(countersResponse);
  }
}
