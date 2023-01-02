// <copyright file="IdentityConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StandardConverters
{
    internal sealed class IdentityConverter<TIn> : Converter<TIn, TIn>
    {
        public override Boolean AcceptsNull => true;

        public override Boolean IsExplicit => false;

        public override TIn? Convert(TIn? value, IFormatProvider? formatProvider)
        {
            return value;
        }
    }
}