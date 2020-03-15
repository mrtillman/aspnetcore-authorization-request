using Common;
using System.Collections.Generic;

// TODO: consume Redis client
// https://www.nuget.org/packages/StackExchange.Redis

namespace Services
{
  public class CacheService : ICacheService
  {
    static Dictionary<KEYS, object> cache = new Dictionary<KEYS, object>();
    public void Clear(){
      CacheService.cache = new Dictionary<KEYS, object>();
    }

    public T GetValue<T>(KEYS key)
    {
      object result;
      if(CacheService.cache.TryGetValue(key, out result)){
        return (T)result;
      };
      return default(T);
    }

    public void SetValue<T>(KEYS key, T value)
    {
      CacheService.cache[key] = value;
    }
  }
}