// <copyright file="IIdentityConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for getting an identity converter (<c>x => x</c>).
    /// </summary>
    public interface IIdentityConverters
    {
        /// <summary>Returns an identity <see cref="Converter"/>.</summary>
        /// <param name="inType">An <see cref="Type"/> that specifies the type of format object to return.</param>
        /// <returns>An instance of the <see cref="Converter"/> object that passes values of the <paramref name="inType"/> through.</returns>
        Converter GetIdentityConverter(Type inType);
    }
}