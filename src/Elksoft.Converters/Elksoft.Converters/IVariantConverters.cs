// <copyright file="IVariantConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides a mechanism for for getting object that converts any object into object of desired type.
    /// </summary>
    public interface IVariantConverters
    {
        /// <summary>
        /// Gets object that converts any object into a <paramref name="outType"/> object.
        /// </summary>
        /// <param name="outType">The type the input object is to be converted to.</param>
        /// <param name="converterFinder">The object used to get converters from specific type to <paramref name="outType"/>.</param>
        /// <exception cref="ArgumentNullException">Either <paramref name="outType"/> or <paramref name="converterFinder"/> is null.</exception>
        /// <returns>Converter any object to a <paramref name="outType"/> object.</returns>
        /// <remarks><para>Resulting converter uses <paramref name="converterFinder"/> to find converter from the type of value to <paramref name="outType"/>.</para></remarks>
        Converter GetVariantConverter(Type outType, IConverterFinder converterFinder);
    }
}