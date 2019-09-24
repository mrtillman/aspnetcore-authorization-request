using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using AuthDemo.Constants;
using Newtonsoft.Json;

public class OAuth2Controller : BaseController
{

    public OAuth2Controller(IConfiguration Configuration)
        :base(Configuration) { }

    public async Task<string> Callback()
    {
        // 2. & 3. Authorization Grant
        var code = Request.Query["code"];
        var state = Request.Query["state"];
        var token = await getToken(code, state);
        return await getCounters(token);
    }

    private async Task<string> getToken(string code, string state) {
        
        // 4. & 5. Access Token
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

    private async Task<string> getCounters(string token) {
        
        // 6. Protected Resource
        var baseUrl = ServerUrls.API[ENV.DEV];
        var requestUri = $"{baseUrl}/v1/counters";
        client.DefaultRequestHeaders.Add("Authorization", $"bearer {token}");
        var response = await client.GetAsync(requestUri);
        return await response.Content.ReadAsStringAsync();
    }

}