#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitHalfToInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitHalfToInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitHalfToInt128Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Half, Int128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Int128>() {
                { Half.MinValue, Int128.Parse("-65504") },
                { (Half)Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { Half.NegativeOne, -1 },
                { Half.NegativeZero, Int128.Zero },
                { Half.Zero, Int128.Zero },
                { Half.Epsilon, Int128.Zero },
                { Half.One, Int128.One },
                { Half.E, Int128.Parse("2") },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { (Half)Int16.MaxValue, Int128.Parse("32768") },
                { Half.MaxValue, Int128.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Int128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitHalfToInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
