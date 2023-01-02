// <copyright file="PrimitiveOrEnumConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IPrimitiveOrEnumConverters"/>
    public sealed class PrimitiveOrEnumConverters : IPrimitiveOrEnumConverters
    {
        private readonly IPrimitiveConverters m_primitiveConverters;
        private readonly IEnumConverters m_enumConverters;
        private readonly IIdentityConverters m_identityConverters;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveOrEnumConverters"/> class.
        /// </summary>
        /// <param name="primitiveConverters">Object used for getting a converter from one primitive type to another primitive type.</param>
        /// <param name="enumConverters">Object used for getting converter from <see langword="enum" />
        /// either to its' underlying type or to <see cref="String"/> and vice verce.</param>
        /// <param name="identityConverters">Object used for getting an identity converter.</param>
        public PrimitiveOrEnumConverters(
            IPrimitiveConverters primitiveConverters,
            IEnumConverters enumConverters,
            IIdentityConverters identityConverters)
        {
            m_primitiveConverters = Check.NotNull(primitiveConverters, nameof(primitiveConverters));
            m_enumConverters = Check.NotNull(enumConverters, nameof(enumConverters));
            m_identityConverters = Check.NotNull(identityConverters, nameof(identityConverters));
        }

        /// <inheritdoc />
        public Boolean IsSupported(Type type)
        {
            Check.NotNull(type, nameof(type));

            return m_primitiveConverters.IsSupported(type) || m_enumConverters.IsSupported(type);
        }

        /// <inheritdoc />
        public IEnumerable<Converter> FindConverterChain(Type inType, Type outType)
        {
            Check.NotNull(inType, nameof(inType));
            Check.NotNull(outType, nameof(outType));

            if (inType == outType)
            {
                return new[] { m_identityConverters.GetIdentityConverter(inType) };
            }

            if (m_enumConverters.IsSupported(inType))
            {
                if (outType == Types.String)
                {
                    return new[] { m_enumConverters.GetEnumToString(inType) };
                }
                else if (m_enumConverters.IsSupported(outType))
                {
                    var inEnumToBase = m_enumConverters.GetEnumToUnderlying(inType);
                    var baseToOutEnum = m_enumConverters.GetUnderlyingToEnum(outType);

                    if (inEnumToBase.OutType == baseToOutEnum.InType)
                    {
                        return new[] { inEnumToBase, baseToOutEnum };
                    }

                    var other = m_primitiveConverters.FindConverter(inEnumToBase.OutType, baseToOutEnum.InType);

                    if (other == null)
                    {
                        return Array.Empty<Converter>();
                    }

                    return new[] { inEnumToBase, other, baseToOutEnum };
                }
                else if (m_primitiveConverters.IsSupported(outType))
                {
                    var inEnumToBase = m_enumConverters.GetEnumToUnderlying(inType);

                    if (inEnumToBase.OutType == outType)
                    {
                        return new[] { inEnumToBase };
                    }

                    var other = m_primitiveConverters.FindConverter(inEnumToBase.OutType, outType);

                    if (other == null)
                    {
                        return Array.Empty<Converter>();
                    }

                    return new[] { inEnumToBase, other };
                }

                return Array.Empty<Converter>();
            }
            else if (m_enumConverters.IsSupported(outType))
            {
                if (inType == Types.String)
                {
                    return new[] { m_enumConverters.GetStringToEnum(outType) };
                }
                else if (m_primitiveConverters.IsSupported(inType))
                {
                    var baseToOutEnum = m_enumConverters.GetUnderlyingToEnum(outType);

                    if (outType == baseToOutEnum.InType)
                    {
                        return new[] { baseToOutEnum };
                    }

                    var other = m_primitiveConverters.FindConverter(inType, baseToOutEnum.InType);

                    if (other == null)
                    {
                        return Array.Empty<Converter>();
                    }

                    return new[] { other, baseToOutEnum };
                }

                return Array.Empty<Converter>();
            }
            else
            {
                var converter = m_primitiveConverters.FindConverter(inType, outType);

                if (converter == null)
                {
                    return Array.Empty<Converter>();
                }

                return new[] { converter };
            }
        }
    }
}