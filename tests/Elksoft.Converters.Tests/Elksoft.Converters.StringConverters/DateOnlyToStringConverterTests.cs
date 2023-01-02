using System;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class DateOnlyToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new DateOnlyToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new DateOnlyToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<DateOnly, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var value = new DateOnly(2022, 12, 23);
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<DateOnly, IFormatProvider>() {
                { value, formatProvider0 },
                { value, formatProvider1 },
                { value, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(DateOnly input, IFormatProvider provider)
        {
            var converter = new DateOnlyToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}