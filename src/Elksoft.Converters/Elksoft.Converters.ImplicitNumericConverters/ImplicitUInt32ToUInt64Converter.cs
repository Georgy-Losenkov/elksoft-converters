// <copyright file="ImplicitUInt32ToUInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt32ToUInt64Converter : Converter<UInt32, UInt64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override UInt64 Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return unchecked((UInt64)value);
        }
    }
}
