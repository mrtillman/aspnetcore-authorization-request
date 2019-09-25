using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Http;

public class AuthResponse
{
    public string access_token { get; set; }
    public string expires_in { get; set; }
    public string token_type { get; set; }
    public string scope { get; set; }
}