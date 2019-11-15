using System.Threading.Tasks;
using System.Net.Http;
using Domain;

namespace Infrastructure {
  public interface IHttpShim
  {
    string Token { get; set; }
    Task<HttpResponseMessage> FetchCounters();
    Task<HttpResponseMessage> FetchToken(AuthorizationRequest authRequest);
  }
}
