using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class StringToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new StringToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new StringToBooleanConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void Convert_NullInput_ThrowsArgumentNullException()
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToBooleanConverter();
            converter.Invoking(x => x.Convert(null, null)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");
            converter.Invoking(x => x.Convert(null, mockProvider.Object)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");

            mockProvider.VerifyAll();
        }

        public static TheoryData<String> Convert_NotNullInput_ReturnsExpected_Data()
        {
            return new TheoryData<String>() {
                { "True" },
                { "FALSE" },
                { "true" },
                { "fALSE" },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_NotNullInput_ReturnsExpected_Data))]
        public static void Convert_NotNullInput_ReturnsExpected(String input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToBooleanConverter();
            converter.Convert(input, null).Should().Be(Boolean.Parse(input));
            converter.Convert(input, mockProvider.Object).Should().Be(Boolean.Parse(input));

            mockProvider.VerifyAll();
        }

        public static TheoryData<String> Convert_ThrowsException_Data()
        {
            return new TheoryData<String>() {
                { String.Empty },
                { "T" },
                { "True1" },
                { "f" },
                { "false0" },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(String input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToBooleanConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}