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
      IServiceAgent ServiceAgent)
      : base(Configuration, ServiceAgent){ 
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
        querystring["scope"] = "openid offline_access";
        querystring["state"] = _state = Guid.NewGuid().ToString();
        var parameters = querystring.ToString();
        return $"{baseUrl}/connect/authorize?{parameters}";
      }
    }

    public async Task<Result<AuthorizationResponse>> GetToken(string code, string state)
    {
      if (state != _state)
      {
        return Result.Fail<AuthorizationResponse>("Forged Authorization Request");
      }

      var authRequest = new AuthorizationRequest(){
        code = code,
        redirectUri = configuration["REDIRECT_URI"].ToString(),
        clientId = configuration["CLIENT_ID"].ToString(),
        clientSecret = configuration["CLIENT_SECRET"].ToString(),
        scope = "openid",
        grantType = "authorization_code"
      };

      var response = await agent.FetchToken(authRequest);

      if (response.StatusCode != HttpStatusCode.OK)
      {
        return Result.Fail<AuthorizationResponse>(response.ReasonPhrase);
      }

      var AuthorizationResponse = await DeserializeResponseStringAs<AuthorizationResponse>(response);

      return Result.Ok(AuthorizationResponse);
    }

    public async Task<Result<AuthorizationResponse>> RenewToken(string refreshToken)
    {
      var authRequest = new AuthorizationRequest(){
        clientId = configuration["CLIENT_ID"].ToString(),
        clientSecret = configuration["CLIENT_SECRET"].ToString(),
        grantType = "refresh_token",
        refreshToken = refreshToken
      };

      var response = await agent.RenewToken(authRequest);

      if (response.StatusCode != HttpStatusCode.OK)
      {
        return Result.Fail<AuthorizationResponse>(response.ReasonPhrase);
      }

      var AuthorizationResponse = await DeserializeResponseStringAs<AuthorizationResponse>(response);

      return Result.Ok(AuthorizationResponse);
    }
  }
}
