// <copyright file="ImplicitSByteToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitSByteToDecimalConverter : Converter<SByte, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(SByte value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
