using System;
using System.Text.RegularExpressions;

namespace Common {
  public class AuthorizationUrlRegex : Regex {
    public AuthorizationUrlRegex()
      :base($"^((http|https)://({hostNames})/connect/authorize\\?{requestParameters})(.*)$")
      { }

    private static string hostNames
    {
      get => String.Join("|", new String[]{
                "localhost:5000",
                "secure.counter-culture.io",
                "counter-culture:5000"
            });
    }
    private static string requestParameters
    {
      get => String.Join("", new String[]{
                "(?=.*(response_type=(.*)))",
                "(?=.*(&client_id=(.*)))",
                "(?=.*(&redirect_uri=(.*)))",
                "(?=.*(&scope=(.*)))",
                "(?=.*(&state=(.*)))",
            });
    }
  }
}
