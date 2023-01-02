// <copyright file="PrimitiveOrEnumOrUserTypeOrNullableConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Linq;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IPrimitiveOrEnumOrUserTypeOrNullableConverters"/>
    public class PrimitiveOrEnumOrUserTypeOrNullableConverters : IPrimitiveOrEnumOrUserTypeOrNullableConverters
    {
        private readonly IPrimitiveOrEnumOrUserTypeConverters m_primitiveOrEnumOrUserTypeConverters;
        private readonly IIdentityConverters m_identityConverters;
        private readonly ICombiner m_combiner;
        private readonly INullableWrapper m_nullableWrapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveOrEnumOrUserTypeOrNullableConverters"/> class.
        /// </summary>
        /// <param name="primitiveOrEnumOrUserTypeConverters">Object used for getting a chain of converters for converting from one primitive or enum or user type to another primitive or enum or user type.</param>
        /// <param name="identityConverters">Object used for getting an identity converter.</param>
        /// <param name="combiner">Object used for combining converters.</param>
        /// <param name="nullableWrapper">Object used for changing nullability of the input and output types of the converter.</param>
        public PrimitiveOrEnumOrUserTypeOrNullableConverters(
            IPrimitiveOrEnumOrUserTypeConverters primitiveOrEnumOrUserTypeConverters,
            IIdentityConverters identityConverters,
            ICombiner combiner,
            INullableWrapper nullableWrapper)
        {
            m_primitiveOrEnumOrUserTypeConverters = Check.NotNull(primitiveOrEnumOrUserTypeConverters, nameof(primitiveOrEnumOrUserTypeConverters));
            m_identityConverters = Check.NotNull(identityConverters, nameof(identityConverters));
            m_combiner = Check.NotNull(combiner, nameof(combiner));
            m_nullableWrapper = Check.NotNull(nullableWrapper, nameof(nullableWrapper));
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

            var inTypeCore = inType.MakeNotNullable();
            var outTypeCore = outType.MakeNotNullable();

            var chain = m_primitiveOrEnumOrUserTypeConverters.FindConverterChain(inTypeCore, outTypeCore);
            if (chain == null || !chain.Any())
            {
                return null;
            }

            var list = chain.ToList();

            // combine all we can
            for (var i = 1; i < list.Count;)
            {
                if (NullableWrapper.GetWrapKind(list[i - 1].OutType, list[i].InType) == NullableWrapper.WrapKind.SS)
                {
                    list[i - 1] = m_combiner.Combine(list[i - 1], list[i]);
                    list.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }

            // wrap each converter in chain
            for (var i = 0; i < list.Count; i++)
            {
                var localInType = (0 < i) ? list[i].InType.MakeNullable() : inType;
                var localOutType = (i + 1 < list.Count) ? list[i].OutType.MakeNullable() : outType;

                list[i] = m_nullableWrapper.Wrap(localInType, list[i], localOutType);
            }

            var result = list[0];
            for (var i = 1; i < list.Count; i++)
            {
                result = m_combiner.Combine(result, list[i]);
            }

            return result;
        }
    }
}