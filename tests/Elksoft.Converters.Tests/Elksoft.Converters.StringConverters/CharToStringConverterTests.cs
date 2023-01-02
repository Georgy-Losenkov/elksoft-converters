using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class CharToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CharToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CharToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Char> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Char>() {
                { Char.MinValue },
                { 'A' },
                { 'a' },
                { '1' },
                { Char.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Char input)
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CharToStringConverter();
            converter.Convert(input, null).Should().Be(input.ToString());
            converter.Convert(input, mockFormatProvider.Object).Should().Be(input.ToString());

            mockFormatProvider.VerifyAll();
        }
    }
}