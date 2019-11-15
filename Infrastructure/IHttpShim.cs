using System.Threading.Tasks;
using System.Net.Http;

namespace Infrastructure {
  public interface IHttpShim
  {
    string BaseURL { get; set; }
    string Token { get; set; }
    Task<HttpResponseMessage> FetchCounters(string Path);
    Task<HttpResponseMessage> FetchToken(string Path, HttpContent Content);
  }
}
