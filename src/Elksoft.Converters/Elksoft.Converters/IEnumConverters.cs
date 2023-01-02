// <copyright file="IEnumConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for retrieving converter from <see langword="enum" />
    /// either to its' underlying type or to <see cref="String"/> and vice verce.
    /// </summary>
    public interface IEnumConverters
    {
        /// <summary>Checks if specified <see cref="Type"/> is supported.</summary>
        /// <param name="type"><see cref="Type"/> to be checked.</param>
        /// <returns><see langword="true"/> if <paramref name="type"/> is <see langword="enum"/>, <see langword="false"/> otherwise.</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="type"/> is <see langword="null"/>.</exception>
        Boolean IsSupported(Type type);

        /// <summary>Gets converter from underlying type of <paramref name="enumType"/> to <paramref name="enumType"/> itself.</summary>
        /// <param name="enumType"><see langword="enum"/> <see cref="Type"/> for which we getting converter.</param>
        /// <returns>Converter from underlying type of <paramref name="enumType"/> to <paramref name="enumType"/> itself.</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="enumType"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">if <paramref name="enumType"/> is not <see langword="enum"/>.</exception>
        Converter GetUnderlyingToEnum(Type enumType);

        /// <summary>Gets converter from <paramref name="enumType"/> to underlying type of <paramref name="enumType"/>.</summary>
        /// <param name="enumType"><see langword="enum"/> <see cref="Type"/> for which we getting converter.</param>
        /// <returns>Converter from <paramref name="enumType"/> to underlying type of <paramref name="enumType"/>.</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="enumType"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">if <paramref name="enumType"/> is not <see langword="enum"/>.</exception>
        Converter GetEnumToUnderlying(Type enumType);

        /// <summary>Gets converter from <paramref name="enumType"/> to <see cref="String"/>.</summary>
        /// <param name="enumType"><see langword="enum"/> <see cref="Type"/> for which we getting converter.</param>
        /// <returns>Converter from <paramref name="enumType"/> to <see cref="String"/> based on the method <see cref="Enum.ToString()"/>.</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="enumType"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">if <paramref name="enumType"/> is not <see langword="enum"/>.</exception>
        Converter GetEnumToString(Type enumType);

        /// <summary>Gets converter from <see cref="String"/> to <paramref name="enumType"/>.</summary>
        /// <param name="enumType"><see langword="enum"/> <see cref="Type"/> for which we getting converter.</param>
        /// <returns>Converter from <see cref="String"/> to <paramref name="enumType"/> based on <see cref="Enum.TryParse{TEnum}(String, Boolean, out TEnum)"/>.</returns>
        /// <exception cref="ArgumentNullException">if <paramref name="enumType"/> is <see langword="null"/>.</exception>
        /// <exception cref="ArgumentException">if <paramref name="enumType"/> is not <see langword="enum"/>.</exception>
        Converter GetStringToEnum(Type enumType);
    }
}