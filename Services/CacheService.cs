using Common;
using System.Collections.Generic;

// TODO: consume Redis client
// https://www.nuget.org/packages/StackExchange.Redis

namespace Services
{
  public class CacheService : ICacheService
  {
    static Dictionary<KEYS, object> cache = new Dictionary<KEYS, object>();
    public string GetRefreshToken()
    {
      object refresh_token;
      if(CacheService.cache.TryGetValue(KEYS.REFRESH_TOKEN, out refresh_token)){
        return (string)refresh_token;
      };
      return null;
    }

    public T GetValue<T>(KEYS key)
    {
      object result;
      if(CacheService.cache.TryGetValue(key, out result)){
        return (T)result;
      };
      return default(T);
    }

    public void SetRefreshToken(string value)
    {
      CacheService.cache[KEYS.REFRESH_TOKEN] = value;
    }

    public void SetValue<T>(KEYS key, T value)
    {
      CacheService.cache[key] = value;
    }
  }
}