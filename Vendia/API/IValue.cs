namespace Vendia.API
{

  public interface IValue <out T>
  {
    T Value { get; }
  }

}