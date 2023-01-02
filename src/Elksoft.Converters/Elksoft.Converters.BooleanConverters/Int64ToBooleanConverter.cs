// <copyright file="Int64ToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class Int64ToBooleanConverter : Converter<Int64, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Int64 value, IFormatProvider? formatProvider)
        {
            return value != 0L;
        }
    }
}
