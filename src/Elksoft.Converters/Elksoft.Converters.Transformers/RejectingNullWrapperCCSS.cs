// <copyright file="RejectingNullWrapperCCSS.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.Transformers
{
    internal sealed class RejectingNullWrapperCCSS<TIn, TOut> : Converter<TIn, TOut>
        where TIn : class
        where TOut : struct
    {
        public RejectingNullWrapperCCSS(Converter<TIn, TOut> innerConverter)
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

        public override TOut Convert(TIn? value, IFormatProvider? formatProvider)
        {
            return value != null ? InnerConverter.Convert(value, formatProvider) : default;
        }
    }
}