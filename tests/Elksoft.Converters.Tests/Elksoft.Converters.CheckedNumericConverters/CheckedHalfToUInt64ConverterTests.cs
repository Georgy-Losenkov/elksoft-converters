#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedHalfToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedHalfToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedHalfToUInt64Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, UInt64>() {
                { Half.Zero, 0UL },
                { Half.Epsilon, 0UL },
                { Half.One, 1UL },
                { Half.E, UInt64.Parse("2") },
                { SByte.MaxValue, (UInt64)SByte.MaxValue },
                { Byte.MaxValue, (UInt64)Byte.MaxValue },
                { (Half)Int16.MaxValue, UInt64.Parse("32768") },
                { Half.MaxValue, UInt64.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedHalfToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Half> Convert_ThrowsException_Data()
        {
            return new TheoryData<Half>() {
                { Half.NaN },
                { Half.NegativeInfinity },
                { Half.MinValue },
                { (Half)Int16.MinValue },
                { SByte.MinValue },
                { Half.NegativeOne },
                { Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Half input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedHalfToUInt64Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
