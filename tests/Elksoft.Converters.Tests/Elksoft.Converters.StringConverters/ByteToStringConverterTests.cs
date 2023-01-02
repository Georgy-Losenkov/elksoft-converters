using System;
using System.Globalization;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class ByteToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ByteToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new ByteToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Byte, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<Byte, IFormatProvider>() {
                { 123, formatProvider0 },
                { 123, formatProvider1 },
                { 123, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Byte input, IFormatProvider provider)
        {
            var converter = new ByteToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}