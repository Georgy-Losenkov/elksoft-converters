// <copyright file="ImplicitUInt32ToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt32ToDecimalConverter : Converter<UInt32, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
