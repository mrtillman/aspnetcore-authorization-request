using System;
using System.Web;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Specialized;
using Common;
using Domain;

namespace Infrastructure {
  public class ServiceAgent : IServiceAgent {
    public ServiceAgent(HttpClient Client, IServerUrls ServerUrls)
    {
        client = Client;
        serverUrls = ServerUrls;
    }

    private HttpClient client { get; set; }
    private IServerUrls serverUrls { get; set; }
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
    public async Task<HttpResponseMessage> FetchCounters()
    {
      return await client.GetAsync($"{this.serverUrls.API}/v1/counters");
    }

    public async Task<HttpResponseMessage> FetchToken(AuthorizationRequest authRequest)
    {
      NameValueCollection querystring = HttpUtility.ParseQueryString(string.Empty);
      var keyValues = new List<KeyValuePair<string, string>>();
      keyValues.Add(new KeyValuePair<string, string>("code", authRequest.code));
      keyValues.Add(new KeyValuePair<string, string>("redirect_uri", authRequest.redirectUri));
      keyValues.Add(new KeyValuePair<string, string>("client_id", authRequest.clientId));
      keyValues.Add(new KeyValuePair<string, string>("client_secret", authRequest.clientSecret));
      keyValues.Add(new KeyValuePair<string, string>("scope", authRequest.scope));
      keyValues.Add(new KeyValuePair<string, string>("grant_type", authRequest.grantType));
      var requestParameters = new FormUrlEncodedContent(keyValues);
      return await client.PostAsync($"{serverUrls.SECURE}/connect/token", requestParameters);
    }

    public async Task<HttpResponseMessage> RenewToken(AuthorizationRequest authRequest)
    {
      NameValueCollection querystring = HttpUtility.ParseQueryString(string.Empty);
      var keyValues = new List<KeyValuePair<string, string>>();
      keyValues.Add(new KeyValuePair<string, string>("client_id", authRequest.clientId));
      keyValues.Add(new KeyValuePair<string, string>("client_secret", authRequest.clientSecret));
      keyValues.Add(new KeyValuePair<string, string>("grant_type", authRequest.grantType));
      keyValues.Add(new KeyValuePair<string, string>("refresh_token", authRequest.refreshToken));
      var requestParameters = new FormUrlEncodedContent(keyValues);
      return await client.PostAsync($"{serverUrls.SECURE}/connect/token", requestParameters);
    }
  }

}
