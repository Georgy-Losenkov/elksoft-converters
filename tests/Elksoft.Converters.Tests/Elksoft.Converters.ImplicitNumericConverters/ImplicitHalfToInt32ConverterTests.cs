#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitHalfToInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitHalfToInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitHalfToInt32Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Half, Int32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Int32>() {
                { Half.MinValue, Int32.Parse("-65504") },
                { (Half)Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { Half.NegativeOne, -1 },
                { Half.NegativeZero, 0 },
                { Half.Zero, 0 },
                { Half.Epsilon, 0 },
                { Half.One, 1 },
                { Half.E, Int32.Parse("2") },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { (Half)Int16.MaxValue, Int32.Parse("32768") },
                { Half.MaxValue, Int32.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Int32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitHalfToInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
