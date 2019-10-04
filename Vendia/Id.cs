using System;

using Vendia.Entities;

namespace Vendia
{

  /// <summary>
  /// Represents a unique <see cref="Entity"/> identifier.
  /// </summary>
  public struct Id : IEquatable <Id>, ICloneable
  {
    public static readonly Id Null = new Id(-1);

    /// <summary>
    /// Initializes a new instance of the <see cref="Id"/> structure.
    /// </summary>
    /// <param name="value">The identifier value.</param>
    public Id(int value = -1)
    {
      Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Id"/> structure based on <paramref name="other"/>.
    /// </summary>
    /// <param name="other">The <see cref="Id"/> to clone.</param>
    public Id(Id other)
    {
      Value = other.Value;
    }

    /// <summary>
    /// Gets the value of the <see cref="Id"/>.
    /// </summary>
    public int Value { get; }

    /// <summary>
    /// Indicates whether or not the identifier value represents <see langword="null"/>.
    /// </summary>
    public bool IsNull => Value.Equals(-1);

    public bool Equals(Id other)
    {
      return Value.Equals(other.Value);
    }

    /// <summary>Creates a new object that is a copy of the current instance.</summary>
    /// <returns>A new object that is a copy of this instance.</returns>
    public object Clone()
    {
      return new Id(this);
    }
  }

}