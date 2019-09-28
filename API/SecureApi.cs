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

public class SecureApi : BaseApi {

  public SecureApi (
    IConfiguration Configuration,
    IServerUrls ServerUrls,
    HttpClient Client) 
    : base(Configuration, Client) { 
      serverUrls = ServerUrls;
  }

  private IServerUrls serverUrls { get; set; }
  
  private static string _state { get; set; }

  public string AuthorizationUrl { 
    get {
      var baseUrl = serverUrls.SECURE;
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

  public async Task<Result<AuthResponse>> GetToken(string code, string state) {
    if(state != _state){
      return Result<AuthResponse>.Fail("Forged Authorization Request");
    }
    var keyValues = new List<KeyValuePair<string, string>>();
    var requestUri = $"{serverUrls.SECURE}/connect/token";
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

    if(response.StatusCode != HttpStatusCode.OK){
      return Result<AuthResponse>.Fail(response.ReasonPhrase);
    }

    var authResponse = await DeserializeResponseStringAs<AuthResponse>(response);

    return Result<AuthResponse>.Ok(authResponse);
  }

}
