// <copyright file="ImplicitByteToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitByteToSingleConverter : Converter<Byte, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(Byte value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
