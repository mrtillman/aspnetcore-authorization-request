using Common;
using System.Collections.Generic;

namespace Services
{
  public class CacheService : ICacheService
  {
    static Dictionary<KEYS, object> cache = new Dictionary<KEYS, object>();
    public string GetRefreshToken()
    {
      return CacheService.cache[KEYS.REFRESH_TOKEN] as string;
    }

    public T GetValue<T>(KEYS key)
    {
      return (T)CacheService.cache[key];
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