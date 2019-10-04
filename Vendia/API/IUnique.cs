using System;

namespace Vendia.API
{

  public interface IUnique <out TValue>
    where TValue : IEquatable <TValue>
  {
    TValue Value { get; }
    bool IsNull { get; }
  }

}