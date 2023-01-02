// <copyright file="DefaultConverterFinder.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Concurrent;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IConverterFinder"/>
    public class DefaultConverterFinder : IConverterFinder
    {
        private readonly IPrimitiveOrEnumOrUserTypeOrNullableConverters m_primitiveOrEnumOrUserTypeOrNullableConverters;
        private readonly IIdentityConverters m_identityConverters;
        private readonly IVariantConverters m_variantConverters;
        private readonly ConcurrentDictionary<ValueTuple<Type, Type>, Converter> m_cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConverterFinder"/> class.
        /// </summary>
        /// <param name="primitiveOrEnumOrUserTypeOrNullableConverters">An оbject that provides a mechanism for getting object that converts objects of one type into objects of another type.</param>
        /// <param name="identityConverters">An оbject that provides a mechanism for getting an identity converter.</param>
        /// <param name="variantConverters">An оbject that provides a mechanism for getting object that converts any object into object of desired type.</param>
        /// <exception cref="ArgumentNullException">Either <paramref name="primitiveOrEnumOrUserTypeOrNullableConverters"/> is <see langword="null"/>
        /// or <paramref name="identityConverters"/> is <see langword="null"/> or <paramref name="variantConverters"/> is <see langword="null"/>.</exception>
        public DefaultConverterFinder(
            IPrimitiveOrEnumOrUserTypeOrNullableConverters primitiveOrEnumOrUserTypeOrNullableConverters,
            IIdentityConverters identityConverters,
            IVariantConverters variantConverters)
        {
            m_primitiveOrEnumOrUserTypeOrNullableConverters = Check.NotNull(primitiveOrEnumOrUserTypeOrNullableConverters, nameof(primitiveOrEnumOrUserTypeOrNullableConverters));
            m_identityConverters = Check.NotNull(identityConverters, nameof(identityConverters));
            m_variantConverters = Check.NotNull(variantConverters, nameof(variantConverters));
            m_cache = new ConcurrentDictionary<ValueTuple<Type, Type>, Converter>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultConverterFinder"/> class.
        /// </summary>
        /// <param name="useCheckedConversions">Use checked conversions for numeric type conversions.</param>
        /// <returns>Object providing a mechanism for finding a converter from one type to another type.</returns>
        public static DefaultConverterFinder Create(Boolean useCheckedConversions)
        {
            var identityConverters = new IdentityConverters();

            var primitiveOrEnumOrUserTypeOrNullableConverters = new PrimitiveOrEnumOrUserTypeOrNullableConverters(
                new PrimitiveOrEnumOrUserTypeConverters(
                    new PrimitiveOrEnumConverters(
                        new PrimitiveConverters(useCheckedConversions, identityConverters),
                        new EnumConverters(),
                        identityConverters),
                    new UserTypeConverters(),
                    identityConverters,
                    new UpCastConverters()),
                identityConverters,
                new Combiner(),
                new NullableWrapper());

            var variantConverters = new VariantConverters();

            return new DefaultConverterFinder(
                primitiveOrEnumOrUserTypeOrNullableConverters,
                identityConverters,
                variantConverters);
        }

        /// <inheritdoc />
        public Converter<TIn, TOut>? FindConverter<TIn, TOut>()
        {
            var key = ValueTuple.Create(typeof(TIn), typeof(TOut));
            var cached = m_cache.GetOrAdd(key, InnerFindConverter);
            return (Converter<TIn, TOut>)cached;
        }

        /// <inheritdoc />
        public Converter? FindConverter(Type inType, Type outType)
        {
            Check.NotNull(inType, nameof(inType));
            Check.NotNull(outType, nameof(outType));

            var key = ValueTuple.Create(inType, outType);
            return m_cache.GetOrAdd(key, InnerFindConverter);
        }

#nullable disable
        private Converter InnerFindConverter(ValueTuple<Type, Type> tuple)
        {
            if (tuple.Item1 == tuple.Item2)
            {
                return m_identityConverters.GetIdentityConverter(tuple.Item1);
            }

            if (tuple.Item1 == Types.Object)
            {
                return m_variantConverters.GetVariantConverter(tuple.Item2, this);
            }

            return m_primitiveOrEnumOrUserTypeOrNullableConverters.FindConverter(tuple.Item1, tuple.Item2);
        }
#nullable enable
    }
}