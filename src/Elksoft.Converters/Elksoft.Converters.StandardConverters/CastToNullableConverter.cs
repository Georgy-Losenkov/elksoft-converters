// <copyright file="CastToNullableConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StandardConverters
{
    internal sealed class CastToNullableConverter<TIn> : Converter<TIn, TIn?>
        where TIn : struct
    {
        public override Boolean AcceptsNull => true;

        public override Boolean IsExplicit => false;

        public override TIn? Convert(TIn value, IFormatProvider? formatProvider)
        {
            return value;
        }
    }
}