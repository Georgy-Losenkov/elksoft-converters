// <copyright file="IPrimitiveOrEnumOrUserTypeOrNullableConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for finding converter from one type into another type.
    /// </summary>
    public interface IPrimitiveOrEnumOrUserTypeOrNullableConverters
    {
        /// <summary>
        /// Finds converter from <paramref name="inType"/> to <paramref name="outType"/>.
        /// </summary>
        /// <param name="inType">The type of object that is to be converted.</param>
        /// <param name="outType">The type of object that is to be converted to.</param>
        /// <returns>Converter, if convertion from <paramref name="inType"/> to <paramref name="outType"/> is possible; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="inType"/> or <paramref name="outType"/> is <see langword="null"/>.</exception>
        Converter? FindConverter(Type inType, Type outType);
    }
}