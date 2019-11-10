using System;

namespace Application
{
  public interface IUseCase<T>
  {
      T Execute();
  }
}