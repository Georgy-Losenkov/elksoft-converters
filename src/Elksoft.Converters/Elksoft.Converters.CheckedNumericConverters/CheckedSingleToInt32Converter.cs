// <copyright file="CheckedSingleToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSingleToInt32Converter : Converter<Single, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int32 Convert(Single value, IFormatProvider? formatProvider)
        {
            return checked((Int32)value);
        }
    }
}
