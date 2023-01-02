// <copyright file="UpCastConverters.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using Elksoft.Converters.StandardConverters;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="IUpCastConverters"/>
    public class UpCastConverters : IUpCastConverters
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UpCastConverters"/> class.
        /// </summary>
        public UpCastConverters()
        {
        }

        /// <inheritdoc/>
        public Converter? FindUpCastConverter(Type inType, Type outType)
        {
            Check.NotNull(inType, nameof(inType));
            Check.NotNull(outType, nameof(outType));

            if (inType == outType
                || inType.IsSubclassOf(outType)
                || (outType.IsInterface && inType.ImplementInterface(outType)))
            {
                var converterType = typeof(UpCastConverter<,>).MakeGenericType(inType, outType);
                var converter = Activator.CreateInstance(converterType);
                return (Converter)converter!;
            }

            return null;
        }
    }
}