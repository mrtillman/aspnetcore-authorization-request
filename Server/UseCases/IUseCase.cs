using System;

namespace AuthDemo.UseCases
{
  public interface IUseCase<T>
  {
      T Execute();
  }
}