using System.Threading.Tasks;
using System.Collections.Generic;
using Common;
using Services;
using Domain;

namespace Application
{
  public class GetCountersUseCase : IUseCase<Task<Result<List<Counter>>>>
  {
    private ICountersService countersService { get; set; }
    public string Token { 
      get { return countersService.Token; }
      set { countersService.Token = value; } 
    }
    public GetCountersUseCase(ICountersService CountersService)
    {
        countersService = CountersService;
    }
    public async Task<Result<List<Counter>>> Execute()
    {
      return await countersService.GetCounters();
    }
  }
}