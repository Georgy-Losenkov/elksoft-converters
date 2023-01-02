// <copyright file="ImplicitDoubleToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitDoubleToSingleConverter : Converter<Double, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(Double value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
