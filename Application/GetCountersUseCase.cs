using System.Threading.Tasks;
using System.Collections.Generic;
using Common;
using Services;
using Domain;

namespace Application
{
  public class GetCountersUseCase : IUseCase<Task<Result<List<Counter>>>>
  {
    private ICounterService counterService { get; set; }
    public string Token { 
      get { return counterService.Token; }
      set { counterService.Token = value; } 
    }
    public GetCountersUseCase(ICounterService CounterService)
    {
        counterService = CounterService;
    }
    public async Task<Result<List<Counter>>> Execute()
    {
      return await counterService.GetCounters();
    }
  }
}