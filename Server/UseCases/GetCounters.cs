using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using AuthDemo.API;
using AuthDemo.Models;

namespace AuthDemo.UseCases
{
  public class GetCountersUseCase : IUseCase<Task<Result<List<Counter>>>>
  {
    private CoreApi coreApi { get; set; }
    public string Token { get; set; }
    public GetCountersUseCase(CoreApi CoreApi)
    {
        coreApi = CoreApi;
    }
    public async Task<Result<List<Counter>>> Execute()
    {
      coreApi.Token = Token;

      Result<List<Counter>> countersResult = await coreApi.GetCounters();

      if (countersResult.DidFail)
      {
        return Result<List<Counter>>.Fail(countersResult.ErrorMessage);
      }

      return Result<List<Counter>>.Ok(countersResult.Value);
    }
  }
}