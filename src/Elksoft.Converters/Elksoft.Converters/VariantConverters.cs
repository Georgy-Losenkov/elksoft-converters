// <copyright file="VariantConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using Elksoft.Converters.StandardConverters;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IVariantConverters"/>
    public class VariantConverters : IVariantConverters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VariantConverters"/> class.
        /// </summary>
        public VariantConverters()
        {
        }

        /// <inheritdoc />
        public Converter GetVariantConverter(Type outType, IConverterFinder converterFinder)
        {
            Check.NotNull(outType, nameof(outType));
            Check.NotNull(converterFinder, nameof(converterFinder));

            var converterType = typeof(VariantConverter<>).MakeGenericType(outType);
            var converter = Activator.CreateInstance(converterType, converterFinder);
            return (Converter)(converter!);
        }
    }
}