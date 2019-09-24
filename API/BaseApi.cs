using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;

public class BaseApi {

  public BaseApi (IConfiguration Configuration, HttpClient Client) {
    configuration = Configuration;
    client = Client;
  }

  protected IConfiguration configuration { get; set; }
  protected HttpClient client { get; set; }

}