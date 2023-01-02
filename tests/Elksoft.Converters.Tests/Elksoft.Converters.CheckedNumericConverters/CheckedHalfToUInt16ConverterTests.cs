#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedHalfToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedHalfToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedHalfToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, UInt16>() {
                { Half.Zero, (UInt16)0 },
                { Half.Epsilon, (UInt16)0 },
                { Half.One, (UInt16)1 },
                { Half.E, UInt16.Parse("2") },
                { SByte.MaxValue, (UInt16)SByte.MaxValue },
                { Byte.MaxValue, (UInt16)Byte.MaxValue },
                { (Half)Int16.MaxValue, UInt16.Parse("32768") },
                { Half.MaxValue, UInt16.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedHalfToUInt16Converter();
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

            var converter = new CheckedHalfToUInt16Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
