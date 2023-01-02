// <copyright file="ImplicitUInt16ToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt16ToSingleConverter : Converter<UInt16, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(UInt16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
