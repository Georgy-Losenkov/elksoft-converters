// <copyright file="PrimitiveOrEnumOrUserTypeConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IPrimitiveOrEnumOrUserTypeConverters"/>
    public class PrimitiveOrEnumOrUserTypeConverters : IPrimitiveOrEnumOrUserTypeConverters
    {
        private readonly IPrimitiveOrEnumConverters m_primitiveOrEnumConverters;
        private readonly IUserTypeConverters m_userTypeConverters;
        private readonly IIdentityConverters m_identityConverters;
        private readonly IUpCastConverters m_upCastConverters;

        /// <summary>
        /// Initializes a new instance of the <see cref="PrimitiveOrEnumOrUserTypeConverters"/> class.
        /// </summary>
        /// <param name="primitiveOrEnumConverters">Object used for getting a chain of converters for converting from one primitive or enum type to another primitive or enum type.</param>
        /// <param name="userTypeConverters">Object used for getting converters based on operators defined in the user type.</param>
        /// <param name="identityConverters">Object used for getting an identity converter.</param>
        /// <param name="upCastConverters">Object used for getting a converter converting objects of specified type to its' base type.</param>
        public PrimitiveOrEnumOrUserTypeConverters(
            IPrimitiveOrEnumConverters primitiveOrEnumConverters,
            IUserTypeConverters userTypeConverters,
            IIdentityConverters identityConverters,
            IUpCastConverters upCastConverters)
        {
            m_primitiveOrEnumConverters = Check.NotNull(primitiveOrEnumConverters, nameof(primitiveOrEnumConverters));
            m_userTypeConverters = Check.NotNull(userTypeConverters, nameof(userTypeConverters));
            m_identityConverters = Check.NotNull(identityConverters, nameof(identityConverters));
            m_upCastConverters = Check.NotNull(upCastConverters, nameof(upCastConverters));
        }

        /// <inheritdoc />
        public IEnumerable<Converter> FindConverterChain(Type inType, Type outType)
        {
            Check.NotNull(inType, nameof(inType));
            Check.NotNull(outType, nameof(outType));

            if (TypeExtensions.IsNullable(inType, out _))
            {
                return Array.Empty<Converter>();
            }

            if (TypeExtensions.IsNullable(outType, out _))
            {
                return Array.Empty<Converter>();
            }

            if (inType == outType)
            {
                return new[] { m_identityConverters.GetIdentityConverter(inType) };
            }

            {
                var castToBaseConverter = m_upCastConverters.FindUpCastConverter(inType, outType);
                if (castToBaseConverter != null)
                {
                    return new[] { castToBaseConverter };
                }
            }

            if (m_primitiveOrEnumConverters.IsSupported(inType))
            {
                if (m_primitiveOrEnumConverters.IsSupported(outType))
                {
                    return m_primitiveOrEnumConverters.FindConverterChain(inType, outType);
                }
                else
                {
                    /*
                        one implict conversion is better than two implicit
                        two implict conversions are better than one explicit
                        one explict conversion is better than explicit + implicit
                        two explict conversions are not allowed
                    */
                    var implicitConvertersToOutType = m_userTypeConverters.GetImplicitConvertersTo(outType);
                    var explicitConvertersToOutType = m_userTypeConverters.GetExplicitConvertersTo(outType);

                    foreach (var converterToOutType in implicitConvertersToOutType)
                    {
                        var midTypeCore = converterToOutType.InType.MakeNotNullable();

                        if (midTypeCore == inType)
                        {
                            return new[] { converterToOutType };
                        }
                    }

                    foreach (var converterToOutType in implicitConvertersToOutType)
                    {
                        var midTypeCore = converterToOutType.InType.MakeNotNullable();

                        var chainFromInTypeCore = m_primitiveOrEnumConverters.FindConverterChain(inType, midTypeCore);
                        if (chainFromInTypeCore.Any())
                        {
                            if (chainFromInTypeCore.All(IsImplicit))
                            {
                                return chainFromInTypeCore.Append(converterToOutType).ToArray();
                            }
                        }
                    }

                    foreach (var converterToOutType in explicitConvertersToOutType)
                    {
                        var midTypeCore = converterToOutType.InType.MakeNotNullable();

                        if (midTypeCore == inType)
                        {
                            return new[] { converterToOutType };
                        }
                    }

                    foreach (var converterToOutType in explicitConvertersToOutType)
                    {
                        var midTypeCore = converterToOutType.InType.MakeNotNullable();

                        var chainFromInTypeCore = m_primitiveOrEnumConverters.FindConverterChain(inType, midTypeCore);
                        if (chainFromInTypeCore.Any())
                        {
                            if (chainFromInTypeCore.All(IsImplicit))
                            {
                                return chainFromInTypeCore.Append(converterToOutType).ToArray();
                            }
                        }
                    }
                }
            }
            else
            {
                if (m_primitiveOrEnumConverters.IsSupported(outType))
                {
                    /*
                        one implict conversion is better than two implicit
                        two implict conversions are better than one explicit
                        one explict conversion is better than explicit + implicit
                        two explict conversions are not allowed
                    */
                    var implicitConvertersFromInType = m_userTypeConverters.GetImplicitConvertersFrom(inType);
                    var explicitConvertersFromInType = m_userTypeConverters.GetExplicitConvertersFrom(inType);

                    foreach (var converterFromInType in implicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        if (midTypeCore == outType)
                        {
                            return new[] { converterFromInType };
                        }
                    }

                    foreach (var converterFromInType in implicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        var chainToOutTypeCore = m_primitiveOrEnumConverters.FindConverterChain(midTypeCore, outType);
                        if (chainToOutTypeCore.Any())
                        {
                            if (chainToOutTypeCore.All(IsImplicit))
                            {
                                return chainToOutTypeCore.Prepend(converterFromInType).ToArray();
                            }
                        }
                    }

                    foreach (var converterFromInType in explicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        if (midTypeCore == outType)
                        {
                            return new[] { converterFromInType };
                        }
                    }

                    foreach (var converterFromInType in explicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        var chainToOutTypeCore = m_primitiveOrEnumConverters.FindConverterChain(midTypeCore, outType);
                        if (chainToOutTypeCore.Any())
                        {
                            if (chainToOutTypeCore.All(IsImplicit))
                            {
                                return chainToOutTypeCore.Prepend(converterFromInType).ToArray();
                            }
                        }
                    }
                }
                else
                {
                    /*
                        one implict conversion is better than two implicit
                        two implict conversions are better than one explicit
                        one explict conversion is better than explicit + implicit
                        two explict conversions are not allowed
                    */
                    var implicitConvertersToOutType = m_userTypeConverters.GetImplicitConvertersTo(outType);
                    var explicitConvertersToOutType = m_userTypeConverters.GetExplicitConvertersTo(outType);
                    var implicitConvertersFromInType = m_userTypeConverters.GetImplicitConvertersFrom(inType);
                    var explicitConvertersFromInType = m_userTypeConverters.GetExplicitConvertersFrom(inType);

                    foreach (var converterFromInType in implicitConvertersFromInType)
                    {
                        if (converterFromInType.OutType.MakeNotNullable() == outType)
                        {
                            return new[] { converterFromInType };
                        }
                    }

                    foreach (var converterToOutType in implicitConvertersToOutType)
                    {
                        if (inType == converterToOutType.InType.MakeNotNullable())
                        {
                            return new[] { converterToOutType };
                        }
                    }

                    foreach (var converterFromInType in implicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        var converter = m_upCastConverters.FindUpCastConverter(midTypeCore, outType);
                        if (converter != null)
                        {
                            return new[] { converterFromInType, converter };
                        }
                    }

                    foreach (var converterToOutType in implicitConvertersToOutType)
                    {
                        var midTypeCore = converterToOutType.InType.MakeNotNullable();

                        var converter = m_upCastConverters.FindUpCastConverter(inType, midTypeCore);
                        if (converter != null)
                        {
                            return new[] { converter, converterToOutType };
                        }
                    }

                    foreach (var converterFromInType in implicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        foreach (var converterToOutType in implicitConvertersToOutType)
                        {
                            if (midTypeCore == converterToOutType.InType.MakeNotNullable())
                            {
                                return new[] { converterFromInType, converterToOutType };
                            }
                        }
                    }

                    foreach (var converterFromInType in explicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        if (midTypeCore == outType)
                        {
                            return new[] { converterFromInType };
                        }
                    }

                    foreach (var converterToOutType in explicitConvertersToOutType)
                    {
                        var midTypeCore = converterToOutType.InType.MakeNotNullable();

                        if (midTypeCore == inType)
                        {
                            return new[] { converterToOutType };
                        }
                    }

                    foreach (var converterFromInType in explicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        var converter = m_upCastConverters.FindUpCastConverter(midTypeCore, outType);
                        if (converter != null)
                        {
                            return new[] { converterFromInType, converter };
                        }
                    }

                    foreach (var converterToOutType in explicitConvertersToOutType)
                    {
                        var midTypeCore = converterToOutType.InType.MakeNotNullable();

                        var converter = m_upCastConverters.FindUpCastConverter(inType, midTypeCore);
                        if (converter != null)
                        {
                            return new[] { converter, converterToOutType };
                        }
                    }

                    foreach (var converterFromInType in explicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        foreach (var converterToOutType in m_userTypeConverters.GetImplicitConvertersTo(outType))
                        {
                            if (midTypeCore == converterToOutType.InType.MakeNotNullable())
                            {
                                return new[] { converterFromInType, converterToOutType };
                            }
                        }
                    }

                    foreach (var converterFromInType in implicitConvertersFromInType)
                    {
                        var midTypeCore = converterFromInType.OutType.MakeNotNullable();

                        foreach (var converterToOutType in explicitConvertersToOutType)
                        {
                            if (midTypeCore == converterToOutType.InType.MakeNotNullable())
                            {
                                return new[] { converterFromInType, converterToOutType };
                            }
                        }
                    }
                }
            }

            return Array.Empty<Converter>();
        }

        private static Boolean IsImplicit(Converter converter)
        {
            return !converter.IsExplicit;
        }
    }
}