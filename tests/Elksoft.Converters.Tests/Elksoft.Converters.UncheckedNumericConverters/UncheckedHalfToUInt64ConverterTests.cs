#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedHalfToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedHalfToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedHalfToUInt64Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, UInt64>() {
                { Half.NegativeZero, 0UL },
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

            var converter = new UncheckedHalfToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
