// <copyright file="IPrimitiveOrEnumOrUserTypeConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for finding a chain of converters from one primitive or enum or user type to another primitive or enum or user type.
    /// </summary>
    public interface IPrimitiveOrEnumOrUserTypeConverters
    {
        /// <summary>
        /// Finds chain of converters from <paramref name="inType"/> to <paramref name="outType"/>.
        /// </summary>
        /// <param name="inType">The type of object that is to be converted.</param>
        /// <param name="outType">The type of object that is to be converted to.</param>
        /// <returns>Non empty chain of converters, if convertion from <paramref name="inType"/> to <paramref name="outType"/> is possible; otherwise, empty.</returns>
        /// <exception cref="ArgumentNullException">Either <paramref name="inType"/> or <paramref name="outType"/> is <see langword="null"/>.</exception>
        IEnumerable<Converter> FindConverterChain(Type inType, Type outType);
    }
}