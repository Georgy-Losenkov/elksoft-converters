// <copyright file="SingleToBooleanConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.BooleanConverters
{
    internal sealed class SingleToBooleanConverter : Converter<Single, Boolean>
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => false;

        public override Boolean Convert(Single value, IFormatProvider? formatProvider)
        {
            return value != 0.0f;
        }
    }
}
