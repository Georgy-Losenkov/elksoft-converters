// <copyright file="UncheckedSingleToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSingleToUInt32Converter : Converter<Single, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt32 Convert(Single value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt32)value);
        }
    }
}
