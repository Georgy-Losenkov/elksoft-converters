// <copyright file="IUserTypeConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for enumerating converters either to or from specified user type.
    /// </summary>
    public interface IUserTypeConverters
    {
        /// <summary>
        /// Enumerates all explicit operators defined in the user type <paramref name="inType"/>
        /// and for each operator converting from <paramref name="inType"/> returns converter.
        /// </summary>
        /// <param name="inType">The type of object that is to be converted.</param>
        /// <returns>Enumerable containing converter for each explicit operators defined in the type <paramref name="inType"/> that converts from <paramref name="inType"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="inType"/> is <see langword="null"/>.</exception>
        IEnumerable<Converter> GetExplicitConvertersFrom(Type inType);

        /// <summary>
        /// Enumerates all explicit operators defined in the user type <paramref name="outType"/>
        /// and for each operator converting to <paramref name="outType"/> returns converter.
        /// </summary>
        /// <param name="outType">The type the input object is to be converted to.</param>
        /// <returns>Enumerable containing converter for each explicit operators defined in the type <paramref name="outType"/> that converts to <paramref name="outType"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="outType"/> is <see langword="null"/>.</exception>
        IEnumerable<Converter> GetExplicitConvertersTo(Type outType);

        /// <summary>
        /// Enumerates all implicit operators defined in the user type <paramref name="inType"/>
        /// and for each operator converting from <paramref name="inType"/> returns converter.
        /// </summary>
        /// <param name="inType">The type of object that is to be converted.</param>
        /// <returns>Enumerable containing converter for each implicit operators defined in the type <paramref name="inType"/> that converts from <paramref name="inType"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="inType"/> is <see langword="null"/>.</exception>
        IEnumerable<Converter> GetImplicitConvertersFrom(Type inType);

        /// <summary>
        /// Enumerates all implicit operators defined in the user type <paramref name="outType"/>
        /// and for each operator converting to <paramref name="outType"/> returns converter.
        /// </summary>
        /// <param name="outType">The type the input object is to be converted to.</param>
        /// <returns>Enumerable containing converter for each implicit operators defined in the type <paramref name="outType"/> that converts to <paramref name="outType"/>.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="outType"/> is <see langword="null"/>.</exception>
        IEnumerable<Converter> GetImplicitConvertersTo(Type outType);
    }
}