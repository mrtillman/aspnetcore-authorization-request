using System.Threading.Tasks;
using Common;
using Services;
using Domain;

namespace Application
{
  public class GetTokenUseCase : IUseCase<Task<Result<AuthorizationResponse>>>
  {
    public GetTokenUseCase(ISecureService SecureService, ICacheService CacheService)
    {
        secureService = SecureService;
        cacheService = CacheService;
    }
    private ISecureService secureService { get; set; }
    private ICacheService cacheService { get; set; }
    public string Code { get; set; }
    public string State { get; set; }

    public string AuthorizationUrl { 
      get {
        cacheService.Clear();
        return secureService.AuthorizationUrl;
      } 
    }
    public async Task<Result<AuthorizationResponse>> Execute()
    {
      var authResponse = cacheService.GetValue<AuthorizationResponse>(KEYS.ACCESS_TOKEN);
      if(authResponse != null) return Result.Ok(authResponse);
      var result = await secureService.GetToken(Code, State);
      if(result.DidSucceed){
        cacheService.SetValue(KEYS.ACCESS_TOKEN, result.Value);
      }
      return result;
    }
  }
}