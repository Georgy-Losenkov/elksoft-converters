// <copyright file="BooleanToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToUInt16Converter : Converter<Boolean, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt16 Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? (UInt16)1 : (UInt16)0;
        }
    }
}
