using Microsoft.AspNetCore.Mvc;
using Services;
public class HomeController : Controller
{

  public HomeController(ISecureService SecureService, ICacheService CacheService)
  {
    secureService = SecureService;
    cacheService = CacheService;
  }

  private ISecureService secureService { get; set; }
  private ICacheService cacheService { get; set; }

  public ViewResult Index()
  {
    cacheService.Clear();
    return View();
  }

  public void SignIn()
  {
    // 1. Begin Authorization Request
    Response.Redirect(secureService.AuthorizationUrl);
  }
}
