// <copyright file="UserTypeConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Elksoft.Converters.StandardConverters;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IUserTypeConverters" />
    public sealed class UserTypeConverters : IUserTypeConverters
    {
        /// <inheritdoc />
        public IEnumerable<Converter> GetExplicitConvertersFrom(Type inType)
        {
            Check.NotNull(inType, nameof(inType));

            return GetFieldValue(inType, nameof(Storage<DayOfWeek>.ExplicitConvertersFrom));
        }

        /// <inheritdoc />
        public IEnumerable<Converter> GetExplicitConvertersTo(Type outType)
        {
            Check.NotNull(outType, nameof(outType));

            return GetFieldValue(outType, nameof(Storage<DayOfWeek>.ExplicitConvertersTo));
        }

        /// <inheritdoc />
        public IEnumerable<Converter> GetImplicitConvertersFrom(Type inType)
        {
            Check.NotNull(inType, nameof(inType));

            return GetFieldValue(inType, nameof(Storage<DayOfWeek>.ImplicitConvertersFrom));
        }

        /// <inheritdoc />
        public IEnumerable<Converter> GetImplicitConvertersTo(Type outType)
        {
            Check.NotNull(outType, nameof(outType));

            return GetFieldValue(outType, nameof(Storage<DayOfWeek>.ImplicitConvertersTo));
        }

        private static IEnumerable<Converter> GetFieldValue(Type type, String fieldName)
        {
            var storageType = typeof(Storage<>).MakeGenericType(type);
            var obj = storageType
                .InvokeMember(
                    name: fieldName,
                    invokeAttr: BindingFlags.Public | BindingFlags.Static | BindingFlags.GetField,
                    binder: null,
                    target: null,
                    args: Array.Empty<Object>());
            return (IEnumerable<Converter>)(obj!);
        }

        private static class Storage<T>
        {
            public static readonly IReadOnlyList<Converter> ExplicitConvertersFrom;
            public static readonly IReadOnlyList<Converter> ExplicitConvertersTo;
            public static readonly IReadOnlyList<Converter> ImplicitConvertersFrom;
            public static readonly IReadOnlyList<Converter> ImplicitConvertersTo;

            static Storage()
            {
                var type = typeof(T);

                var list = new List<Converter>();
                foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Static))
                {
                    var converter = CultureInvariantDelegateConverter.TryCreateUserTypeConverter(method);
                    if (converter != null)
                    {
                        list.Add(converter);
                    }
                }

                ExplicitConvertersFrom = Array.AsReadOnly(list.Where(f => f.IsExplicit).Where(f => f.InType.MakeNotNullable() == type).ToArray());
                ExplicitConvertersTo = Array.AsReadOnly(list.Where(f => f.IsExplicit).Where(f => f.OutType.MakeNotNullable() == type).ToArray());
                ImplicitConvertersFrom = Array.AsReadOnly(list.Where(f => !f.IsExplicit).Where(f => f.InType.MakeNotNullable() == type).ToArray());
                ImplicitConvertersTo = Array.AsReadOnly(list.Where(f => !f.IsExplicit).Where(f => f.OutType.MakeNotNullable() == type).ToArray());
            }
        }
    }
}