using System;

using Vendia.API;

namespace Vendia.Entities
{
  /// <summary>
  /// Represents an object with its own unique identifier.
  /// </summary>
  public class Entity : Node, IEntity, IEquatable <Entity>
  {
    public static readonly Entity Null = new Entity(Id.Null);

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity"/> class.
    /// </summary>
    /// <param name="id">The unique identifier.</param>
    public Entity(Id id)
    {
      Id = id;
    }

    /// <summary>
    /// Gets the unique identfifier of the <see cref="Entity"/>.
    /// </summary>
    public Id Id { get; }

    /// <summary>
    /// Indicates whether or not the <see cref="Entity"/> points to an <see cref="Vendia.Id"/> that is <see langword="null"/>.
    /// </summary>
    public bool IsNull => Id.IsNull;

    public bool Equals(Entity other)
    {
      return Id.Equals(other?.Id);
    }

    public bool Equals(IEntity other)
    {
      return Id.Equals(other?.Id);
    }
  }

}