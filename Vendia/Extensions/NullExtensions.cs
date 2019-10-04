using System.Collections.Generic;
using System.Linq;

using Vendia.Entities;

namespace Vendia.Extensions
{

  public static class NullExtensions
  {
    /// <summary>
    /// Determines whether the <paramref name="collection"/> is either <see langword="null"/> or empty.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    /// <param name="collection">The object collection.</param>
    /// <returns><see langword="true"/> if <paramref name="collection"/> is <see langword="null"/> or empty; otherwise, <see langword="false"/>.</returns>
    public static bool IsNullOrEmpty <T>(this IEnumerable <T> collection)
    {
      return collection == null || !collection.Any();
    }

    /// <summary>
    /// Determines whether all objects in the specified collection are <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    /// <param name="collection">The object collection.</param>
    /// <returns><see langword="true"/> if all the objects in the collection are null; otherwise, <see langword="false"/>.</returns>
    public static bool AreNull <T>(this IEnumerable <T> collection)
    {
      return collection != null && collection.All(x => x.Equals(null));
    }

    /// <summary>
    /// Determines whether the specified <typeparamref name="T"/> is <see langword="null"/>.
    /// </summary>
    /// <typeparam name="T">The object type.</typeparam>
    /// <param name="obj">The object.</param>
    /// <returns><see langword="true"/> if the object is <see langword="null"/>; otherwise, <see langword="false"/>.</returns>
    public static bool IsNull <T>(this T obj)
    {
      switch (obj)
      {
        case Id id : return id.IsNull;
        case Entity entity : return entity.IsNull;

        default : return obj.Equals(null);
      }
    }

  }

}