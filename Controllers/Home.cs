using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using AuthDemo.Constants;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

public class HomeController : Controller
{

    public HomeController(IConfiguration Configuration)
    {
        _configuration = Configuration;
    }

    private IConfiguration _configuration { get; set; }

    public ViewResult Index()
    {
        return View();
    } 

    public void SignIn()
    {
        Response.Redirect(getAuthUrl());
    }

    private string getAuthUrl(){
        var baseUrl = ServerUrls.SECURE[ENV.DEV];
        var client_id = _configuration["CLIENT_ID"].ToString();
        var redirect_uri = _configuration["REDIRECT_URI"].ToString();
        NameValueCollection querystring = HttpUtility.ParseQueryString(string.Empty);
        querystring["response_type"] = "code";
        querystring["client_id"] = client_id;
        querystring["redirect_uri"] = redirect_uri;
        querystring["scope"] = "openid";
        querystring["state"] = Guid.NewGuid().ToString();
        var parameters = querystring.ToString();
        return $"{baseUrl}/connect/authorize?{parameters}";
    }
}