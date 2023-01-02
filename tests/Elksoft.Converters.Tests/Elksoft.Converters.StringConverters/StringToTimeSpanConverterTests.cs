using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class StringToTimeSpanConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new StringToTimeSpanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new StringToTimeSpanConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void Convert_NullInput_ThrowsArgumentNullException()
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new StringToTimeSpanConverter();
            converter.Invoking(x => x.Convert(null, null)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");
            converter.Invoking(x => x.Convert(null, mockProvider.Object)).Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("value");

            mockProvider.VerifyAll();
        }

        public static TheoryData<String, IFormatProvider> Convert_NotNullInput_ReturnsExpected_Data()
        {
            var value = TimeSpan.FromSeconds(100000);
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<String, IFormatProvider>() {
                { value.ToString(null, formatProvider0), formatProvider0 },
                { value.ToString(null, formatProvider1), formatProvider1 },
                { value.ToString(null, formatProvider2), formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_NotNullInput_ReturnsExpected_Data))]
        public static void Convert_NotNullInput_ReturnsExpected(String input, IFormatProvider provider)
        {
            var converter = new StringToTimeSpanConverter();
            converter.Convert(input, provider).Should().Be(TimeSpan.Parse(input, provider));
        }

        public static TheoryData<String, IFormatProvider> Convert_ThrowsException_Data()
        {
            return new TheoryData<String, IFormatProvider>() {
                { String.Empty, null },
                { "-", null },
                { "12.3", CultureInfo.InvariantCulture },
                { "12,3", CultureInfo.CreateSpecificCulture("ru-RU") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(String input, IFormatProvider provider)
        {
            var converter = new StringToTimeSpanConverter();
            converter.Invoking(x => x.Convert(input, provider)).Should().Throw<Exception>();
        }
    }
}