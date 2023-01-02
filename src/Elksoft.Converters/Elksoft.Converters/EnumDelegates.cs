// <copyright file="EnumDelegates.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Linq.Expressions;

namespace Elksoft.Converters
{
    internal static class EnumDelegates<TEnum>
        where TEnum : struct, Enum
    {
        public static readonly Delegate UnderlyingToEnumDelegate = CreateUnderlyingToEnum();
        public static readonly Delegate EnumToUnderlyingDelegate = CreateEnumToUnderlying();

        private static Delegate CreateUnderlyingToEnum()
        {
            var enumType = typeof(TEnum);
            var underlyingType = enumType.GetEnumUnderlyingType();

            var param = Expression.Parameter(underlyingType, "value");
            var exp = Expression.Lambda(
                typeof(Func<,>).MakeGenericType(underlyingType, enumType),
                Expression.Convert(param, enumType),
                param);

            return exp.Compile();
        }

        private static Delegate CreateEnumToUnderlying()
        {
            var enumType = typeof(TEnum);
            var underlyingType = enumType.GetEnumUnderlyingType();

            var param = Expression.Parameter(enumType, "value");
            var exp = Expression.Lambda(
                typeof(Func<,>).MakeGenericType(enumType, underlyingType),
                Expression.Convert(param, underlyingType),
                param);

            return exp.Compile();
        }
    }
}