using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Infrastructure {
  public class HttpShim : IHttpShim {
    public HttpShim(HttpClient Client)
    {
        client = Client;
    }

    private HttpClient client { get; set; }
    private Uri baseUri { get; set; }
    public string BaseURL { 
      get { return baseUri.ToString(); }
      set { baseUri = new Uri(value); }
     }
    private string token;
    public string Token { 
      get => token;
      set { 
        token = value;
        client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
      } 
    }
    public async Task<HttpResponseMessage> FetchCounters(string Path)
    {
      var resourceUri = new Uri(baseUri, Path);
      return await client.GetAsync(resourceUri.ToString());
    }

    public async Task<HttpResponseMessage> FetchToken(string Path, HttpContent Content)
    {
      var resourceUri = new Uri(baseUri, Path);
      return await client.PostAsync(resourceUri.ToString(), Content);
    }
  }

}
