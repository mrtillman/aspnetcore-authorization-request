namespace Tests
{
  static partial class Stub
  {
    public static class JSON {
      public static string Counters
        => "[{\"_id\":\"5d16c0cd11ee4a3d6f44b045\",\"name\":\"alcohol\",\"value\":0,\"skip\":1,\"__v\":0},{\"_id\":\"5d16c0cd11ee4a3d6f44b046\",\"name\":\"tobacco\",\"value\":0,\"skip\":1,\"__v\":0},{\"_id\":\"5d16c0cd11ee4a3d6f44b047\",\"name\":\"firearms\",\"value\":0,\"skip\":1,\"__v\":0}]";

      public static string AuthorizationResponse
        => "{\"id_token\":\"id_token\", \"access_token\":\"access_token\", \"expires_in\":86400, \"token_type\":\"Bearer\",  \"scope\":\"openid\"}";
    }
  }
}
