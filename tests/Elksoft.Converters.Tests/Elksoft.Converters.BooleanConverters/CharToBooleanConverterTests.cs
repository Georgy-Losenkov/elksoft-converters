using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class CharToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CharToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new CharToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Char, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char, Boolean>() {
                { '\u0000', false },
                { '\u0001', true },
                { (Char)SByte.MaxValue, true },
                { (Char)Byte.MaxValue, true },
                { (Char)Int16.MaxValue, true },
                { Char.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CharToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
