#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedHalfToSByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedHalfToSByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedHalfToSByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, SByte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, SByte>() {
                { SByte.MinValue, SByte.MinValue },
                { Half.NegativeOne, (SByte)(-1) },
                { Half.NegativeZero, (SByte)0 },
                { Half.Zero, (SByte)0 },
                { Half.Epsilon, (SByte)0 },
                { Half.One, (SByte)1 },
                { Half.E, SByte.Parse("2") },
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, SByte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedHalfToSByteConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
