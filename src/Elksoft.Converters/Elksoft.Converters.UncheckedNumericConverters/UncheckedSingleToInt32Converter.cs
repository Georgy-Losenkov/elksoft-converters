// <copyright file="UncheckedSingleToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSingleToInt32Converter : Converter<Single, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(Single value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}
