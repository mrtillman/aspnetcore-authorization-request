namespace Domain
{
  public class AuthorizationRequest
  {
    public string clientId { get; set; }
    public string clientSecret { get; set; }
    public string code { get; set; }
    public string grantType { get; set; }
    public string redirectUri { get; set; }
    public string scope { get; set; }
    public string refreshToken { get; set; }
  }
}
