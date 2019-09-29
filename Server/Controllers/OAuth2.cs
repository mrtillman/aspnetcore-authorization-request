using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AuthDemo.API;
using AuthDemo.Models;
public class OAuth2Controller : Controller
{

  public OAuth2Controller(SecureApi SecureApi, CoreApi CoreApi)
  {
    secureApi = SecureApi;
    coreApi = CoreApi;
  }

  private SecureApi secureApi { get; set; }

  private CoreApi coreApi { get; set; }

  // 2. Authorization Grant
  public async Task<ActionResult> Callback()
  {
    // 3. Authorization Grant
    var code = Request.Query["code"];
    var state = Request.Query["state"];

    Result<AuthResponse> authResult = await secureApi.GetToken(code, state);

    if (authResult.DidFail)
    {
      return Unauthorized(authResult.ErrorMessage);
    }

    // 4. Access Token
    coreApi.Token = authResult.Value.access_token;

    // 5. Access Token
    Result<List<Counter>> countersResult = await coreApi.GetCounters();

    if (countersResult.DidFail)
    {
      return BadRequest(countersResult.ErrorMessage);
    }

    // 6. Protected Resource
    return Ok(countersResult.Value);

  }

}