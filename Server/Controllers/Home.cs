using Microsoft.AspNetCore.Mvc;
using AuthDemo.API;
public class HomeController : Controller
{

  public HomeController(SecureApi SecureApi)
  {
    secureApi = SecureApi;
  }

  private SecureApi secureApi { get; set; }

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
