// <copyright file="IPrimitiveOrEnumConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for retrieving a chain of converters for converting from one primitive or enum type to another primitive or enum type.
    /// </summary>
    public interface IPrimitiveOrEnumConverters
    {
        /// <summary>
        /// Finds chain of converters from <paramref name="inType"/> to <paramref name="outType"/>.
        /// </summary>
        /// <param name="inType">The type of object that is to be converted.</param>
        /// <param name="outType">The type of object that is to be converted to.</param>
        /// <returns>Non empty chain of converters, if convertion from <paramref name="inType"/> to <paramref name="outType"/> is possible; otherwise, empty.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="inType"/> or <paramref name="outType"/> is <see langword="null"/>.</exception>
        IEnumerable<Converter> FindConverterChain(Type inType, Type outType);

        /// <summary>
        /// Determines if type is supported, that is method <see cref="FindConverterChain(Type, Type)"/> may return not empty chain of converters.
        /// </summary>
        /// <param name="type">The type to be checked.</param>
        /// <returns><see langword="true"/> if <paramref name="type"/> is either primitive or enum; otherwise <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="type"/> is <see langword="null"/>.</exception>
        Boolean IsSupported(Type type);
    }
}