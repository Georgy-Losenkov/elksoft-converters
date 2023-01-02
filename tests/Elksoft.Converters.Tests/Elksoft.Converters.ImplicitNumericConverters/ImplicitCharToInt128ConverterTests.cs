#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToInt128Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, Int128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, Int128>() {
                { '\u0000', Int128.Zero },
                { '\u0001', Int128.One },
                { (Char)SByte.MaxValue, SByte.MaxValue },
                { (Char)Byte.MaxValue, Byte.MaxValue },
                { (Char)Int16.MaxValue, Int16.MaxValue },
                { Char.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, Int128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
