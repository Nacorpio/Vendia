using System;

using Vendia.Entities;

namespace Vendia.API
{

  public interface IEntity : IEquatable <IEntity>
  {
    /// <summary>
    /// Gets the unique identifier of the <see cref="IEntity"/>.
    /// </summary>
    Id Id { get; }

    /// <summary>
    /// Indicates whether or not the <see cref="Entity"/> points to an <see cref="Vendia.Id"/> that is <see langword="null"/>.
    /// </summary>
    bool IsNull { get; }
  }

}