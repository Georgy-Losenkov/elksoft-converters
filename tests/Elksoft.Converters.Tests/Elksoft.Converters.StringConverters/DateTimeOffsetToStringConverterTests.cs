using System;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class DateTimeOffsetToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new DateTimeOffsetToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new DateTimeOffsetToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<DateTimeOffset, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var value = new DateTimeOffset(new DateTime(2022, 12, 23, 09, 08, 07, 657), TimeSpan.FromMinutes(-45));
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<DateTimeOffset, IFormatProvider>() {
                { value, formatProvider0 },
                { value, formatProvider1 },
                { value, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(DateTimeOffset input, IFormatProvider provider)
        {
            var converter = new DateTimeOffsetToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}