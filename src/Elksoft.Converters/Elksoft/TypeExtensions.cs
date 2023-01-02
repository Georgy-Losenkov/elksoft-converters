// <copyright file="TypeExtensions.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;

namespace Elksoft
{
    /// <summary>
    /// Provides extensions for <see cref="Type"/> widely used in project.
    /// </summary>
    internal static class TypeExtensions
    {
        /// <summary>
        /// Determines whether a specified type allows <see langword="null"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to validate.</param>
        /// <returns><see langword="true"/> if the <paramref name="type"/> is either reference type or <see cref="Nullable{T}"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="type"/> is <see langword="null"/>.</exception>
        internal static Boolean AllowsNull(this Type type)
        {
            Check.NotNull(type, nameof(type));

            if (type.IsValueType)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Determines whether a specified type is <see cref="Nullable{T}"/> and gets its' underlying type.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to validate.</param>
        /// <param name="underlyingType">When this method returns, contains the underlying <see cref="Type"/> for <paramref name="type"/>; otherwise the <see langword="null"/>. This parameter is passed uninitialized.</param>
        /// <returns><see langword="true"/> if the type is <see cref="Nullable{T}"/>; otherwise, <see langword="false"/>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="type"/> is <see langword="null"/>.</exception>
        internal static Boolean IsNullable(Type type, out Type? underlyingType)
        {
            Check.NotNull(type, nameof(type));

            if (type.IsValueType && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                underlyingType = type.GetGenericArguments()[0];
                return true;
            }
            else
            {
                underlyingType = null;
                return false;
            }
        }

        internal static Type MakeNotNullable(this Type type)
        {
            Check.NotNull(type, nameof(type));

            if (type.IsValueType && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return type.GetGenericArguments()[0];
            }

            return type;
        }

        internal static Type MakeNullable(this Type type)
        {
            Check.NotNull(type, nameof(type));

            if (type.IsValueType)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    return type;
                }
                else
                {
                    return typeof(Nullable<>).MakeGenericType(type);
                }
            }
            else
            {
                return type;
            }
        }

        internal static Boolean ImplementInterface(this Type type, Type interfaceType)
        {
            Check.NotNull(type, nameof(type));
            Check.NotNull(interfaceType, nameof(interfaceType));

            for (var t = type; t != null; t = t.BaseType)
            {
                var interfaces = t.GetInterfaces();
                if (interfaces != null)
                {
                    for (var i = 0; i < interfaces.Length; i++)
                    {
                        var test = interfaces[i];
                        if (test == interfaceType || (test != null && test.ImplementInterface(interfaceType)))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}