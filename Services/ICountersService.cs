using System.Threading.Tasks;
using System.Collections.Generic;
using Common;
using Domain;

namespace Services {
  public interface ICounterService
  {
    string Token { get; set; }
    Task<Result<List<Counter>>> GetCounters();
  }
}