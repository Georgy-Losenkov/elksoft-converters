using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class Int32ToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new Int32ToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new Int32ToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int32, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<Int32, IFormatProvider>() {
                { 123_456_789, formatProvider0 },
                { 123_456_789, formatProvider1 },
                { 123_456_789, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int32 input, IFormatProvider provider)
        {
            var converter = new Int32ToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}