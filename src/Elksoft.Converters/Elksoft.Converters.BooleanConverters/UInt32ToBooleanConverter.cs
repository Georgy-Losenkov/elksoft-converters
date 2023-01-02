// <copyright file="UInt32ToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class UInt32ToBooleanConverter : Converter<UInt32, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return value != 0U;
        }
    }
}
