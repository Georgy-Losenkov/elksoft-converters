// <copyright file="SByteToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class SByteToBooleanConverter : Converter<SByte, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(SByte value, IFormatProvider? formatProvider)
        {
            return value != (SByte)0;
        }
    }
}
