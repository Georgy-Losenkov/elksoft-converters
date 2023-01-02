// <copyright file="BooleanToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToInt32Converter : Converter<Boolean, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int32 Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? 1 : 0;
        }
    }
}
