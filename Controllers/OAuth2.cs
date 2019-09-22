using Microsoft.Extensions.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;

public class OAuth2Controller : BaseController
{

    public OAuth2Controller(IConfiguration Configuration)
        :base(Configuration) { }

    public string Callback()
    {
        var code = Request.Query["code"];
        return code;
    }

    // private void getToken(string code) {
    //     var keyValues = new List<KeyValuePair<string, string>>();
    //     /*
    //      code,
    //     redirect_uri,
    //     client_id,
    //     client_secret,
    //     scope: 'openid',
    //     grant_type: 'authorization_code',
    //     */
    //     keyValues.Add(new KeyValuePair<string, string>("code", code));
    //     var content = new FormUrlEncodedContent(keyValues);
    //     client.PostAsync("requestUri", content);
    // }

}