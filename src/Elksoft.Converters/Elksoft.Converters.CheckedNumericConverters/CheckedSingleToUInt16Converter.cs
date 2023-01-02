// <copyright file="CheckedSingleToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedSingleToUInt16Converter : Converter<Single, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(Single value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}
