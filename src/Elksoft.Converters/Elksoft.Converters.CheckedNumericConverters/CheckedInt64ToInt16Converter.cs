// <copyright file="CheckedInt64ToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt64ToInt16Converter : Converter<Int64, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return checked((Int16)value);
        }
    }
}
