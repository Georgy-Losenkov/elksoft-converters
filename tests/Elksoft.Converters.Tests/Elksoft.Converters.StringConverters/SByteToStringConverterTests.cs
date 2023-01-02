using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class SByteToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new SByteToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new SByteToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<SByte, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<SByte, IFormatProvider>() {
                { 123, formatProvider0 },
                { 123, formatProvider1 },
                { 123, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(SByte input, IFormatProvider provider)
        {
            var converter = new SByteToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}