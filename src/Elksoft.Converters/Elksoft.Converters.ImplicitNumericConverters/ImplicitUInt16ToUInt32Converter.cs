// <copyright file="ImplicitUInt16ToUInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt16ToUInt32Converter : Converter<UInt16, UInt32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt32 Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt32)value);
        }
    }
}
