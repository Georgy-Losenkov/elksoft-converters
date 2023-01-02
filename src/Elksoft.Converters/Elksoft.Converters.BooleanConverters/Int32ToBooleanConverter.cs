// <copyright file="Int32ToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class Int32ToBooleanConverter : Converter<Int32, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Int32 value, IFormatProvider? formatProvider)
        {
            return value != 0;
        }
    }
}
