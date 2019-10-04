namespace Vendia.API
{

  public interface IBuilder <out TEntity> : IBuilder
    where TEntity : IEntity
  {
    new TEntity Build(Id id);
  }

  public interface IBuilder
  {
    IEntity Build(Id id);
  }

}