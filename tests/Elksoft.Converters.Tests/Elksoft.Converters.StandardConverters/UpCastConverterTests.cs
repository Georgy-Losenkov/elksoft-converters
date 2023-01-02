using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class UpCastConverterTests
    {
        private static void Verify_Convert_ReturnsExpected<TIn, TOut>(TIn input)
            where TIn : TOut
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UpCastConverter<TIn, TOut>();
            converter.Convert(input, null).Should().Be(input);
            converter.Convert(input, mockProvider.Object).Should().Be(input);

            mockProvider.VerifyAll();
        }

        [Fact]
        public static void Convert_ReturnsExpected()
        {
            Verify_Convert_ReturnsExpected<Int32, IConvertible>(0);
            Verify_Convert_ReturnsExpected<Int32, IConvertible>(5);
            Verify_Convert_ReturnsExpected<Int32, IConvertible>(10);

            Verify_Convert_ReturnsExpected<Int32, Int32>(0);
            Verify_Convert_ReturnsExpected<Int32, Int32>(5);
            Verify_Convert_ReturnsExpected<Int32, Int32>(10);

            Verify_Convert_ReturnsExpected<Int32, Object>(0);
            Verify_Convert_ReturnsExpected<Int32, Object>(5);
            Verify_Convert_ReturnsExpected<Int32, Object>(10);

            Verify_Convert_ReturnsExpected<String, IConvertible>(null);
            Verify_Convert_ReturnsExpected<String, IConvertible>("");
            Verify_Convert_ReturnsExpected<String, IConvertible>("ten");

            Verify_Convert_ReturnsExpected<String, IComparable>(null);
            Verify_Convert_ReturnsExpected<String, IComparable>("");
            Verify_Convert_ReturnsExpected<String, IComparable>("ten");

            Verify_Convert_ReturnsExpected<String, Object>(null);
            Verify_Convert_ReturnsExpected<String, Object>("");
            Verify_Convert_ReturnsExpected<String, Object>("ten");
        }

        public static IEnumerable<Object[]> Types()
        {
            yield return new Object[] { typeof(Int32), typeof(IConvertible) };
            yield return new Object[] { typeof(Int32), typeof(Int32) };
            yield return new Object[] { typeof(Int32), typeof(Object) };

            yield return new Object[] { typeof(String), typeof(IConvertible) };
            yield return new Object[] { typeof(String), typeof(IComparable) };
            yield return new Object[] { typeof(String), typeof(Object) };
        }

        internal static void Verify_AcceptsNull_IsTrue<TIn, TOut>()
            where TIn : TOut
        {
            var converter = new UpCastConverter<TIn, TOut>();

            converter.AcceptsNull.Should().BeTrue();
        }

        internal static void Verify_IsExplicit_IsFalse<TIn, TOut>()
            where TIn : TOut
        {
            var converter = new UpCastConverter<TIn, TOut>();

            converter.IsExplicit.Should().BeFalse();
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void AcceptsNull_IsFalse(Type typeIn, Type typeOut)
        {
            Utilities.Invoke(typeIn, typeOut, Verify_AcceptsNull_IsTrue<Int32, Int32>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void IsExplicit_IsFalse(Type typeIn, Type typeOut)
        {
            Utilities.Invoke(typeIn, typeOut, Verify_IsExplicit_IsFalse<Int32, Int32>);
        }
    }
}