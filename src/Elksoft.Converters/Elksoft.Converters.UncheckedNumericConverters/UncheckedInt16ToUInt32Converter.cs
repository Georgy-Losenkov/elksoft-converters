// <copyright file="UncheckedInt16ToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt16ToUInt32Converter : Converter<Int16, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt32)value);
        }
    }
}
