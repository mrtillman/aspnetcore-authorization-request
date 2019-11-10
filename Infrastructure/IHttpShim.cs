using System.Threading.Tasks;
using System.Net.Http;

namespace Infrastructure {
  public interface IHttpShim
  {
    string BaseURL { get; set; }
    string Token { get; set; }
    Task<HttpResponseMessage> Get(string Path);
    Task<HttpResponseMessage> Post(string Path, HttpContent Content);
  }
}
