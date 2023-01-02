// <copyright file="IdentityConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using Elksoft.Converters.StandardConverters;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IIdentityConverters"/>
    public class IdentityConverters : IIdentityConverters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityConverters"/> class.
        /// </summary>
        public IdentityConverters()
        {
        }

        /// <inheritdoc/>
        public Converter GetIdentityConverter(Type inType)
        {
            Check.NotNull(inType, nameof(inType));

            var converterType = typeof(IdentityConverter<>).MakeGenericType(inType);
            var converter = Activator.CreateInstance(converterType);
            return (Converter)converter!;
        }
    }
}