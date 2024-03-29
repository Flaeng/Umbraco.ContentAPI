using System;
using System.Collections;
using System.Linq;

namespace Flaeng.Umbraco.Extensions;

public static class TypeExtensions
{
    public static bool IsEnumerableOf<T>(this Type t)
        => typeof(IEnumerable).IsAssignableFrom(t)
        && typeof(T).IsAssignableFrom(t.GetGenericArguments().FirstOrDefault());

}