// <copyright file="StringToGuidConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class StringToGuidConverter : Converter<String, Guid>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Guid Convert(String? value, IFormatProvider? formatProvider)
        {
            return Guid.Parse(
                Check.NotNull(value, nameof(value)));
        }
    }
}
