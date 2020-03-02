using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application;
using Services;

public class OAuth2Controller : Controller
{

  public OAuth2Controller(
    GetTokenUseCase GetTokenUseCase, 
    GetCountersUseCase GetCountersUseCase,
    RenewTokenUseCase RenewTokenUseCase)
  {
    getTokenUseCase = GetTokenUseCase;
    getCountersUseCase = GetCountersUseCase;
    renewTokenUseCase = RenewTokenUseCase;
  }
  private GetTokenUseCase getTokenUseCase { get; set; }
  private GetCountersUseCase getCountersUseCase { get; set; }
  private RenewTokenUseCase renewTokenUseCase { get; set; }

  // 2. Authorization Grant (inbound)
  public async Task<ActionResult> Callback(string code, string state)
  {

    // 3. Authorization Grant (outbound)
    
    getTokenUseCase.Code = code;
    getTokenUseCase.State = state;

    var tokenResult = await getTokenUseCase.Execute();
    
    if (tokenResult.DidFail)
    {
      return Unauthorized(tokenResult.ErrorMessage);
    }
    
    var authResponse  = tokenResult.Value;
    
    renewTokenUseCase.RefreshToken = authResponse.refresh_token;

    // 4. Access Token (inbound)
    getCountersUseCase.Token = authResponse.access_token;

    // 5. Access Token (outbound)
    var countersResult = await getCountersUseCase.Execute();

    if (countersResult.DidFail)
    {
      return BadRequest(countersResult.ErrorMessage);
    }

    // 6. Protected Resource
    return Ok(countersResult.Value);

  }

  [Route("/renewtoken")]
  public async Task<ActionResult> RenewToken()
  {
    var result = await renewTokenUseCase.Execute();
    
    if(result.DidFail){
      return Redirect("/");
    }

    return Ok(result.Value);
  }
}