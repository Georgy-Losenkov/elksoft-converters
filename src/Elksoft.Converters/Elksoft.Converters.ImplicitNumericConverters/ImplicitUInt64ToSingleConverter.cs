// <copyright file="ImplicitUInt64ToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt64ToSingleConverter : Converter<UInt64, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(UInt64 value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
