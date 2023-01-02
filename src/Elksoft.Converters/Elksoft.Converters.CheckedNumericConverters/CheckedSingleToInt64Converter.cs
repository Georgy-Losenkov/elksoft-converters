// <copyright file="CheckedSingleToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSingleToInt64Converter : Converter<Single, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int64 Convert(Single value, IFormatProvider? formatProvider)
        {
            return checked((Int64)value);
        }
    }
}
