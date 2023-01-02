// <copyright file="ImplicitUInt64ToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt64ToDecimalConverter : Converter<UInt64, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
