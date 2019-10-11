using Microsoft.AspNetCore.Mvc;
using AuthDemo.Interfaces;
public class HomeController : Controller
{

  public HomeController(ISecureApi SecureApi)
  {
    secureApi = SecureApi;
  }

  private ISecureApi secureApi { get; set; }

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
