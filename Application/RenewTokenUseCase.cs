using System.Threading.Tasks;
using Common;
using Services;
using Domain;

namespace Application
{
  public class RenewTokenUseCase : IUseCase<Task<Result<AuthorizationResponse>>>
  {
    public RenewTokenUseCase(ISecureService SecureService)
    {
        secureService = SecureService;
    }
    private ISecureService secureService { get; set; }
    public string RefreshToken { get; set; }
    public async Task<Result<AuthorizationResponse>> Execute()
    {
      return await secureService.RenewToken(RefreshToken);
    }
  }
}