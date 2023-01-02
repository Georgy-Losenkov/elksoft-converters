// <copyright file="ImplicitUInt16ToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt16ToInt32Converter : Converter<UInt16, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int32 Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}
