using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class TimeSpanToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new TimeSpanToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new TimeSpanToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<TimeSpan, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var value = TimeSpan.FromSeconds(100000);
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<TimeSpan, IFormatProvider>() {
                { value, formatProvider0 },
                { value, formatProvider1 },
                { value, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(TimeSpan input, IFormatProvider provider)
        {
            var converter = new TimeSpanToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(null, provider));
        }
    }
}