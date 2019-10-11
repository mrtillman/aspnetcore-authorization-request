using AuthDemo.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace AuthDemo.Interfaces {
  public interface ICoreApi
  {
    string Token { get; set; }
    Task<Result<List<Counter>>> GetCounters();
  }
}