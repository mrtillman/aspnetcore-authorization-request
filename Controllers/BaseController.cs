using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

public class BaseController : Controller
{

    public BaseController(IConfiguration Configuration)
    {
        configuration = Configuration;
        client = new HttpClient();
    }

    protected IConfiguration configuration { get; set; }
    protected HttpClient client { get; set; }

    protected static string _state { get; set; }

}