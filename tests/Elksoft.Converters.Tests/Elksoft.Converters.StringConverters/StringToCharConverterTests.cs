using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class StringToCharConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var convert = new StringToCharConverter();
            convert.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var convert = new StringToCharConverter();
            convert.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void Convert_NullInput_ThrowsArgumentNullException()
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var convert = new StringToCharConverter();
            convert.Invoking(x => x.Convert(null, null)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");
            convert.Invoking(x => x.Convert(null, mockProvider.Object)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");

            mockProvider.VerifyAll();
        }

        public static TheoryData<String> Convert_NotNullInput_ReturnsExpected_Data()
        {
            return new TheoryData<String>() {
                { Char.MinValue.ToString() },
                { Char.MaxValue.ToString() },
                { '\u0023'.ToString() },
                { '\u1234'.ToString() },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_NotNullInput_ReturnsExpected_Data))]
        public static void Convert_NotNullInput_ReturnsExpected(String input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var convert = new StringToCharConverter();
            convert.Convert(input, null).Should().Be(Char.Parse(input));
            convert.Convert(input, mockProvider.Object).Should().Be(Char.Parse(input));

            mockProvider.VerifyAll();
        }

        public static TheoryData<String> Convert_ThrowsException_Data()
        {
            return new TheoryData<String>() {
                { String.Empty },
                { "12" },
                { "123" },
                { "1234" },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(String input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var convert = new StringToCharConverter();
            convert.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            convert.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}