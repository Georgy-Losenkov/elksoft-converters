﻿// <copyright file="CheckedInt64ToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedInt64ToUInt64Converter : Converter<Int64, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override UInt64 Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return checked((UInt64)value);
        }
    }
}
