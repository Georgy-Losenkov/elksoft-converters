// <copyright file="NullableWrapper.cs" company="Georgy Losenkov">
// Copyright (c) Georgy Losenkov. All rights reserved.
// </copyright>

using System;
using System.Reflection;
using Elksoft.Converters.Transformers;

namespace Elksoft.Converters
{
    /// <inheritdoc cref="INullableWrapper"/>
    public class NullableWrapper : INullableWrapper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NullableWrapper"/> class.
        /// </summary>
        public NullableWrapper()
        {
        }

        internal enum WrapKind
        {
            CC,
            NN,
            NS,
            SN,
            SS,
            UN,
        }

        /// <inheritdoc />
        public Converter Wrap(Type inType, Converter converter, Type outType)
        {
            Check.NotNull(inType, nameof(inType));
            Check.NotNull(converter, nameof(converter));
            Check.NotNull(outType, nameof(outType));

            var wrapKind1 = GetWrapKind(inType, converter.InType);
            if (wrapKind1 == WrapKind.UN)
            {
                throw new ArgumentException("Converter input type is not compatible with target input type", nameof(converter));
            }

            var wrapKind2 = GetWrapKind(converter.OutType, outType);
            if (wrapKind2 == WrapKind.UN)
            {
                throw new ArgumentException("Converter output type is not compatible with target output type", nameof(converter));
            }

            var methodName = $"Transform{wrapKind1}{wrapKind2}";

            var methodInfo = typeof(NullableWrapper).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);

            methodInfo = (methodInfo!).MakeGenericMethod(converter.InType.MakeNotNullable(), converter.OutType.MakeNotNullable());

            var result = methodInfo.Invoke(null, new Object[] { converter });

            return (Converter)result!;

            /*
            accepts null means that converter accepts null input and convert it into something
            rejects null means that null value is not allowed by converter.
            if we need to convert null value we should convert it into default value of the output type

            |-----------------|-----------------------------|-----------------------------|------------------------------|
            |                 |                             | Original: accepts null true | Original: accepts null false |
            |-----------------|-----------------------------|-----------------------------|------------------------------|
            | С, f: С -> С, С | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            | С, f: С -> S, S | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            | С, f: С -> N, S | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            | С, f: С -> S ,N | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            | С, f: С -> N, N | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            |-----------------|-----------------------------|-----------------------------|------------------------------|
            | N, f: N -> С, С | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            | N, f: N -> S, S | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            | N, f: N -> N, S | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            | N, f: N -> S ,N | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            | N, f: N -> N, N | May accept null             | wrapper accepts null true   | wrapper accepts null true    |
            |-----------------|-----------------------------|-----------------------------|------------------------------|
            | N, f: S -> С, С |                             | wrapper accepts null true   | wrapper accepts null true    |
            | N, f: S -> S, S |                             | wrapper accepts null true   | wrapper accepts null true    |
            | N, f: S -> N, S |                             | wrapper accepts null true   | wrapper accepts null true    |
            | N, f: S -> S ,N |                             | wrapper accepts null true   | wrapper accepts null true    |
            | N, f: S -> N, N |                             | wrapper accepts null true   | wrapper accepts null true    |
            |-----------------|-----------------------------|-----------------------------|------------------------------|
            | S, f: N -> С, С | May accept null but ignored | wrapper accepts null false  | wrapper accepts null false   |
            | S, f: N -> S, S | May accept null but ignored | wrapper accepts null false  | wrapper accepts null false   |
            | S, f: N -> N, S | May accept null but ignored | wrapper accepts null false  | wrapper accepts null false   |
            | S, f: N -> S ,N | May accept null but ignored | wrapper accepts null false  | wrapper accepts null false   |
            | S, f: N -> N, N | May accept null but ignored | wrapper accepts null false  | wrapper accepts null false   |
            |-----------------|-----------------------------|-----------------------------|------------------------------|
            | S, f: S -> С, С |                             | wrapper accepts null false  | wrapper accepts null false   |
            | S, f: S -> S, S |                             | wrapper accepts null false  | wrapper accepts null false   |
            | S, f: S -> N, S |                             | wrapper accepts null false  | wrapper accepts null false   |
            | S, f: S -> S ,N |                             | wrapper accepts null false  | wrapper accepts null false   |
            | S, f: S -> N, N |                             | wrapper accepts null false  | wrapper accepts null false   |
            |-----------------|-----------------------------|-----------------------------|------------------------------|
            */
        }

        internal static WrapKind GetWrapKind(Type inType, Type outType)
        {
            Check.NotNull(inType, nameof(inType));
            Check.NotNull(outType, nameof(outType));

            if (TypeExtensions.IsNullable(inType, out var x0))
            {
                if (TypeExtensions.IsNullable(outType, out var y0))
                {
                    return (x0 == y0) ? WrapKind.NN : WrapKind.UN;
                }
                else
                {
                    return (x0 == outType) ? WrapKind.NS : WrapKind.UN;
                }
            }
            else
            {
                if (TypeExtensions.IsNullable(outType, out var y0))
                {
                    return (inType == y0) ? WrapKind.SN : WrapKind.UN;
                }
                else if (inType == outType)
                {
                    return inType.IsValueType ? WrapKind.SS : WrapKind.CC;
                }
                else
                {
                    return WrapKind.UN;
                }
            }
        }

        private static Converter<TIn, TOut> TransformCCCC<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : class
            where TOut : class
        {
            if (innerConverter.AcceptsNull)
            {
                return innerConverter;
            }
            else
            {
                return new RejectingNullWrapperCCCC<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn, TOut?> TransformCCNN<TIn, TOut>(Converter<TIn, TOut?> innerConverter)
            where TIn : class
            where TOut : struct
        {
            if (innerConverter.AcceptsNull)
            {
                return innerConverter;
            }
            else
            {
                return new RejectingNullWrapperCCNN<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn, TOut> TransformCCNS<TIn, TOut>(Converter<TIn, TOut?> innerConverter)
            where TIn : class
            where TOut : struct
        {
            if (innerConverter.AcceptsNull)
            {
                return new AcceptingNullWrapperCCNS<TIn, TOut>(innerConverter);
            }
            else
            {
                return new RejectingNullWrapperCCNS<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn, TOut?> TransformCCSN<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : class
            where TOut : struct
        {
            if (innerConverter.AcceptsNull)
            {
                return new AcceptingNullWrapperCCSN<TIn, TOut>(innerConverter);
            }
            else
            {
                return new RejectingNullWrapperCCSN<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn, TOut> TransformCCSS<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : class
            where TOut : struct
        {
            if (innerConverter.AcceptsNull)
            {
                return innerConverter;
            }
            else
            {
                return new RejectingNullWrapperCCSS<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn?, TOut> TransformNNCC<TIn, TOut>(Converter<TIn?, TOut> innerConverter)
            where TIn : struct
            where TOut : class
        {
            if (innerConverter.AcceptsNull)
            {
                return innerConverter;
            }
            else
            {
                return new RejectingNullWrapperNNCC<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn?, TOut?> TransformNNNN<TIn, TOut>(Converter<TIn?, TOut?> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            if (innerConverter.AcceptsNull)
            {
                return innerConverter;
            }
            else
            {
                return new RejectingNullWrapperNNNN<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn?, TOut> TransformNNNS<TIn, TOut>(Converter<TIn?, TOut?> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            if (innerConverter.AcceptsNull)
            {
                return new AcceptingNullWrapperNNNS<TIn, TOut>(innerConverter);
            }
            else
            {
                return new RejectingNullWrapperNNNS<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn?, TOut?> TransformNNSN<TIn, TOut>(Converter<TIn?, TOut> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            if (innerConverter.AcceptsNull)
            {
                return new AcceptingNullWrapperNNSN<TIn, TOut>(innerConverter);
            }
            else
            {
                return new RejectingNullWrapperNNSN<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn?, TOut> TransformNNSS<TIn, TOut>(Converter<TIn?, TOut> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            if (innerConverter.AcceptsNull)
            {
                return innerConverter;
            }
            else
            {
                return new RejectingNullWrapperNNSS<TIn, TOut>(innerConverter);
            }
        }

        private static Converter<TIn?, TOut> TransformNSCC<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : struct
            where TOut : class
        {
            return new CommonWrapperNSCC<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn?, TOut?> TransformNSNN<TIn, TOut>(Converter<TIn, TOut?> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperNSNN<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn?, TOut> TransformNSNS<TIn, TOut>(Converter<TIn, TOut?> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperNSNS<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn?, TOut?> TransformNSSN<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperNSSN<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn?, TOut> TransformNSSS<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperNSSS<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn, TOut> TransformSNCC<TIn, TOut>(Converter<TIn?, TOut> innerConverter)
            where TIn : struct
            where TOut : class
        {
            return new CommonWrapperSNCC<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn, TOut?> TransformSNNN<TIn, TOut>(Converter<TIn?, TOut?> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperSNNN<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn, TOut> TransformSNNS<TIn, TOut>(Converter<TIn?, TOut?> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperSNNS<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn, TOut?> TransformSNSN<TIn, TOut>(Converter<TIn?, TOut> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperSNSN<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn, TOut> TransformSNSS<TIn, TOut>(Converter<TIn?, TOut> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperSNSS<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn, TOut> TransformSSCC<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : struct
            where TOut : class
        {
            return innerConverter;
        }

        private static Converter<TIn, TOut?> TransformSSNN<TIn, TOut>(Converter<TIn, TOut?> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return innerConverter;
        }

        private static Converter<TIn, TOut> TransformSSNS<TIn, TOut>(Converter<TIn, TOut?> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperSSNS<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn, TOut?> TransformSSSN<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return new CommonWrapperSSSN<TIn, TOut>(innerConverter);
        }

        private static Converter<TIn, TOut> TransformSSSS<TIn, TOut>(Converter<TIn, TOut> innerConverter)
            where TIn : struct
            where TOut : struct
        {
            return innerConverter;
        }
    }
}