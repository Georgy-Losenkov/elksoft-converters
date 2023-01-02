// <copyright file="CommonWrapperNSSN.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.Transformers
{
    internal sealed class CommonWrapperNSSN<TIn, TOut> : Converter<TIn?, TOut?>
        where TIn : struct
        where TOut : struct
    {
        public CommonWrapperNSSN(Converter<TIn, TOut> innerConverter)
        {
            InnerConverter = Check.NotNull(innerConverter, nameof(innerConverter));
        }

        public override Boolean AcceptsNull
        {
            get { return true; }
        }

        public override Boolean IsExplicit
        {
            get { return InnerConverter.IsExplicit; }
        }

        public Converter<TIn, TOut> InnerConverter { get; }

        public override TOut? Convert(TIn? value, IFormatProvider? formatProvider)
        {
            return value.HasValue ? InnerConverter.Convert(value.GetValueOrDefault(), formatProvider) : default(TOut?);
        }
    }
}