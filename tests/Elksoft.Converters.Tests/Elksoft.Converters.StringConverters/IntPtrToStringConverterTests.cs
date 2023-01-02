#if NET7_0_OR_GREATER
using System;
using System.Globalization;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class IntPtrToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new IntPtrToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new IntPtrToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, IFormatProvider> Convert_ReturnsExpected_Data()
        {
            var formatProvider0 = default(IFormatProvider);
            var formatProvider1 = CultureInfo.InvariantCulture;
            var formatProvider2 = CultureInfo.CreateSpecificCulture("ru-RU");

            return new TheoryData<IntPtr, IFormatProvider>() {
                { 123_456_789, formatProvider0 },
                { 123_456_789, formatProvider1 },
                { 123_456_789, formatProvider2 },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, IFormatProvider provider)
        {
            var converter = new IntPtrToStringConverter();
            converter.Convert(input, provider).Should().Be(input.ToString(provider));
        }
    }
}
#endif