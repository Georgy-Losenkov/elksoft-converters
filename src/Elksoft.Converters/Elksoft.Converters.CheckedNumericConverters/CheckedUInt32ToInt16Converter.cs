﻿// <copyright file="CheckedUInt32ToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt32ToInt16Converter : Converter<UInt32, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return checked((Int16)value);
        }
    }
}
