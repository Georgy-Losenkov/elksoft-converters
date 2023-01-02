// <copyright file="CheckedInt64ToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt64ToUInt16Converter : Converter<Int64, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}
