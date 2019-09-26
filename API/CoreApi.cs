using System;
using System.Web;
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
  
  public async Task<string> GetCounters() {
      var baseUrl = serverUrls.API;
      var requestUri = $"{baseUrl}/v1/counters";
      client.DefaultRequestHeaders.Add("Authorization", $"bearer {Token}");
      var response = await client.GetAsync(requestUri);
      return await response.Content.ReadAsStringAsync();
  }
}
