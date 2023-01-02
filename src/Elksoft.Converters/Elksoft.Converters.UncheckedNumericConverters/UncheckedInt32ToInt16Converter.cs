// <copyright file="UncheckedInt32ToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    internal sealed class UncheckedInt32ToInt16Converter : Converter<Int32, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int16)value);
        }
    }
}
