// <copyright file="CheckedInt16ToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt16ToUInt16Converter : Converter<Int16, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}
