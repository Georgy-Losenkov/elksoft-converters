#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedHalfToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedHalfToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedHalfToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, UInt32>() {
                { Half.Zero, 0U },
                { Half.Epsilon, 0U },
                { Half.One, 1U },
                { Half.E, UInt32.Parse("2") },
                { SByte.MaxValue, (UInt32)SByte.MaxValue },
                { Byte.MaxValue, (UInt32)Byte.MaxValue },
                { (Half)Int16.MaxValue, UInt32.Parse("32768") },
                { Half.MaxValue, UInt32.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedHalfToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
