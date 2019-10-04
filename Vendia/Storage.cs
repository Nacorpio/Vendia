using System;
using System.Collections.Generic;

using Vendia.API;
using Vendia.Entities;

namespace Vendia
{

  public static class Storage
  {
    private static readonly Dictionary <Id, IEntity> _entities;

    /// <summary>
    /// Initializes a new static instance of the <see cref="Storage"/> class.
    /// </summary>
    static Storage()
    {
      _entities = new Dictionary <Id, IEntity>();
    }

    /// <summary>
    /// Gets the total number of entities stored in the <see cref="Storage"/>.
    /// </summary>
    public static int Count => _entities.Count;

    /// <summary>
    /// Adds the specified <see cref="IEntity"/> to the <see cref="Storage"/>.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    public static void Add(IEntity entity)
    {
      if (entity == null)
      {
        throw new ArgumentNullException(nameof (entity));
      }

      if (entity.IsNull)
      {
        return;
      }

      if (_entities.ContainsKey(entity.Id))
      {
        return;
      }

      _entities.Add(entity.Id, entity);
    }

    /// <summary>
    /// Fetches a <typeparamref name="TEntity"/> with the specified identifier from the <see cref="Storage"/>.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <param name="id">The identifier of the <typeparamref name="TEntity"/> to fetch.</param>
    /// <param name="entity">The resulting <typeparamref name="TEntity"/>.</param>
    /// <returns><see langword="true"/> if the entity was successfully fetched; otherwise, <see langword="false"/>.</returns>
    public static bool Fetch <TEntity>(Id id, out TEntity entity)
      where TEntity : class, IEntity
    {
      if (!Fetch(id, out var result))
      {
        entity = Entity.Null as TEntity;

        return false;
      }

      entity = result as TEntity;

      return true;
    }

    /// <summary>
    /// Fetches an <see cref="IEntity"/> with the specified identifier from the <see cref="Storage"/>.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="IEntity"/> to fetch.</param>
    /// <param name="entity">The resulting <see cref="IEntity"/>.</param>
    /// <returns><see langword="true"/> if the entity was successfully fetched; otherwise, <see langword="false"/>.</returns>
    public static bool Fetch(Id id, out IEntity entity)
    {
      if (!id.IsNull)
      {
        return _entities.TryGetValue(id, out entity);
      }

      entity = Entity.Null;

      return false;
    }

    /// <summary>
    /// Removes an <see cref="IEntity"/> with the specified identifier from the <see cref="Storage"/>, if found.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="IEntity"/> to remove.</param>
    /// <returns><see langword="true"/> if the entity was successfully removed; otherwise, <see langword="false"/>.</returns>
    public static bool Remove(Id id)
    {
      return !id.IsNull && _entities.Remove(id);
    }

    /// <summary>
    /// Removes the specified <see cref="IEntity"/> from the <see cref="Storage"/>, if found.
    /// </summary>
    /// <param name="entity">The <see cref="IEntity"/> to remove.</param>
    /// <returns><see langword="true"/> if the entity was successfully removed; otherwise, <see langword="false"/></returns>
    public static bool Remove(IEntity entity)
    {
      return entity != null && !entity.IsNull && Remove(entity.Id);
    }

    /// <summary>
    /// Determines whether the <see cref="Storage"/> contains an <see cref="IEntity"/> with the specified identifier.
    /// </summary>
    /// <param name="id">The identifier of the <see cref="IEntity"/> to find.</param>
    /// <returns><see langword="true"/> if the entity was found; otherwise, <see langword="false"/>.</returns>
    public static bool Contains(Id id)
    {
      return !id.IsNull && _entities.ContainsKey(id);
    }

    /// <summary>
    /// Determines whether the <see cref="Storage"/> contains the specified <see cref="IEntity"/>.
    /// </summary>
    /// <param name="entity">The <see cref="IEntity"/> to find.</param>
    /// <returns><see langword="true"/> if the entity was found; otherwise, <see langword="false"/></returns>
    public static bool Contains(IEntity entity)
    {
      return entity != null && !entity.IsNull && Contains(entity.Id);
    }

    /// <summary>
    /// Removes all entities from the <see cref="Storage"/>.
    /// </summary>
    public static void Clear()
    {
      _entities.Clear();
    }

  }

}