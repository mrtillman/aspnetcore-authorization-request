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
  
  public async Task<string> GetCounters(string token) {
      var baseUrl = serverUrls.API;
      var requestUri = $"{baseUrl}/v1/counters";
      client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
      var response = await client.GetAsync(requestUri);
      return await response.Content.ReadAsStringAsync();
  }
}
