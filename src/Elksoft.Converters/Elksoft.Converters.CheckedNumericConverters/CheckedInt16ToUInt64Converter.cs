// <copyright file="CheckedInt16ToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt16ToUInt64Converter : Converter<Int16, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return checked((UInt64)value);
        }
    }
}
