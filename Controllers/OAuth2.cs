using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using AuthDemo.Constants;
public class OAuth2Controller : BaseController
{

    public OAuth2Controller(IConfiguration Configuration)
        :base(Configuration) { }

    public async Task<string> Callback()
    {
        var code = Request.Query["code"];
        return await getToken(code);
    }

    private async Task<string> getToken(string code) {
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
        return await response.Content.ReadAsStringAsync();
    }

}