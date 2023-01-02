using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class StringToByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new StringToByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new StringToByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void Convert_NullInput_ThrowsArgumentNullException()
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToByteConverter();
            converter.Invoking(x => x.Convert(null, null)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");
            converter.Invoking(x => x.Convert(null, mockProvider.Object)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");

            mockProvider.VerifyAll();
        }

        public static TheoryData<String, IFormatProvider> Convert_NotNullInput_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<String, IFormatProvider>() {
                { 127.ToString(formatProvider0), formatProvider0 },
                { 127.ToString(formatProvider1), formatProvider1 },
                { 127.ToString(formatProvider2), formatProvider2 },
                { 255.ToString(formatProvider0), formatProvider0 },
                { 255.ToString(formatProvider1), formatProvider1 },
                { 255.ToString(formatProvider2), formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_NotNullInput_ReturnsExpected_Data))]
        public static void Convert_NotNullInput_ReturnsExpected(String input, IFormatProvider provider)
        {
            var converter = new StringToByteConverter();
            converter.Convert(input, provider).Should().Be(Byte.Parse(input, provider));
        }

        public static TheoryData<String, IFormatProvider> Convert_ThrowsException_Data()
        {
            return new TheoryData<String, IFormatProvider>() {
                { String.Empty, null },
                { "-12", null },
                { "12.3", null },
                { "12,3", CultureInfo.CreateSpecificCulture("ru-RU") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(String input, IFormatProvider provider)
        {
            var converter = new StringToByteConverter();
            converter.Invoking(x => x.Convert(input, provider)).Should().Throw<Exception>();
        }
    }
}