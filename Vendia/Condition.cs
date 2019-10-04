namespace Vendia
{

  public abstract class Condition<T>
  {
    public abstract bool Evaluate(T @in);
  }

}