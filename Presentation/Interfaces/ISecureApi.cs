using AuthDemo.Models;
using System.Threading.Tasks;

namespace AuthDemo.Interfaces {
  public interface ISecureApi
  {
      string AuthorizationUrl { get; }
      Task<Result<AuthResponse>> GetToken(string code, string state);
  }
}