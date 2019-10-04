using Vendia.API;
using Vendia.Entities;

namespace Vendia
{

  public sealed class ProductBuilder : IBuilder <Product>
  {
    private string _name, _description;

    public ProductBuilder()
    {
    }

    public ProductBuilder WithName(string value)
    {
      _name = value;
      return this;
    }

    public ProductBuilder WithDescription(string value)
    {
      _description = value;
      return this;
    }

    public Product Build(Id id)
    {
      return new Product(id)
      {
        Name        = _name,
        Description = _description
      };
    }

    IEntity IBuilder.Build(Id id)
    {
      return Build(id);
    }
  }

}