using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;

public class BaseApi {

  public BaseApi (IConfiguration Configuration, HttpClient Client) {
    configuration = Configuration;
    client = Client;
  }

  protected IConfiguration configuration { get; set; }
  protected HttpClient client { get; set; }

  protected async Task<T> DeserializeResponseStringAs<T>(HttpResponseMessage response){
    var responseJson = await response.Content.ReadAsStringAsync();
    return JsonConvert.DeserializeObject<T>(responseJson);
  }

}