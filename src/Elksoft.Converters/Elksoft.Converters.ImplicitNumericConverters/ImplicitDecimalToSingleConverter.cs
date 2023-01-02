// <copyright file="ImplicitDecimalToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitDecimalToSingleConverter : Converter<Decimal, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(Decimal value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
