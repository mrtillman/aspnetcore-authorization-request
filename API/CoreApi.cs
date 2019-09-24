using System;
using System.Web;
using System.Net.Http;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using AuthDemo.Constants;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class CoreApi {

  public CoreApi (IConfiguration Configuration, HttpClient Client) {
    configuration = Configuration;
    client = Client;
  }
  private IConfiguration configuration { get; set; }
  private HttpClient client { get; set; }
  public async Task<string> GetCounters(string token) {
      var baseUrl = ServerUrls.API[ENV.DEV];
      var requestUri = $"{baseUrl}/v1/counters";
      client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
      var response = await client.GetAsync(requestUri);
      return await response.Content.ReadAsStringAsync();
  }
}
