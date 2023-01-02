// <copyright file="BooleanToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class BooleanToUInt32Converter : Converter<Boolean, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt32 Convert(Boolean value, IFormatProvider? formatProvider)
        {
            return value ? 1U : 0U;
        }
    }
}
