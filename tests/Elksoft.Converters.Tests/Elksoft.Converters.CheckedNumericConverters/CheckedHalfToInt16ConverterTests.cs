#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedHalfToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedHalfToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedHalfToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Int16>() {
                { (Half)Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { Half.NegativeOne, -1 },
                { Half.Zero, (Int16)0 },
                { Half.Epsilon, (Int16)0 },
                { Half.One, (Int16)1 },
                { Half.E, Int16.Parse("2") },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedHalfToInt16Converter();
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
                { (Half)Int16.MaxValue },
                { Half.MaxValue },
                { Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Half input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedHalfToInt16Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
