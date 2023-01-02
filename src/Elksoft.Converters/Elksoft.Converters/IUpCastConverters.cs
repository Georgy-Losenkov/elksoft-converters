// <copyright file="IUpCastConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for creating converters from specified type to its' base type.
    /// </summary>
    public interface IUpCastConverters
    {
        /// <summary>
        /// Creates object that converts a <paramref name="inType"/> object to a <paramref name="outType"/> object.
        /// </summary>
        /// <param name="inType">The type of object that is to be converted.</param>
        /// <param name="outType">The type the input object is to be converted to.</param>
        /// <returns>Converter if <paramref name="outType"/> is either equal to <paramref name="inType"/>
        /// or <paramref name="outType"/> is base class for <paramref name="inType"/>
        /// or <paramref name="outType"/> is interface and <paramref name="inType"/> implements it.
        /// Otherwise <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="inType"/> or <paramref name="outType"/> is <see langword="null"/>.</exception>
        Converter? FindUpCastConverter(Type inType, Type outType);
    }
}