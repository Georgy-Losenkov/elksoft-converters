using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class Int16ToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new Int16ToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new Int16ToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int16, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<Int16, IFormatProvider>() {
                { 12_345, formatProvider0 },
                { 12_345, formatProvider1 },
                { 12_345, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int16 input, IFormatProvider provider)
        {
            var converter = new Int16ToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}