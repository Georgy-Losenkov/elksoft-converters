// <copyright file="ImplicitSByteToInt32Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitSByteToInt32Converter : Converter<SByte, Int32>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int32 Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((Int32)value);
        }
    }
}
