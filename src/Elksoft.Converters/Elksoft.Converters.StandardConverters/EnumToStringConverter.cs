// <copyright file="EnumToStringConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StandardConverters
{
    internal sealed class EnumToStringConverter<TEnum> : Converter<TEnum, String>
        where TEnum : struct, Enum
    {
        public override Boolean AcceptsNull => false;

        public override Boolean IsExplicit => true;

        public override String? Convert(TEnum value, IFormatProvider? formatProvider)
        {
            return value.ToString();
        }
    }
}