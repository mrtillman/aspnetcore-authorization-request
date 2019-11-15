using Microsoft.AspNetCore.Mvc;
using Services;
public class HomeController : Controller
{

  public HomeController(ISecureService SecureService)
  {
    secureService = SecureService;
  }

  private ISecureService secureService { get; set; }

  public ViewResult Index()
  {
    return View();
  }

  public void SignIn()
  {
    // 1. Begin Authorization Request
    Response.Redirect(secureService.AuthorizationUrl);
  }
}
