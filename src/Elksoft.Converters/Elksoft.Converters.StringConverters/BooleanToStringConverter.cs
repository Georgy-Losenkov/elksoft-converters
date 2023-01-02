// <copyright file="BooleanToStringConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class BooleanToStringConverter : Converter<Boolean, String>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override String? Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value.ToString();
        }
    }
}
