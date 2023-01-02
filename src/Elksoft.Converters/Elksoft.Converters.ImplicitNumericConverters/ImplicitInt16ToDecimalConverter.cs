// <copyright file="ImplicitInt16ToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt16ToDecimalConverter : Converter<Int16, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
