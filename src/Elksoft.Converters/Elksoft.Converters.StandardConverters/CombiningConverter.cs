// <copyright file="CombiningConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StandardConverters
{
    internal sealed class CombiningConverter<TIn, TMid, TOut> : Converter<TIn, TOut>
    {
        public CombiningConverter(Converter<TIn, TMid> firstConverter, Converter<TMid, TOut> secondConverter)
        {
            FirstConverter = Check.NotNull(firstConverter, nameof(firstConverter));
            SecondConverter = Check.NotNull(secondConverter, nameof(secondConverter));
        }

        public override Boolean AcceptsNull => FirstConverter.AcceptsNull;

        public override Boolean IsExplicit => FirstConverter.IsExplicit || SecondConverter.IsExplicit;

        public Converter<TIn, TMid> FirstConverter { get; }

        public Converter<TMid, TOut> SecondConverter { get; }

        public override TOut? Convert(TIn? value, IFormatProvider? formatProvider)
        {
            return SecondConverter.Convert(FirstConverter.Convert(value, formatProvider), formatProvider);
        }
    }
}