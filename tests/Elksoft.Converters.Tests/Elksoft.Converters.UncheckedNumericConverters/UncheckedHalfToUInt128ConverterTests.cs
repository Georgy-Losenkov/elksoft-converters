#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedHalfToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedHalfToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedHalfToUInt128Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, UInt128>() {
                { Half.Zero, UInt128.Zero },
                { Half.Epsilon, UInt128.Zero },
                { Half.One, UInt128.One },
                { Half.E, UInt128.Parse("2") },
                { SByte.MaxValue, (UInt128)SByte.MaxValue },
                { Byte.MaxValue, (UInt128)Byte.MaxValue },
                { (Half)Int16.MaxValue, UInt128.Parse("32768") },
                { Half.MaxValue, UInt128.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedHalfToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
