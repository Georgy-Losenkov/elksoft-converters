// <copyright file="ImplicitUInt16ToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt16ToUInt64Converter : Converter<UInt16, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt64 Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}
