using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters
{
    public class PrimitiveConvertersTests
    {
        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public static void Ctor_ThrowsArgumentNullExpected(Boolean useCheckedConversions)
        {
            new Func<Object>(() => new PrimitiveConverters(useCheckedConversions, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("identityConverters");
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void FindConverter_ThrowsArgumentNullExpected(Boolean useCheckedConversions)
        {
            var mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);

            var instance = new PrimitiveConverters(useCheckedConversions, mockIdentityConverters.Object);

            instance.Invoking(x => x.FindConverter(null, null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("inType");

            instance.Invoking(x => x.FindConverter(null, typeof(Byte)))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("inType");

            instance.Invoking(x => x.FindConverter(typeof(Byte), null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("outType");

            mockIdentityConverters.VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverter_UnsupportedType_ReturnsNull_Data()
        {
            var e1 = from flag in new[] { false, true }
                     from t1 in GetSupportedTypes().Select(t => t.Item1)
                     from t2 in GetUnsupportedTypes()
                     where t1 != t2
                     select new Object[] { flag, t1, t2 };

            var e2 = from flag in new[] { false, true }
                     from t1 in GetUnsupportedTypes()
                     from t2 in GetSupportedTypes().Select(t => t.Item1)
                     where t1 != t2
                     select new Object[] { flag, t1, t2 };

            var e3 = from flag in new[] { false, true }
                     from t1 in GetUnsupportedTypes()
                     from t2 in GetUnsupportedTypes()
                     where t1 != t2
                     select new Object[] { flag, t1, t2 };

            return e1.Concat(e2.Concat(e3));
        }

        [Theory]
        [MemberData(nameof(FindConverter_UnsupportedType_ReturnsNull_Data))]
        public void FindConverter_UnsupportedType_ReturnsNull(Boolean useCheckedConversions, Type inType, Type outType)
        {
            var mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);

            var instance = new PrimitiveConverters(useCheckedConversions, mockIdentityConverters.Object);

            instance.FindConverter(inType, outType).Should().BeNull();

            mockIdentityConverters.VerifyAll();
        }

        public static IEnumerable<(Type InType, Type OutType, String Namespace)> GetMap(Boolean useCheckedConversions)
        {
            var binaryConvertersNamespace = "Elksoft.Converters.BinaryConverters";
            var booleanConvertersNamespace = "Elksoft.Converters.BooleanConverters";
            var dateTimeConvertersNamespace = "Elksoft.Converters.DateTimeConverters";
            var implicitNumericConvertersNamespace = "Elksoft.Converters.ImplicitNumericConverters";
            var explicitNumericConvertersNamespace = useCheckedConversions ? "Elksoft.Converters.CheckedNumericConverters" : "Elksoft.Converters.UncheckedNumericConverters";
            var stringConvertersNamespace = "Elksoft.Converters.StringConverters";

            foreach (var (inType, inTypeGroup) in GetSupportedTypes())
            {
                foreach (var (outType, outTypeGroup) in GetSupportedTypes())
                {
                    if (inType == outType)
                    {
                        continue;
                    }

                    String @namespace;
                    switch ((inTypeGroup, outTypeGroup))
                    {
                        case (TypeGroup.Binary, TypeGroup.Binary):
                            @namespace = binaryConvertersNamespace;
                            break;

                        case (TypeGroup.Boolean, TypeGroup.Numeric):
                        case (TypeGroup.Numeric, TypeGroup.Boolean):
                            @namespace = booleanConvertersNamespace;
                            break;

                        case (TypeGroup.Numeric, TypeGroup.Numeric):
                            var inTypeBoundaries = GetTypeBoundary(inType);
                            var outTypeBoundaries = GetTypeBoundary(outType);
                            var isExplicit = outTypeBoundaries.IsFinite && (
                                (inTypeBoundaries.MinValueX86 < outTypeBoundaries.MinValueX86) ||
                                (outTypeBoundaries.MaxValueX86 < inTypeBoundaries.MaxValueX86) ||
                                (inTypeBoundaries.MinValueX64 < outTypeBoundaries.MinValueX64) ||
                                (outTypeBoundaries.MaxValueX64 < inTypeBoundaries.MaxValueX64));
                            @namespace = isExplicit ? explicitNumericConvertersNamespace : implicitNumericConvertersNamespace;
                            break;

                        case (TypeGroup.Boolean, TypeGroup.String):
                        case (TypeGroup.String, TypeGroup.Boolean):
                        case (TypeGroup.DateTime, TypeGroup.String):
                        case (TypeGroup.String, TypeGroup.DateTime):
                        case (TypeGroup.Numeric, TypeGroup.String):
                        case (TypeGroup.String, TypeGroup.Numeric):
                        case (TypeGroup.String, TypeGroup.String):
                            @namespace = stringConvertersNamespace;
                            break;

                        case (TypeGroup.Binary, TypeGroup.String):
                            @namespace = (inType == typeof(Guid)) ? stringConvertersNamespace : null;
                            break;
                        case (TypeGroup.String, TypeGroup.Binary):
                            @namespace = (outType == typeof(Guid)) ? stringConvertersNamespace : null;
                            break;

                        case (TypeGroup.DateTime, TypeGroup.DateTime):
                            var isNotSupported =
                                (inType == typeof(DateOnly) && outType == typeof(TimeOnly)) ||
                                (inType == typeof(DateOnly) && outType == typeof(TimeSpan)) ||
                                (inType == typeof(TimeOnly) && outType == typeof(DateOnly)) ||
                                (inType == typeof(TimeOnly) && outType == typeof(DateTime)) ||
                                (inType == typeof(TimeOnly) && outType == typeof(DateTimeOffset));
                            @namespace = isNotSupported ? null : dateTimeConvertersNamespace;
                            break;

                        default:
                            @namespace = null;
                            break;
                    }

                    yield return (inType, outType, @namespace);
                }
            }
        }

        public static IEnumerable<Object[]> FindConverter_SupportedTypes_UnsupportedPair_ReturnsNull_Data()
        {
            foreach (var flag in new[] { false, true })
            {
                foreach (var (inType, outType, @namespace) in GetMap(flag))
                {
                    if (String.IsNullOrEmpty(@namespace))
                    {
                        yield return new Object[] { flag, inType, outType };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverter_SupportedTypes_UnsupportedPair_ReturnsNull_Data))]
        public void FindConverter_SupportedTypes_UnsupportedPair_ReturnsNull(Boolean useCheckedConversions, Type inType, Type outType)
        {
            var mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);

            var instance = new PrimitiveConverters(useCheckedConversions, mockIdentityConverters.Object);

            instance.FindConverter(inType, outType).Should().BeNull();

            mockIdentityConverters.VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverter_SupportedTypes_SameTypes_ReturnsExpected_Data()
        {
            return
                from flag in new[] { false, true }
                from t in GetSupportedTypes()
                select new Object[] { flag, t.Item1 };
        }

        [Theory]
        [MemberData(nameof(FindConverter_SupportedTypes_SameTypes_ReturnsExpected_Data))]
        public void FindConverter_SupportedTypes_SameTypes_ReturnsExpected(Boolean useCheckedConversions, Type type)
        {
            var mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);
            var mockConverter = new Mock<Converter>(MockBehavior.Strict);

            mockIdentityConverters
                .Setup(x => x.GetIdentityConverter(type))
                .Returns(mockConverter.Object);

            var instance = new PrimitiveConverters(useCheckedConversions, mockIdentityConverters.Object);

            instance.FindConverter(type, type).Should().BeSameAs(mockConverter.Object);

            mockConverter.VerifyAll();
            mockIdentityConverters.VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverter_SupportedTypes_SupportedPair_ReturnsExpected_Data()
        {
            foreach (var flag in new[] { false, true })
            {
                foreach (var (inType, outType, @namespace) in GetMap(flag))
                {
                    if (!String.IsNullOrEmpty(@namespace))
                    {
                        yield return new Object[] { flag, inType, outType, @namespace };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverter_SupportedTypes_SupportedPair_ReturnsExpected_Data))]
        public void FindConverter_SupportedTypes_SupportedPair_ReturnsExpected(Boolean useCheckedConversions, Type inType, Type outType, String @namespace)
        {
            var mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);

            var instance = new PrimitiveConverters(useCheckedConversions, mockIdentityConverters.Object);

            var result = instance.FindConverter(inType, outType);

            result.Should().BeAssignableTo(typeof(Converter<,>).MakeGenericType(inType, outType));

            result.GetType().Namespace.Should().Be(@namespace);

            mockIdentityConverters.VerifyAll();
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void IsSupported_ThrowsArgumentNullException(Boolean useCheckedConversions)
        {
            var mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);

            var instance = new PrimitiveConverters(useCheckedConversions, mockIdentityConverters.Object);

            instance.Invoking(x => x.IsSupported(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");

            mockIdentityConverters.VerifyAll();
        }

        public static IEnumerable<Object[]> IsSupported_ReturnsFalse_Data()
        {
            return
                from flag in new[] { false, true }
                from t in GetUnsupportedTypes()
                select new Object[] { flag, t };
        }

        [Theory]
        [MemberData(nameof(IsSupported_ReturnsFalse_Data))]
        public void IsSupported_ReturnsFalse(Boolean useCheckedConversions, Type type)
        {
            var mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);

            var instance = new PrimitiveConverters(useCheckedConversions, mockIdentityConverters.Object);

            instance.IsSupported(type).Should().Be(false);

            mockIdentityConverters.VerifyAll();
        }

        public static IEnumerable<Object[]> IsSupported_ReturnsTrue_Data()
        {
            return
                from flag in new[] { false, true }
                from t in GetSupportedTypes()
                select new Object[] { flag, t.Item1 };
        }

        [Theory]
        [MemberData(nameof(IsSupported_ReturnsTrue_Data))]
        public void IsSupported_ReturnsTrue(Boolean useCheckedConversions, Type type)
        {
            var mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);

            var instance = new PrimitiveConverters(useCheckedConversions, mockIdentityConverters.Object);

            instance.IsSupported(type).Should().Be(true);

            mockIdentityConverters.VerifyAll();
        }

        private static IEnumerable<(Type, TypeGroup)> GetSupportedTypes()
        {
            yield return (typeof(Byte[]), TypeGroup.Binary);
            yield return (typeof(Guid), TypeGroup.Binary);

            yield return (typeof(Boolean), TypeGroup.Boolean);

            yield return (typeof(DateOnly), TypeGroup.DateTime);
            yield return (typeof(DateTime), TypeGroup.DateTime);
            yield return (typeof(DateTimeOffset), TypeGroup.DateTime);
            yield return (typeof(TimeOnly), TypeGroup.DateTime);
            yield return (typeof(TimeSpan), TypeGroup.DateTime);

            yield return (typeof(Byte), TypeGroup.Numeric);
            yield return (typeof(Char), TypeGroup.Numeric);
            yield return (typeof(Decimal), TypeGroup.Numeric);
            yield return (typeof(Double), TypeGroup.Numeric);
#if NET7_0_OR_GREATER
            yield return (typeof(Half), TypeGroup.Numeric);
#endif
            yield return (typeof(Int16), TypeGroup.Numeric);
            yield return (typeof(Int32), TypeGroup.Numeric);
            yield return (typeof(Int64), TypeGroup.Numeric);
#if NET7_0_OR_GREATER
            yield return (typeof(Int128), TypeGroup.Numeric);
            yield return (typeof(IntPtr), TypeGroup.Numeric);
#endif
            yield return (typeof(SByte), TypeGroup.Numeric);
            yield return (typeof(Single), TypeGroup.Numeric);
            yield return (typeof(UInt16), TypeGroup.Numeric);
            yield return (typeof(UInt32), TypeGroup.Numeric);
            yield return (typeof(UInt64), TypeGroup.Numeric);
#if NET7_0_OR_GREATER
            yield return (typeof(UInt128), TypeGroup.Numeric);
            yield return (typeof(UIntPtr), TypeGroup.Numeric);
#endif

            yield return (typeof(String), TypeGroup.String);
        }

        private static Boundaries GetTypeBoundary(Type type)
        {
            if (type == typeof(Byte))
            {
                return new Boundaries(true, Byte.MinValue, Byte.MaxValue);
            }
            if (type == typeof(Char))
            {
                return new Boundaries(true, Char.MinValue, Char.MaxValue);
            }
            if (type == typeof(Decimal))
            {
                return new Boundaries(true, (Double)Decimal.MinValue, (Double)Decimal.MaxValue);
            }
            if (type == typeof(Double))
            {
                return new Boundaries(false, Double.MinValue, Double.MaxValue);
            }
#if NET7_0_OR_GREATER
            if (type == typeof(Half))
            {
                return new Boundaries(false, (Double)Half.MinValue, (Double)Half.MaxValue);
            }
#endif
            if (type == typeof(Int16))
            {
                return new Boundaries(true, Int16.MinValue, Int16.MaxValue);
            }
            if (type == typeof(Int32))
            {
                return new Boundaries(true, Int32.MinValue, Int32.MaxValue);
            }
            if (type == typeof(Int64))
            {
                return new Boundaries(true, Int64.MinValue, Int64.MaxValue);
            }
#if NET7_0_OR_GREATER
            if (type == typeof(Int128))
            {
                return new Boundaries(true, (Double)Int128.MinValue, (Double)Int128.MaxValue);
            }
            if (type == typeof(IntPtr))
            {
                return new Boundaries(true, Int32.MinValue, Int32.MaxValue, Int64.MinValue, Int64.MaxValue);
            }
#endif
            if (type == typeof(SByte))
            {
                return new Boundaries(true, SByte.MinValue, SByte.MaxValue);
            }
            if (type == typeof(Single))
            {
                return new Boundaries(false, Single.MinValue, Single.MaxValue);
            }
            if (type == typeof(UInt16))
            {
                return new Boundaries(true, UInt16.MinValue, UInt16.MaxValue);
            }
            if (type == typeof(UInt32))
            {
                return new Boundaries(true, UInt32.MinValue, UInt32.MaxValue);
            }
            if (type == typeof(UInt64))
            {
                return new Boundaries(true, UInt64.MinValue, UInt64.MaxValue);
            }
#if NET7_0_OR_GREATER
            if (type == typeof(UInt128))
            {
                return new Boundaries(true, (Double)UInt128.MinValue, (Double)UInt128.MaxValue);
            }
            if (type == typeof(UIntPtr))
            {
                return new Boundaries(true, UInt32.MinValue, UInt32.MaxValue, UInt64.MinValue, UInt64.MaxValue);
            }
#endif

            throw new ArgumentException(nameof(type));
        }

        private static IEnumerable<Type> GetUnsupportedTypes()
        {
#if !NET7_0_OR_GREATER
#if NET5_0_OR_GREATER
            yield return typeof(Half);
#endif
            yield return typeof(UIntPtr);
            yield return typeof(IntPtr);
#endif
            yield return typeof(DateTimeKind);
            yield return typeof(DateTimeKind?);
            yield return typeof(Nullable<>);
            yield return typeof(IFormattable);
            yield return typeof(Object);
        }

        private enum TypeGroup
        {
            Binary,
            Boolean,
            DateTime,
            Numeric,
            String
        }

        private class Boundaries
        {
            public Boundaries(Boolean isFinite, Double minValueX86, Double maxValueX86, Double? minValueX64 = null, Double? maxValueX64 = null)
            {
                IsFinite = isFinite;
                MinValueX86 = minValueX86;
                MaxValueX86 = maxValueX86;
                MinValueX64 = minValueX64 ?? minValueX86;
                MaxValueX64 = maxValueX64 ?? maxValueX86;
            }

            public Boolean IsFinite { get; }
            public Double MinValueX86 { get; }
            public Double MaxValueX86 { get; }
            public Double MinValueX64 { get; }
            public Double MaxValueX64 { get; }
        }
    }
}