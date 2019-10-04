using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using Vendia.API;
using Vendia.Entities;
using Vendia.Extensions;

namespace Vendia
{

  public static class Factory
  {
    private static readonly Dictionary <Type, IBuilder> _builders;
    private static readonly HashSet <Id> _ids;

    static Factory()
    {
      _builders = new Dictionary <Type, IBuilder>();
      _ids      = new HashSet <Id>();

      Initialize();
    }

    private static void Initialize()
    {
      var assembly = Assembly.GetEntryAssembly();

      if (assembly == null)
      {
        return;
      }

      foreach (var type in assembly.DefinedTypes
       .Where(
          x => x.GetInterfaces()
           .Any(y => y == typeof (IBuilder)) && !x.IsInterface
        ))
      {
        var builderType = type.GetInterfaces().FirstOrDefault();
        var entityType  = builderType?.GenericTypeArguments.FirstOrDefault();

        if (entityType == null)
        {
          continue;
        }

        _builders.Add(entityType, (IBuilder) Activator.CreateInstance(type));
      }
    }

    /// <summary>
    /// Builds and creates a new <typeparamref name="TEntity"/> instance then returns a <see cref="Ref{TEntity}"/> referring to it.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TBuilder">The builder type.</typeparam>
    /// <param name="builderFunc">The builder function.</param>
    /// <returns>A <see cref="Ref{TEntity}"/> referring to the created <typeparamref name="TEntity"/>.</returns>
    public static Ref <TEntity> Create <TEntity, TBuilder>(Action<TBuilder> builderFunc = null)
      where TEntity : class, IEntity
      where TBuilder : class, IBuilder
    {
      var entity = Build <TEntity, TBuilder>(new Id(_ids.Count), builderFunc);

      if (Storage.Contains(entity))
      {
        return Ref <TEntity>.Null;
      }

      Storage.Add(entity);

      return entity.Ref();
    }

    /// <summary>
    /// Builds a new <typeparamref name="TEntity"/> instance.
    /// </summary>
    /// <typeparam name="TEntity">The entity type.</typeparam>
    /// <typeparam name="TBuilder">The builder type.</typeparam>
    /// <param name="id">The unique identifier.</param>
    /// <param name="builderFunc">The builder function.</param>
    /// <returns>The entity if successfully built; otherwise, <see cref="Entity.Null"/>.</returns>
    internal static TEntity Build <TEntity, TBuilder>(Id id, Action<TBuilder> builderFunc = null)
      where TEntity : class, IEntity
      where TBuilder : class, IBuilder
    {
      if (_ids.Contains(id))
      {
        return Entity.Null as TEntity;
      }

      if (id.IsNull)
      {
        return Entity.Null as TEntity;
      }

      var type = typeof (TEntity);

      if (!_builders.TryGetValue(type, out var builder))
      {
        return Entity.Null as TEntity;
      }

      builderFunc?.Invoke(builder as TBuilder);
      _ids.Add(id);

      return builder.Build(id) as TEntity;
    }
  }

}