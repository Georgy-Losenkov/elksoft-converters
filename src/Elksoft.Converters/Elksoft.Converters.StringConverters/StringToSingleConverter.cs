// <copyright file="StringToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToSingleConverter : Converter<String, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Single Convert(String? value, IFormatProvider? formatProvider)
        {
            return Single.Parse(
                Check.NotNull(value, nameof(value)),
                formatProvider);
        }
    }
}
