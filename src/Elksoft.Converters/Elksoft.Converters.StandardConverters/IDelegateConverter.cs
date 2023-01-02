// <copyright file="IDelegateConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters.StandardConverters
{
    internal interface IDelegateConverter
    {
        Delegate Func { get; }
    }
}