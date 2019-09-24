using System;
using System.Web;
using System.Net.Http;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using AuthDemo.Constants;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Threading.Tasks;

public class SecureApi : BaseApi {

  public SecureApi (IConfiguration Configuration, HttpClient Client) 
    : base(Configuration, Client) { }

  private static string _state { get; set; }
  
  public string AuthorizationUrl { 
    get {
      var baseUrl = ServerUrls.SECURE[ENV.DEV];
      var client_id = configuration["CLIENT_ID"].ToString();
      var redirect_uri = configuration["REDIRECT_URI"].ToString();
      NameValueCollection querystring = HttpUtility.ParseQueryString(string.Empty);
      querystring["response_type"] = "code";
      querystring["client_id"] = client_id;
      querystring["redirect_uri"] = redirect_uri;
      querystring["scope"] = "openid";
      querystring["state"] = _state = Guid.NewGuid().ToString();
      var parameters = querystring.ToString();
      return $"{baseUrl}/connect/authorize?{parameters}";
    }
  }

  public async Task<string> GetToken(string code, string state) {
      if(state != _state){
          throw new Exception("Forged Authorization Request");
      }
      var keyValues = new List<KeyValuePair<string, string>>();
      var baseUrl = ServerUrls.SECURE[ENV.DEV];
      var requestUri = $"{baseUrl}/connect/token";
      var redirect_uri = configuration["REDIRECT_URI"].ToString();
      var client_id = configuration["CLIENT_ID"].ToString();
      var client_secret = configuration["CLIENT_SECRET"].ToString();
      keyValues.Add(new KeyValuePair<string, string>("code", code));
      keyValues.Add(new KeyValuePair<string, string>("redirect_uri", redirect_uri));
      keyValues.Add(new KeyValuePair<string, string>("client_id", client_id));
      keyValues.Add(new KeyValuePair<string, string>("client_secret", client_secret));
      keyValues.Add(new KeyValuePair<string, string>("scope", "openid"));
      keyValues.Add(new KeyValuePair<string, string>("grant_type", "authorization_code"));
      var content = new FormUrlEncodedContent(keyValues);
      var response = await client.PostAsync(requestUri, content);
      var authResponseJson = await response.Content.ReadAsStringAsync();
      var authResponse = JsonConvert.DeserializeObject<AuthResponse>(authResponseJson);
      return authResponse.access_token;
  }
}
