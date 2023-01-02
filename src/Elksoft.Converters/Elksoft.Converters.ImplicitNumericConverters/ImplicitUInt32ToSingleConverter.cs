// <copyright file="ImplicitUInt32ToSingleConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitUInt32ToSingleConverter : Converter<UInt32, Single>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Single Convert(UInt32 value, IFormatProvider? formatProvider)
        {
            return unchecked((Single)value);
        }
    }
}
