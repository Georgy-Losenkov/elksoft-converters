// <copyright file="ICombiner.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for combining converters.
    /// </summary>
    public interface ICombiner
    {
        /// <summary>
        /// Creates converter that acts as composition of converters <paramref name="firstConverter"/> and <paramref name="secondConverter"/>.
        /// </summary>
        /// <param name="firstConverter">Converter that will be applied first.</param>
        /// <param name="secondConverter">Converter that will be applied second.</param>
        /// <returns>Converter that acts as composition of converters created by the <paramref name="firstConverter"/> and the <paramref name="secondConverter"/>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="firstConverter"/> or the <paramref name="secondConverter"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">
        /// The <paramref name="secondConverter"/>.<see cref="Converter.InType"/> is not same as <paramref name="firstConverter"/>.<see cref="Converter.OutType"/>.
        /// </exception>
        Converter Combine(Converter firstConverter, Converter secondConverter);
    }
}