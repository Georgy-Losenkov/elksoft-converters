#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedHalfToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedHalfToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedHalfToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, UInt16>() {
                { Half.NegativeZero, (UInt16)0 },
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

            var converter = new UncheckedHalfToUInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
