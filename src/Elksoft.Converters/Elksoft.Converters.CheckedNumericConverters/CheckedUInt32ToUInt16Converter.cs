﻿// <copyright file="CheckedUInt32ToUInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedUInt32ToUInt16Converter : Converter<UInt32, UInt16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt16 Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return checked((UInt16)value);
        }
    }
}
