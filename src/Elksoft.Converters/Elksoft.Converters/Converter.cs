// <copyright file="Converter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft.Converters
{
    /// <summary>
    /// Provides the non-generic part of the class <see cref="Converter{TIn, TOut}"/>.
    /// </summary>
    public abstract class Converter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Converter"/> class.
        /// </summary>
        protected internal Converter()
        {
        }

        /// <summary>
        /// Gets a value indicating whether converter can convert <see langword="null"/> value.
        /// </summary>
        /// <remarks>
        /// <para>If <see langword="null"/> cannot be passed to converter this value will be ignored.</para>
        /// </remarks>
        public abstract Boolean AcceptsNull { get; }

        /// <summary>
        /// Gets a value indicating whether converter is explicit.
        /// </summary>
        /// <remarks>
        /// <para>Should return <see langword="true"/> if convertion looses information or not all input values of the may throw exceptons for not <see langword="null"/> value.</para>
        /// </remarks>
        public abstract Boolean IsExplicit { get; }

        /// <summary>
        /// Gets the type of object that is to be converted.
        /// </summary>
        public abstract Type InType { get; }

        /// <summary>
        /// Gets the type the input object is to be converted to.
        /// </summary>
        public abstract Type OutType { get; }
    }

    /// <summary>
    /// Provides functionality to convert an object from <typeparamref name="TIn"/> to <typeparamref name="TOut"/>.
    /// </summary>
    /// <typeparam name="TIn">The type of object that is to be converted.</typeparam>
    /// <typeparam name="TOut">The type the input object is to be converted to.</typeparam>
    public abstract class Converter<TIn, TOut> : Converter
    {
        /// <summary>
        /// Gets the type of object that is to be converted.
        /// </summary>
        /// <value>typeof(<typeparamref name="TIn"/>).</value>
        public sealed override Type InType
        {
            get { return typeof(TIn); }
        }

        /// <summary>
        /// Gets the type the input object is to be converted to.
        /// </summary>
        /// <value>typeof(<typeparamref name="TOut"/>).</value>
        public sealed override Type OutType
        {
            get { return typeof(TOut); }
        }

        /// <summary>
        /// Converts an object from input type to output type.
        /// </summary>
        /// <param name="value">The object to convert.</param>
        /// <param name="provider">An object that supplies culture-specific formatting information.</param>
        /// <returns>The <typeparamref name="TOut"/> that represents the converted <typeparamref name="TIn"/>.</returns>
        public abstract TOut? Convert(TIn? value, IFormatProvider? provider);
    }
}