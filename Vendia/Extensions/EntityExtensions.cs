using System;

using Vendia.API;
using Vendia.Entities;

namespace Vendia.Extensions
{

  public static class EntityExtensions
  {

    public static TEntity Return <TEntity>(this TEntity entity)
      where TEntity : class, IEntity
    {
      return entity == null || entity.IsNull ? Entity.Null as TEntity : entity;
    }

    public static IEntity Return(this IEntity entity)
    {
      return entity == null || entity.IsNull ? Entity.Null : entity;
    }

    public static Ref <TEntity> Ref <TEntity>(this TEntity entity)
      where TEntity : class, IEntity
    {
      return new Ref <TEntity>(entity.Id, entity);
    }

    public static TEntity Null <TEntity>()
    {
      return (TEntity) Activator.CreateInstance(typeof (TEntity), Id.Null);
    }
  }

}