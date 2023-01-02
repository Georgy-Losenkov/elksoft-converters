// <copyright file="UInt128ToStringConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

#if NET7_0_OR_GREATER
using System;

namespace Elksoft.Converters.StringConverters
{
    internal sealed class UInt128ToStringConverter : Converter<UInt128, String>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override String? Convert(UInt128 value, IFormatProvider? formatProvider)
        {
            return value.ToString(null, formatProvider);
        }
    }
}
#endif
