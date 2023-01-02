using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class DecimalToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new DecimalToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new DecimalToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Decimal, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<Decimal, IFormatProvider>() {
                { 123_456_789, formatProvider0 },
                { 123_456_789, formatProvider1 },
                { 123_456_789, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Decimal input, IFormatProvider provider)
        {
            var converter = new DecimalToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}