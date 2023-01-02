// <copyright file="IPrimitiveConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for retrieving a converter from one primitive type to another primitive type.
    /// </summary>
    public interface IPrimitiveConverters
    {
        /// <summary>
        /// Finds converter from <paramref name="inType"/> to <paramref name="outType"/>.
        /// </summary>
        /// <param name="inType">The type of object that is to be converted.</param>
        /// <param name="outType">The type of object that is to be converted to.</param>
        /// <returns>Converter, if convertion from <paramref name="inType"/> to <paramref name="outType"/> is possible; otherwise, <see langword="null"/>.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="inType"/> or <paramref name="outType"/> is <see langword="null"/>.</exception>
        Converter? FindConverter(Type inType, Type outType);

        /// <summary>
        /// Determines if type is supported, that is method <see cref="FindConverter(Type, Type)"/> may return not <see langword="null"/>.
        /// </summary>
        /// <param name="type">The type to be checked.</param>
        /// <returns><see langword="true"/> if <paramref name="type"/> is primitive; otherwise <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        Boolean IsSupported(Type type);
    }
}