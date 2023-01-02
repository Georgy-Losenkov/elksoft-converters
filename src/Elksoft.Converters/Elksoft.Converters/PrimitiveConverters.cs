// <copyright file="PrimitiveConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IPrimitiveConverters"/>
    public sealed class PrimitiveConverters : IPrimitiveConverters
    {
        private readonly IReadOnlyDictionary<Type, Type> m_supportedTypes = CreateTypesDictionary();
        private readonly IReadOnlyDictionary<ValueTuple<Type, Type>, Converter> m_converters;
        private readonly IIdentityConverters m_identityConverters;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveConverters"/> class.
        /// </summary>
        /// <param name="useCheckedConversions">Use checked conversions for numeric type conversions.</param>
        /// <param name="identityConverters">The object used to get identity converters from type to itself.</param>
        /// <exception cref="ArgumentNullException"><paramref name="identityConverters"/> is <see langword="null"/>.</exception>
        public PrimitiveConverters(Boolean useCheckedConversions, IIdentityConverters identityConverters)
        {
            m_identityConverters = Check.NotNull(identityConverters, nameof(identityConverters));
            m_converters = CreateConvertersDictionary(useCheckedConversions);
        }

        /// <inheritdoc />
        public Boolean IsSupported(Type type)
        {
            Check.NotNull(type, nameof(type));

            return m_supportedTypes.ContainsKey(type);
        }

        /// <inheritdoc />
        public Converter? FindConverter(Type inType, Type outType)
        {
            Check.NotNull(inType, nameof(inType));
            Check.NotNull(outType, nameof(outType));

            if (inType == outType)
            {
                return m_identityConverters.GetIdentityConverter(inType);
            }

            if (m_converters.TryGetValue(ValueTuple.Create(inType, outType), out var converter))
            {
                return converter;
            }

            return null;
        }

        private static IReadOnlyDictionary<Type, Type> CreateTypesDictionary()
        {
            var types = new[] {
                Types.Boolean,
                Types.Byte,
                Types.Binary,
                Types.Char,
                Types.DateOnly,
                Types.DateTime,
                Types.DateTimeOffset,
                Types.Decimal,
                Types.Double,
                Types.Guid,
#if NET7_0_OR_GREATER
                Types.Half,
#endif
                Types.Int16,
                Types.Int32,
                Types.Int64,
#if NET7_0_OR_GREATER
                Types.Int128,
                Types.IntPtr,
#endif
                Types.SByte,
                Types.Single,
                Types.String,
                Types.TimeOnly,
                Types.TimeSpan,
                Types.UInt16,
                Types.UInt32,
                Types.UInt64,
#if NET7_0_OR_GREATER
                Types.UInt128,
                Types.UIntPtr,
#endif
            };

            return new ReadOnlyDictionary<Type, Type>(types.ToDictionary(x => x));
        }

        private static IReadOnlyDictionary<ValueTuple<Type, Type>, Converter> CreateConvertersDictionary(Boolean useCheckedConversions)
        {
            var namespaces = new HashSet<String> {
                "Elksoft.Converters.BinaryConverters",
                "Elksoft.Converters.BooleanConverters",
                "Elksoft.Converters.DateTimeConverters",
                "Elksoft.Converters.ImplicitNumericConverters",
                "Elksoft.Converters.StringConverters",
                useCheckedConversions ? "Elksoft.Converters.CheckedNumericConverters" : "Elksoft.Converters.UncheckedNumericConverters",
            };

            var types = System.Reflection.Assembly.GetExecutingAssembly().GetTypes();

            var converters = new Dictionary<ValueTuple<Type, Type>, Converter>();

            foreach (var type in types)
            {
                if (!namespaces.Contains(type.Namespace!))
                {
                    continue;
                }

                for (var t = type; t != null; t = t.BaseType)
                {
                    if (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Converter<,>))
                    {
                        var instance = Activator.CreateInstance(type);
                        var args = t.GetGenericArguments();

                        converters.Add(ValueTuple.Create(args[0], args[1]), (Converter)instance!);
                        break;
                    }
                }
            }

            return new ReadOnlyDictionary<ValueTuple<Type, Type>, Converter>(converters);
        }
    }
}