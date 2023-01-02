// <copyright file="BooleanToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToInt16Converter : Converter<Boolean, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int16 Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? (Int16)1 : (Int16)0;
        }
    }
}
