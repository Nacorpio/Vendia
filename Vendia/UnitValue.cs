using System;

using Vendia.API;
using Vendia.Entities;

namespace Vendia
{

  public struct Quantity : IValue <float>, IEquatable <Quantity>, IComparable <Quantity>, ICloneable
  {
    public Quantity(float value = 0, MassUnit unit = MassUnit.Units)
    {
      Unit  = unit;
      Value = value;
    }

    public float Value { get; }
    public MassUnit Unit { get; }

    public bool Equals(Quantity other)
    {
      return Value.Equals(other.Value) && Unit.Equals(other.Unit);
    }

    public int CompareTo(Quantity other)
    {
      return Value.CompareTo(other.Value);
    }

    public object Clone()
    {
      return new Quantity(Value, Unit);
    }
  }

}