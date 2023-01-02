// <copyright file="ImplicitSByteToInt16Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitSByteToInt16Converter : Converter<SByte, Int16>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int16 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((Int16)value);
        }
    }
}
