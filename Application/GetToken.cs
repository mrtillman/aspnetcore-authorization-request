using System.Threading.Tasks;
using Common;
using Services;
using Domain;

namespace Application
{
  public class GetTokenUseCase : IUseCase<Task<Result<string>>>
  {
    public GetTokenUseCase(ISecureApi SecureApi)
    {
        secureApi = SecureApi;
    }
    private ISecureApi secureApi { get; set; }
    public string Code { get; set; }
    public string State { get; set; }
    public async Task<Result<string>> Execute()
    {
      Result<AuthorizationResponse> authResult = await secureApi.GetToken(Code, State);

      if (authResult.DidFail)
      {
        return Result<string>.Fail(authResult.ErrorMessage);
      }

      return Result<string>.Ok(authResult.Value.access_token);
    }
  }
}