// <copyright file="StringToEnumConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StandardConverters
{
    internal sealed class StringToEnumConverter<TEnum> : Converter<String, TEnum>
        where TEnum : struct, Enum
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override TEnum Convert(String? value, IFormatProvider? formatProvider)
        {
            Check.NotNull(value, nameof(value));

            if (Enum.TryParse(value, true, out TEnum result))
            {
                return result;
            }

            throw new InvalidCastException();
        }
    }
}