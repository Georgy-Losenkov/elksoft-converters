#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedHalfToByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedHalfToByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedHalfToByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, Byte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Byte>() {
                { Half.Zero, 0 },
                { Half.Epsilon, 0 },
                { Half.One, 1 },
                { Half.E, Byte.Parse("2") },
                { SByte.MaxValue, 127 },
                { Byte.MaxValue, Byte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Byte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedHalfToByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
