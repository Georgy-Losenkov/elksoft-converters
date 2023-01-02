// <copyright file="CultureInvariantDelegateConverter.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Reflection;

namespace Elksoft.Converters.StandardConverters
{
    /// <summary>
    /// Converter converter that uses provided delegate for converting values from <typeparamref name="TIn"/> into <typeparamref name="TOut"/>.
    /// </summary>
    /// <typeparam name="TIn">The type of object that is to be converted.</typeparam>
    /// <typeparam name="TOut">The type the input object is to be converted to.</typeparam>
    public sealed class CultureInvariantDelegateConverter<TIn, TOut> : Converter<TIn, TOut>, IDelegateConverter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CultureInvariantDelegateConverter{TIn, TOut}"/> class.
        /// </summary>
        /// <param name="func">The delegate to be used as converter.</param>
        /// <param name="isExplicit">The value to be returned by <see cref="IsExplicit"/> property.</param>
        /// <param name="acceptsNull">The value to be returned by <see cref="AcceptsNull"/> property.</param>
        /// <exception cref="ArgumentNullException"><paramref name="func"/> is <see langword="null"/>.</exception>
        public CultureInvariantDelegateConverter(Func<TIn?, TOut?> func, Boolean isExplicit, Boolean acceptsNull)
        {
            Func = Check.NotNull(func, nameof(func));
            IsExplicit = isExplicit;
            AcceptsNull = acceptsNull;
        }

        /// <inheritdoc/>
        public override Boolean AcceptsNull { get; }

        /// <inheritdoc/>
        public override Boolean IsExplicit { get; }

        /// <summary>
        /// Gets the delegate used as converter.
        /// </summary>
        public Func<TIn?, TOut?> Func { get; }

        /// <inheritdoc/>
        Delegate IDelegateConverter.Func
        {
            get { return Func; }
        }

        /// <inheritdoc/>
        public override TOut? Convert(TIn? value, IFormatProvider? formatProvider)
        {
            return Func(value);
        }
    }

    internal static class CultureInvariantDelegateConverter
    {
        internal static Converter? TryCreateUserTypeConverter(MethodInfo method)
        {
            Check.NotNull(method, nameof(method));

            if (method.IsGenericMethod || method.IsGenericMethodDefinition || !method.IsStatic)
            {
                return null;
            }

            if (method.ReturnType == typeof(void) || method.ReturnType.IsByRef)
            {
                return null;
            }

            var parameters = method.GetParameters();
            if (parameters.Length != 1)
            {
                return null;
            }

            var parameter = parameters[0];
            if (parameter.ParameterType.IsByRef)
            {
                return null;
            }

            if (method.Name != "op_Implicit" && method.Name != "op_Explicit")
            {
                return null;
            }

            var converterType = typeof(CultureInvariantDelegateConverter<,>).MakeGenericType(parameter.ParameterType, method.ReturnType);
            var func = Delegate.CreateDelegate(
                type: typeof(Func<,>).MakeGenericType(parameter.ParameterType, method.ReturnType),
                firstArgument: null,
                method: method,
                throwOnBindFailure: true);
            var isExplicit = method.Name == "op_Explicit";

            var acceptsNull = false;
            if (parameter.ParameterType.AllowsNull())
            {
                var attribute = method.GetCustomAttribute<RejectsNullAttribute>();
                acceptsNull = attribute == null;
            }

            var converter = Activator.CreateInstance(converterType, func, isExplicit, acceptsNull);
            return (Converter)converter!;
        }
    }
}