// <copyright file="Int16ToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class Int16ToBooleanConverter : Converter<Int16, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Int16 value, IFormatProvider? formatProvider)
        {
            return value != (Int16)0;
        }
    }
}
