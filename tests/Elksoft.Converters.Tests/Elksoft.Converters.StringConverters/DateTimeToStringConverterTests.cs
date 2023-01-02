using System;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class DateTimeToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new DateTimeToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new DateTimeToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<DateTime, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var value = new DateTime(2022, 12, 23, 09, 08, 07, 657);
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<DateTime, IFormatProvider>() {
                { value, formatProvider0 },
                { value, formatProvider1 },
                { value, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(DateTime input, IFormatProvider provider)
        {
            var converter = new DateTimeToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}