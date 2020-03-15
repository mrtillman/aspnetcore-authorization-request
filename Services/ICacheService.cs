using System.Threading.Tasks;
using System.Collections.Generic;
using Common;
using Domain;

namespace Services {
  public interface ICacheService
  {
    void Clear();
    T GetValue<T>(KEYS key);
    void SetValue<T>(KEYS key, T value);
  }
}