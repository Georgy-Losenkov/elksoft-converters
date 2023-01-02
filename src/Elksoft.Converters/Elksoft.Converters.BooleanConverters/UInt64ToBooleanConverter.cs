// <copyright file="UInt64ToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class UInt64ToBooleanConverter : Converter<UInt64, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return value != 0UL;
        }
    }
}
