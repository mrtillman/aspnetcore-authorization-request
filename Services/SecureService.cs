using System;
using System.Web;
using System.Net;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using Common;
using Domain;
using Infrastructure;

namespace Services
{
  public class SecureService : BaseService, ISecureService
  {

    public SecureService(
      IConfiguration Configuration,
      IServerUrls ServerUrls,
      IHttpShim HttpShim)
      : base(Configuration, HttpShim){ 
        serverUrls = ServerUrls;
      }

    private IServerUrls serverUrls { get; set; }

    private static string _state { get; set; }

    public string AuthorizationUrl
    {
      get
      {
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

    public async Task<Result<AuthorizationResponse>> GetToken(string code, string state)
    {
      if (state != _state)
      {
        return Result<AuthorizationResponse>.Fail("Forged Authorization Request");
      }

      var authRequest = new AuthorizationRequest(){
        code = code,
        redirectUri = configuration["REDIRECT_URI"].ToString(),
        clientId = configuration["CLIENT_ID"].ToString(),
        clientSecret = configuration["CLIENT_SECRET"].ToString(),
        scope = "openid",
        grantType = "authorization_code"
      };

      var response = await httpShim.FetchToken(authRequest);

      if (response.StatusCode != HttpStatusCode.OK)
      {
        return Result<AuthorizationResponse>.Fail(response.ReasonPhrase);
      }

      var AuthorizationResponse = await DeserializeResponseStringAs<AuthorizationResponse>(response);

      return Result<AuthorizationResponse>.Ok(AuthorizationResponse);
    }
  }
}
