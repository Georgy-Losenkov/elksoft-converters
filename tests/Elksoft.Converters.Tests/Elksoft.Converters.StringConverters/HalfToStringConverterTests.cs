#if NET7_0_OR_GREATER
using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class HalfToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new HalfToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new HalfToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Half, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<Half, IFormatProvider>() {
                { (Half)12_456, formatProvider0 },
                { (Half)12_456, formatProvider1 },
                { (Half)12_456, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Half input, IFormatProvider provider)
        {
            var converter = new HalfToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}
#endif