// <copyright file="Combiner.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using Elksoft.Converters.StandardConverters;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="ICombiner" />
    public sealed class Combiner : ICombiner
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Combiner"/> class.
        /// </summary>
        public Combiner()
        {
        }

        /// <inheritdoc />
        public Converter Combine(Converter firstConverter, Converter secondConverter)
        {
            Check.NotNull(firstConverter, nameof(firstConverter));
            Check.NotNull(secondConverter, nameof(secondConverter));

            if (firstConverter.OutType != secondConverter.InType)
            {
                throw new ArgumentException("Input type of the second converter must coincide with the output type of the first converter", nameof(secondConverter));
            }

            var type = typeof(CombiningConverter<,,>).MakeGenericType(firstConverter.InType, firstConverter.OutType, secondConverter.OutType);

            var converter = Activator.CreateInstance(type, firstConverter, secondConverter);

            return (Converter)(converter!);
        }
    }
}