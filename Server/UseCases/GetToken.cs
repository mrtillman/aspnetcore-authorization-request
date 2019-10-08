using System.Threading.Tasks;
using AuthDemo.API;
using AuthDemo.Models;

namespace AuthDemo.UseCases
{
  public class GetTokenUseCase : IUseCase<Task<Result<string>>>
  {
    public GetTokenUseCase(SecureApi SecureApi)
    {
        secureApi = SecureApi;
    }
    private SecureApi secureApi { get; set; }
    public string Code { get; set; }
    public string State { get; set; }
    public async Task<Result<string>> Execute()
    {
      Result<AuthResponse> authResult = await secureApi.GetToken(Code, State);

      if (authResult.DidFail)
      {
        return Result<string>.Fail(authResult.ErrorMessage);
      }

      return Result<string>.Ok((authResult.Value.access_token));
    }
  }
}