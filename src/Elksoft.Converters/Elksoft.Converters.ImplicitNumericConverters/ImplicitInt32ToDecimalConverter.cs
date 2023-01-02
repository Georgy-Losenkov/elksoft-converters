// <copyright file="ImplicitInt32ToDecimalConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt32ToDecimalConverter : Converter<Int32, Decimal>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Decimal Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Decimal)value);
        }
    }
}
