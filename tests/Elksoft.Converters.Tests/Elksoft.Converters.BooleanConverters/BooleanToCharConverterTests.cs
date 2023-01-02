using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class BooleanToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new BooleanToCharConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new BooleanToCharConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Boolean, Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Boolean, Char>() {
                { false, '\u0000' },
                { true, '\u0001' },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Boolean input, Char output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new BooleanToCharConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
