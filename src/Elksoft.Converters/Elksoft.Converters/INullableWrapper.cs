// <copyright file="INullableWrapper.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for changing nullability of the input and output types of the converter.
    /// </summary>
    public interface INullableWrapper
    {
        /// <summary>
        /// Transforms passed converter into converter that can convert <see langword="null"/> if <paramref name="inType"/> allows such value
        /// and which <see cref="Converter.InType"/> equals to <paramref name="inType"/> and <see cref="Converter.OutType"/> equals to <paramref name="outType"/>.
        /// </summary>
        /// <param name="inType">Desired input type of the resulting converter.</param>
        /// <param name="converter">Object to be transformed.</param>
        /// <param name="outType">Desired output type of the resulting converter.</param>
        /// <returns>Converter which <see cref="Converter.InType"/> equals to <paramref name="inType"/>,
        /// <see cref="Converter.OutType"/> equals to <paramref name="outType"/> and <see cref="Converter.OutType"/> equals to <see langword="true"/>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="inType"/> or the <paramref name="converter"/> the <paramref name="outType"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">Either <paramref name="inType"/> is not compatible with <paramref name="converter"/>.<see cref="Converter.InType"/>
        /// or <paramref name="outType"/> is not compatible with <paramref name="converter"/>.<see cref="Converter.OutType"/>.</exception>
        /// <remarks>
        /// <para><paramref name="inType"/> must be compatible with the <see cref="Converter.InType" /> of the <paramref name="converter"/>.</para>
        /// <para><paramref name="outType"/> must be compatible with the <see cref="Converter.OutType" /> of the <paramref name="converter"/>.</para>
        /// <para>Two types T1 and T2 are compatible if T1 = T2 or T1 = T2? or T2 = T1?.</para></remarks>
        Converter Wrap(Type inType, Converter converter, Type outType);
    }
}