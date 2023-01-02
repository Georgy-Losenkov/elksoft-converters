using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class TimeOnlyToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new TimeOnlyToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new TimeOnlyToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<TimeOnly, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var value = new TimeOnly(23, 34, 45, 567);
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<TimeOnly, IFormatProvider>() {
                { value, formatProvider0 },
                { value, formatProvider1 },
                { value, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(TimeOnly input, IFormatProvider provider)
        {
            var converter = new TimeOnlyToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}