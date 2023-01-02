// <copyright file="CheckedUInt64ToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt64ToUInt32Converter : Converter<UInt64, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return checked((UInt32)value);
        }
    }
}
