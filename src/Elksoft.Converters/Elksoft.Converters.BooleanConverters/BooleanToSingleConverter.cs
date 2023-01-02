// <copyright file="BooleanToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToSingleConverter : Converter<Boolean, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? 1.0f : 0.0f;
        }
    }
}
