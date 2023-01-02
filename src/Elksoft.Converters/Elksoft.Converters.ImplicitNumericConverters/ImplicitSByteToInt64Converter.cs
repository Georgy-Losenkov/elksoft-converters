// <copyright file="ImplicitSByteToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitSByteToInt64Converter : Converter<SByte, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int64 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}
