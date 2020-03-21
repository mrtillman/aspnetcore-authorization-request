using System.Threading.Tasks;
using Common;
using Services;
using Domain;

namespace Application
{
  public class RenewTokenUseCase : IUseCase<Task<Result<AuthorizationResponse>>>
  {
    public RenewTokenUseCase(ISecureService SecureService, ICacheService CacheService)
    {
        secureService = SecureService;
        cacheService = CacheService;
    }
    private ISecureService secureService { get; set; }
    private ICacheService cacheService { get; set; }
    public string RefreshToken { 
      get {
        return cacheService.GetValue<string>(KEYS.REFRESH_TOKEN);
      } 
      set {
        cacheService.SetValue(KEYS.REFRESH_TOKEN, value);
      }
    }
    public async Task<Result<AuthorizationResponse>> Execute()
    {
      if(string.IsNullOrEmpty(RefreshToken)) {
        return Result<AuthorizationResponse>.Fail("Please Sign In");
      }
      var result = await secureService.RenewToken(RefreshToken);
      if(result.DidSucceed){
        RefreshToken = result.Value.refresh_token;
      }
      return result;
    }
  }
}