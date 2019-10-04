using System;
using System.Collections.Generic;
using System.Linq;

namespace Vendia.Extensions
{

  public static class ReflectionExtensions
  {
    public static bool HasGenericTypeParameter(this Type type, Type other)
    {
      return type != null && other.GenericTypeArguments.Any(x => x == other);
    }

    public static bool HasGenericTypeParameters(this Type type, IEnumerable <Type> pars)
    {
      return pars.All(type.HasGenericTypeParameter);
    }

    public static bool HasGenericTypeParameter(this object type, Type other)
    {
      return type.GetType().HasGenericTypeParameter(other);
    }

    public static bool HasGenericTypeParameters(this object type, IEnumerable <Type> pars)
    {
      return type.GetType().HasGenericTypeParameters(pars);
    }



  }

}