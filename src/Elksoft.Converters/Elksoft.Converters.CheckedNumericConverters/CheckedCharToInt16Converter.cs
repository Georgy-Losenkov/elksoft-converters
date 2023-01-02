﻿// <copyright file="CheckedCharToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.CheckedNumericConverters
{
    internal sealed class CheckedCharToInt16Converter : Converter<Char, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override Int16 Convert(Char value, IFormatProvider? formatProvider)
        {
            return checked((Int16)value);
        }
    }
}