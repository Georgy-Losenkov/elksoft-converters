// <copyright file="UInt16ToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class UInt16ToBooleanConverter : Converter<UInt16, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return value != (UInt16)0;
        }
    }
}
