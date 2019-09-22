using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using AuthDemo.Constants;
using System.Collections.Specialized;
using Microsoft.Extensions.Configuration;

public class OAuth2Controller : Controller
{

    public OAuth2Controller(IConfiguration Configuration)
    {
        _configuration = Configuration;
    }

    private IConfiguration _configuration { get; set; }

    public string Callback()
    {
        var code = Request.Query["code"];
        return code;
    } 

}