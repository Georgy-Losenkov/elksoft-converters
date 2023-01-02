#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedHalfToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedHalfToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedHalfToCharConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Half, Char>() {
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

            var converter = new CheckedHalfToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Half> Convert_ThrowsException_Data()
        {
            return new TheoryData<Half>() {
                { Half.NaN },
                { Half.NegativeInfinity },
                { Half.MinValue },
                { (Half)Int16.MinValue },
                { SByte.MinValue },
                { Half.NegativeOne },
                { Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Half input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedHalfToCharConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
