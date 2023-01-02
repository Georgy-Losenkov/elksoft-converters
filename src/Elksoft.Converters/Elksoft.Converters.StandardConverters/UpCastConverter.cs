// <copyright file="UpCastConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StandardConverters
{
    internal sealed class UpCastConverter<TIn, TOut> : Converter<TIn, TOut>
        where TIn : TOut
    {
        public override Boolean AcceptsNull => true;

        public override Boolean IsExplicit => false;

        public override TOut? Convert(TIn? value, IFormatProvider? formatProvider)
        {
            return value;
        }
    }
}