// <copyright file="ImplicitInt64ToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt64ToDecimalConverter : Converter<Int64, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
