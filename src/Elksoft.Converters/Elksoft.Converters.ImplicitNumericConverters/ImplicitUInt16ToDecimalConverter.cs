// <copyright file="ImplicitUInt16ToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt16ToDecimalConverter : Converter<UInt16, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
