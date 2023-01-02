using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class StringToGuidConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new StringToGuidConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new StringToGuidConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void Convert_NullInput_ThrowsArgumentNullException()
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToGuidConverter();
            converter.Invoking(x => x.Convert(null, null)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");
            converter.Invoking(x => x.Convert(null, mockFormatProvider.Object)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");

            mockFormatProvider.VerifyAll();
        }

        public static TheoryData<String> Convert_NotNullInput_ReturnsExpected_Data()
        {
            return new TheoryData<String>() {
                { Guid.NewGuid().ToString() },
                { Guid.NewGuid().ToString("N") },
                { Guid.NewGuid().ToString("D") },
                { Guid.NewGuid().ToString("B") },
                { Guid.NewGuid().ToString("P") },
                { Guid.NewGuid().ToString("X") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_NotNullInput_ReturnsExpected_Data))]
        public static void Convert_NotNullInput_ReturnsExpected(String input)
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToGuidConverter();
            converter.Convert(input, null).Should().Be(Guid.Parse(input));
            converter.Convert(input, mockFormatProvider.Object).Should().Be(Guid.Parse(input));

            mockFormatProvider.VerifyAll();
        }

        public static TheoryData<String> Convert_ThrowsException_Data()
        {
            return new TheoryData<String>() {
                { String.Empty },
                { "123" },
                { "x123223" },
                { "abcd" },
                { "{123}" },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(String input)
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToGuidConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockFormatProvider.Object)).Should().Throw<Exception>();

            mockFormatProvider.VerifyAll();
        }
    }
}
