using Microsoft.AspNetCore.Mvc;

public class HomeController : Controller
{

    public HomeController (SecureApi SecureApi)
    {
        secureApi = SecureApi;
    }

    public SecureApi secureApi { get; set; }
    public ViewResult Index()
    {
        return View();
    } 

    public void SignIn()
    {
        // 1. Begin Authorization Request
        Response.Redirect(secureApi.AuthorizationUrl);
    }
}
