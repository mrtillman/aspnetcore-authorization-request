using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services;
using Application;
using Common;
using Domain;

namespace Presentation {

  public class AppController : Controller
  {

    public AppController(
      GetTokenUseCase GetTokenUseCase, 
      GetCountersUseCase GetCountersUseCase,
      RenewTokenUseCase RenewTokenUseCase,
      ISecureService SecureService,
      ICacheService CacheService)
    {
      getTokenUseCase = GetTokenUseCase;
      getCountersUseCase = GetCountersUseCase;
      renewTokenUseCase = RenewTokenUseCase;
      secureService = SecureService;
      cacheService = CacheService;
    }

    private ISecureService secureService { get; set; }
    private ICacheService cacheService { get; set; }
    private GetTokenUseCase getTokenUseCase { get; set; }
    private GetCountersUseCase getCountersUseCase { get; set; }
    private RenewTokenUseCase renewTokenUseCase { get; set; }

    [Route("/")]
    public ViewResult Index()
    {
      cacheService.Clear();
      return View();
    }
    [Route("/home/signin")]
    public void SignIn()
    {
      // 1. Begin Authorization Request
      Response.Redirect(secureService.AuthorizationUrl);
    }

    // 2. Authorization Grant (inbound)
    [Route("/oauth2/callback")]
    public async Task<ActionResult> Callback(string code, string state)
    {
      var authResponse = cacheService.GetValue<AuthorizationResponse>(KEYS.ACCESS_TOKEN);

      if(authResponse == null){

        // 3. Authorization Grant (outbound)
        getTokenUseCase.Code = code;
        getTokenUseCase.State = state;

        // 4. Access Token (inbound)
        var tokenResult = await getTokenUseCase.Execute();
        authResponse = tokenResult.Value;
      }

      // 5. Access Token (outbound)      
      getCountersUseCase.Token = authResponse.access_token;

      // 6. Protected Resource
      var countersResult = await getCountersUseCase.Execute();
      
      renewTokenUseCase.RefreshToken = authResponse.refresh_token;

      return Ok(countersResult.Value);

    }

    [Route("/renewtoken")]
    public async Task<ActionResult> RenewToken()
    {
      var result = await renewTokenUseCase.Execute();

      if(result.DidSucceed){
        return Ok(result.Value);
      }

      return Redirect("/");
    }
  }
}
