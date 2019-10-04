using System;

using Vendia.API;
using Vendia.Entities;

namespace Vendia
{

  /// <summary>
  /// Represents a cached reference to a <typeparamref name="TEntity"/>.
  /// </summary>
  /// <typeparam name="TEntity">The entity type.</typeparam>
  public struct Ref <TEntity> : IEquatable <Ref <TEntity>>
    where TEntity : class, IEntity
  {
    public static readonly Ref <TEntity> Null = new Ref <TEntity>(Id.Null, Entity.Null as TEntity);

    /// <summary>
    /// Initializes a new instance of the <see cref="Ref{TEntity}"/> structure.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <param name="value">The value.</param>
    public Ref(Id id, TEntity value = default)
    {
      Id    = id;
      Value = value;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ref{TEntity}"/> structure by cloning another instance.
    /// </summary>
    /// <param name="other">The instance to clone.</param>
    public Ref(Ref <TEntity> other)
    {
      Id    = other.Id;
      Value = other.Value;
    }

    /// <summary>
    /// Gets the identifier of the <see cref="Ref{TEntity}"/>.
    /// </summary>
    public Id Id { get; }

    /// <summary>
    /// Gets the value of the <see cref="Ref{TEntity}"/>.
    /// </summary>
    public TEntity Value { get; private set; }

    /// <summary>
    /// Indicates whether the <see cref="Ref{TEntity}"/> has a value.
    /// </summary>
    public bool HasValue => Value != null && !Value.IsNull;

    /// <summary>
    /// Indicates whether the <see cref="Ref{TEntity}"/> contains no value.
    /// </summary>
    public bool IsEmpty => !HasValue;

    /// <summary>
    /// Indicates whether the <see cref="Ref{TEntity}"/> points to a non-existent <typeparamref name="TEntity"/>. 
    /// </summary>
    public bool IsNullRef => !Storage.Contains(Id);

    /// <summary>
    /// Attemps to fetch the <typeparamref name="TEntity"/> associated with the current <see cref="Ref{TEntity}"/>.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns><see langword="true"/> if the entity was successfully fetched; otherwise, <see langword="false"/>.</returns>
    public bool TryFetch(out TEntity result)
    {
      if (Storage.Fetch(Id, out result))
      {
        Value = result;

        return true;
      }

      result = Entity.Null as TEntity;

      return false;
    }

    /// <summary>
    /// Gets or fetches the <typeparamref name="TEntity"/> associated with the current <see cref="Ref{TEntity}"/>, depending on if it is already cached.
    /// </summary>
    /// <returns>The entity if it was returned or fetched successfully; otherwise, <see cref="Entity.Null"/>.</returns>
    public TEntity GetOrFetch()
    {
      if (HasValue)
      {
        return Value;
      }

      if (Id.IsNull)
      {
        return Entity.Null as TEntity;
      }

      return TryFetch(out var entity) ? entity : Entity.Null as TEntity;
    }

    public bool Equals(Ref <TEntity> other)
    {
      return Id.Equals(other.Id);
    }
  }

}