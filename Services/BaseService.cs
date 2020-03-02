using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Infrastructure;

namespace Services
{
  public class BaseService
  {

    public BaseService(IConfiguration Configuration, IServiceAgent ServiceAgent)
    {
      configuration = Configuration;
      agent = ServiceAgent;
    }

    protected IConfiguration configuration { get; set; }
    protected IServiceAgent agent { get; set; }

    protected async Task<T> DeserializeResponseStringAs<T>(HttpResponseMessage response)
    {
      var responseJson = await response.Content.ReadAsStringAsync();
      return JsonConvert.DeserializeObject<T>(responseJson);
    }

  }
}
