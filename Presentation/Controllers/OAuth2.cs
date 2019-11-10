using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Application;
using Services;
using Domain;

public class OAuth2Controller : Controller
{

  public OAuth2Controller(GetTokenUseCase GetTokenUseCase, GetCountersUseCase GetCountersUseCase)
  {
    getTokenUseCase = GetTokenUseCase;
    getCountersUseCase = GetCountersUseCase;
  }
  private GetTokenUseCase getTokenUseCase { get; set; }
  private GetCountersUseCase getCountersUseCase { get; set; }

  private CoreApi coreApi { get; set; }

  // 2. Authorization Grant
  public async Task<ActionResult> Callback()
  {

    // 3. Authorization Grant
    
    getTokenUseCase.Code = Request.Query["code"];
    getTokenUseCase.State = Request.Query["state"];

    var tokenResult = await getTokenUseCase.Execute();
    
    if (tokenResult.DidFail)
    {
      return Unauthorized(tokenResult.ErrorMessage);
    }

    // 4. Access Token
    getCountersUseCase.Token = tokenResult.Value;

    // 5. Access Token
    var countersResult = await getCountersUseCase.Execute();

    if (countersResult.DidFail)
    {
      return BadRequest(countersResult.ErrorMessage);
    }

    // 6. Protected Resource
    return Ok(countersResult.Value);

  }

}