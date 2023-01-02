#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitHalfToInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitHalfToInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitHalfToInt64Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Half, Int64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Int64>() {
                { Half.MinValue, Int64.Parse("-65504") },
                { (Half)Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { Half.NegativeOne, -1L },
                { Half.Zero, 0L },
                { Half.Epsilon, 0L },
                { Half.One, 1L },
                { Half.E, Int64.Parse("2") },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { (Half)Int16.MaxValue, Int64.Parse("32768") },
                { Half.MaxValue, Int64.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Int64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitHalfToInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
