// <copyright file="ObjectDownCastConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StandardConverters
{
    internal sealed class ObjectDownCastConverter<TOut> : Converter<Object, TOut>
    {
        public override Boolean AcceptsNull => true;

        public override Boolean IsExplicit => true;

        public override TOut? Convert(Object? value, IFormatProvider? formatProvider)
        {
            if (value == null)
            {
                return default(TOut);
            }

            return (TOut)value;
        }
    }
}