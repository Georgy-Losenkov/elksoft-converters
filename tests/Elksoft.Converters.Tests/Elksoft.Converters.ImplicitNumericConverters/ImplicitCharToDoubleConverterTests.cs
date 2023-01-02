using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitCharToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitCharToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitCharToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, Double> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, Double>() {
                { '\u0000', 0.0 },
                { '\u0001', 1.0 },
                { (Char)SByte.MaxValue, SByte.MaxValue },
                { (Char)Byte.MaxValue, Byte.MaxValue },
                { (Char)Int16.MaxValue, Int16.MaxValue },
                { Char.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitCharToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
