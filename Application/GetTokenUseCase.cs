using System.Threading.Tasks;
using Common;
using Services;
using Domain;

namespace Application
{
  public class GetTokenUseCase : IUseCase<Task<Result<AuthorizationResponse>>>
  {
    public GetTokenUseCase(ISecureService SecureService)
    {
        secureService = SecureService;
    }
    private ISecureService secureService { get; set; }
    public string Code { get; set; }
    public string State { get; set; }
    public async Task<Result<AuthorizationResponse>> Execute()
    {
      return await secureService.GetToken(Code, State);
    }
  }
}