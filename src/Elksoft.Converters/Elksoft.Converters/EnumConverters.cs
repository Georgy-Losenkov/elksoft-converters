// <copyright file="EnumConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using Elksoft.Converters.StandardConverters;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IEnumConverters"/>
    public sealed class EnumConverters : IEnumConverters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EnumConverters"/> class.
        /// </summary>
        public EnumConverters()
        {
        }

        /// <inheritdoc />
        public Boolean IsSupported(Type type)
        {
            Check.NotNull(type, nameof(type));

            return type.IsEnum;
        }

        /// <inheritdoc />
        public Converter GetUnderlyingToEnum(Type enumType)
        {
            return GetFieldValue(enumType, nameof(Storage<DayOfWeek>.UnderlyingToEnum));
        }

        /// <inheritdoc />
        public Converter GetEnumToUnderlying(Type enumType)
        {
            return GetFieldValue(enumType, nameof(Storage<DayOfWeek>.EnumToUnderlying));
        }

        /// <inheritdoc />
        public Converter GetEnumToString(Type enumType)
        {
            return GetFieldValue(enumType, nameof(Storage<DayOfWeek>.EnumToString));
        }

        /// <inheritdoc />
        public Converter GetStringToEnum(Type enumType)
        {
            return GetFieldValue(enumType, nameof(Storage<DayOfWeek>.StringToEnum));
        }

        private static Converter GetFieldValue(Type enumType, String fieldName)
        {
            Check.NotNull(enumType, nameof(enumType));

            if (!enumType.IsEnum)
            {
                throw new ArgumentException("Must be enum type", nameof(enumType));
            }

            var storageType = typeof(Storage<>).MakeGenericType(enumType);
            var converter = storageType.InvokeMember(
                name: fieldName,
                invokeAttr: BindingFlags.Public | BindingFlags.Static | BindingFlags.GetField,
                binder: null,
                target: null,
                args: Array.Empty<Object>());
            return (Converter)(converter!);
        }

        private static class Storage<TEnum>
            where TEnum : struct, Enum
        {
            public static readonly Converter StringToEnum = new StringToEnumConverter<TEnum>();
            public static readonly Converter EnumToString = new EnumToStringConverter<TEnum>();

            public static readonly Converter UnderlyingToEnum = CreateUnderlyingToEnum();
            public static readonly Converter EnumToUnderlying = CreateEnumToUnderlying();

            private static Converter CreateUnderlyingToEnum()
            {
                var enumType = typeof(TEnum);
                var underlyingType = enumType.GetEnumUnderlyingType();

                var converterType = typeof(CultureInvariantDelegateConverter<,>).MakeGenericType(underlyingType, enumType);
                var converter = Activator.CreateInstance(converterType, EnumDelegates<TEnum>.UnderlyingToEnumDelegate, false, false);
                return (Converter)(converter!);
            }

            private static Converter CreateEnumToUnderlying()
            {
                var enumType = typeof(TEnum);
                var underlyingType = enumType.GetEnumUnderlyingType();

                var converterType = typeof(CultureInvariantDelegateConverter<,>).MakeGenericType(enumType, underlyingType);
                var converter = Activator.CreateInstance(converterType, EnumDelegates<TEnum>.EnumToUnderlyingDelegate, false, false);
                return (Converter)(converter!);
            }
        }
    }
}