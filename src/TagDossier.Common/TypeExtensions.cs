using System;
using System.Collections.Generic;
using System.Linq;
using Dawn;

namespace TagDossier.Common
{
    public static class TypeExtensions
    {
        public static bool TryGetGenericType<T1, T2, T3>(this Type type, out Type genericType)
        {
            return type.TryGetGenericType(
                new[] { typeof(T1), typeof(T2), typeof(T3) },
                out genericType);
        }

        public static bool TryGetGenericType(this Type type,
            IEnumerable<Type> requestedTypes,
            out Type genericType)
        {
            var requestedType = type.GetInterfaces().FirstOrDefault(x =>
                x.IsGenericType &&
                requestedTypes.Any(y => y.IsAssignableFrom(x)));

            if (requestedType == null)
            {
                genericType = null;
                return false;
            }

            var firstGenericType = requestedType.GenericTypeArguments.Single();
            genericType = firstGenericType.TryGetGenericType(requestedTypes, out var secondGenericType)
                ? secondGenericType
                : firstGenericType;

            return true;
        }
        
        public static bool IsAssignableFromRawGeneric(this Type extendType, Type baseType)
        {
            Guard.Argument(extendType, nameof(extendType)).NotNull();
            Guard.Argument(baseType, nameof(baseType)).NotNull();

            return baseType.GetInterfaces().Any(x => extendType == (x.IsGenericType ? x.GetGenericTypeDefinition() : x));
        }
    }
}