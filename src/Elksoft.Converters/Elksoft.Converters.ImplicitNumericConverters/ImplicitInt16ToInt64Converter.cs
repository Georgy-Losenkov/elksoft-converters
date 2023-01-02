// <copyright file="ImplicitInt16ToInt64Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    internal sealed class ImplicitInt16ToInt64Converter : Converter<Int16, Int64>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Int64 Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return unchecked((Int64)value);
        }
    }
}
