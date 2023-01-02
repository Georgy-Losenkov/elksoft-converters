#if NET7_0_OR_GREATER
using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class UInt128ToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new UInt128ToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UInt128ToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt128, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<UInt128, IFormatProvider>() {
                { 123_456_789, formatProvider0 },
                { 123_456_789, formatProvider1 },
                { 123_456_789, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, IFormatProvider provider)
        {
            var converter = new UInt128ToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}
#endif