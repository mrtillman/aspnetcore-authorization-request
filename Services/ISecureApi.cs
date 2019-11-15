using System.Threading.Tasks;
using Common;
using Domain;

namespace Services {
  public interface ISecureApi
  {
      string AuthorizationUrl { get; }
      Task<Result<AuthorizationResponse>> GetToken(string code, string state);
  }
}