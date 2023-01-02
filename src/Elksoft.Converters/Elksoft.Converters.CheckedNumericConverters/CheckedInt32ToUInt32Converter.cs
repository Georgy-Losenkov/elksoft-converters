// <copyright file="CheckedInt32ToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt32ToUInt32Converter : Converter<Int32, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return checked((UInt32)value);
        }
    }
}
