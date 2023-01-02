// <copyright file="UncheckedSingleToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedSingleToInt16Converter : Converter<Single, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Single value, IFormatProvider? formatProvider)
        {
            return unchecked((Int16)value);
        }
    }
}
