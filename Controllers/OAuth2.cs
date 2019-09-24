using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using AuthDemo.Constants;
using Microsoft.AspNetCore.Mvc;

public class OAuth2Controller : Controller
{

    public OAuth2Controller(SecureApi SecureApi, CoreApi CoreApi)
    {
        secureApi = SecureApi;
        coreApi = CoreApi;
    }

    public SecureApi secureApi { get; set; }
    public CoreApi coreApi { get; set; }
    public async Task<string> Callback()
    {
        // 2. & 3. Authorization Grant
        var code = Request.Query["code"];
        var state = Request.Query["state"];

        // 4. & 5. Access Token
        var token = await secureApi.GetToken(code, state);

        // 6. Protected Resource
        return await coreApi.GetCounters(token);
    }

    

}