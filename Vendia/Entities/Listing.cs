using System.Collections.Generic;

namespace Vendia.Entities
{

  public enum MassUnit
  {
    Unspecified,
    Micrograms,
    Milligrams,
    Grams,
    Kilograms,
    Ounces,
    Pounds,
    Units,
  }

  public class Listing : Entity
  {
    public sealed class Option
    {
      public Option(string name, float quantity, MassUnit unit = MassUnit.Units)
      {
        Name = name;
        Quantity = new Quantity(quantity, unit);
      }

      public string Name { get; internal set; }
      public Quantity Quantity { get; internal set; }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Listing"/> class.
    /// </summary>
    /// <param name="id">The unique identifier.</param>
    public Listing(Id id) : base(id)
    {
      Options = new List <Option>();
    }

    public string Name { get; internal set; }
    public string Description { get; internal set; }

    public Ref <Product> Product { get; internal set; }
    public List <Option> Options { get; }
  }

}