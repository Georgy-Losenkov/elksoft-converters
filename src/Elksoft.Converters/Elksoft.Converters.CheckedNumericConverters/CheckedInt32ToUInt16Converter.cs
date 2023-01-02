// <copyright file="CheckedInt32ToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt32ToUInt16Converter : Converter<Int32, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}
