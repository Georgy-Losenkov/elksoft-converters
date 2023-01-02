// <copyright file="VariantConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Elksoft.Converters.StandardConverters
{
    internal sealed class VariantConverter<TOut> : Converter<Object, TOut>
    {
        private readonly ConcurrentDictionary<Type, Converter<Object, TOut>> m_dictionary;

        public VariantConverter(IConverterFinder converterFinder)
        {
            ConverterFinder = Check.NotNull(converterFinder, nameof(converterFinder));
            m_dictionary = new ConcurrentDictionary<Type, Converter<Object, TOut>>();
        }

        public override Boolean AcceptsNull => true;

        public override Boolean IsExplicit => true;

        public IConverterFinder ConverterFinder { get; }

        public override TOut? Convert(Object? value, IFormatProvider? formatProvider)
        {
            if (value == null)
            {
                return default(TOut);
            }

            var converter = GetConverter(value.GetType());
            return converter.Convert(value, formatProvider);
        }

        private Converter<Object, TOut> CreateConverter(Type type)
        {
            if (type == Types.Object)
            {
                throw new InvalidCastException();
            }

            var converter2 = ConverterFinder.FindConverter(type, typeof(TOut));

            if (converter2 == null)
            {
                throw new InvalidCastException();
            }

            var converterType1 = typeof(ObjectDownCastConverter<>).MakeGenericType(type);
            var converter1 = converterType1.InvokeMember(
                name: String.Empty,
                invokeAttr: BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance,
                binder: null,
                target: null,
                args: Array.Empty<Object>());

            var converterType3 = typeof(CombiningConverter<,,>).MakeGenericType(typeof(Object), type, typeof(TOut));

            var converter3 = converterType3.InvokeMember(
                name: String.Empty,
                invokeAttr: BindingFlags.Public | BindingFlags.Instance | BindingFlags.CreateInstance,
                binder: null,
                target: null,
                args: new Object[] { converter1!, converter2! });

            return (Converter<Object, TOut>)converter3!;
        }

        private Converter<Object, TOut> GetConverter(Type type)
        {
            return m_dictionary.GetOrAdd(type, CreateConverter);
        }
    }
}