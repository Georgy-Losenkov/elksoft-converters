// <copyright file="ByteToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class ByteToBooleanConverter : Converter<Byte, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Byte value, IFormatProvider? formatProvider)
        {
            return value != (Byte)0;
        }
    }
}
