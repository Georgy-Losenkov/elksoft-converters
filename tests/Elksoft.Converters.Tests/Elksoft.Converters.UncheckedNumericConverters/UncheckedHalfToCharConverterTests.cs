#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedHalfToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedHalfToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedHalfToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Char>() {
                { Half.NegativeZero, '\u0000' },
                { Half.Zero, '\u0000' },
                { Half.Epsilon, '\u0000' },
                { Half.One, '\u0001' },
                { Half.E, Char.Parse("") },
                { SByte.MaxValue, (Char)SByte.MaxValue },
                { Byte.MaxValue, (Char)Byte.MaxValue },
                { (Half)Int16.MaxValue, Char.Parse("耀") },
                { Half.MaxValue, Char.Parse("￠") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedHalfToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
