using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToDecimalConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToDecimalConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToDecimalConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, Decimal> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, Decimal>() {
                { '\u0000', Decimal.Zero },
                { '\u0001', Decimal.One },
                { (Char)SByte.MaxValue, SByte.MaxValue },
                { (Char)Byte.MaxValue, Byte.MaxValue },
                { (Char)Int16.MaxValue, Int16.MaxValue },
                { Char.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, Decimal output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToDecimalConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
