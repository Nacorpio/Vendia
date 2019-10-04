using System;

namespace Vendia.Entities
{

  /// <summary>
  /// Represents a product that can be sold or purchased.
  /// </summary>
  public class Product : Entity
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="Product"/> class.
    /// </summary>
    /// <param name="id">The unique identifier.</param>
    public Product(Id id)
      : base(id)
    {
    }

    /// <summary>
    /// Gets the name of the <see cref="Product"/>.
    /// </summary>
    public string Name { get; internal set; }

    /// <summary>
    /// Gets the description of the <see cref="Product"/>.
    /// </summary>
    public string Description { get; internal set; }

    /// <summary>
    /// Creates a new <see cref="Product"/> instance using the specified builder function.
    /// </summary>
    /// <param name="builderFunc">The builder function.</param>
    /// <returns>A <see cref="Ref{TEntity}"/> referring to the created <see cref="Product"/>.</returns>
    public static Ref <Product> Create(Action <ProductBuilder> builderFunc)
    {
      return Factory.Create <Product, ProductBuilder>(builderFunc);
    }
  }

}