#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitHalfToDecimalConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitHalfToDecimalConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitHalfToDecimalConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Half, Decimal> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Decimal>() {
                { Half.MinValue, Decimal.Parse("-65504") },
                { (Half)Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { Half.NegativeOne, -1m },
                { Half.NegativeZero, Decimal.Zero },
                { Half.Zero, Decimal.Zero },
                { Half.Epsilon, Decimal.Parse("0.00000005960464") },
                { Half.One, Decimal.One },
                { Half.E, Decimal.Parse("2.71875") },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { (Half)Int16.MaxValue, Decimal.Parse("32768") },
                { Half.MaxValue, Decimal.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Decimal output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitHalfToDecimalConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
