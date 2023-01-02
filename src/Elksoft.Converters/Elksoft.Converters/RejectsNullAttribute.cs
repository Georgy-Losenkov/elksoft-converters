// <copyright file="RejectsNullAttribute.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Indicates that method will not handle <see langword="null"/> if it will be passed to method.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class RejectsNullAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RejectsNullAttribute"/> class.
        /// </summary>
        public RejectsNullAttribute()
        {
        }
    }
}