using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.DateTimeConverters
{
    public sealed class DateOnlyToDateTimeOffsetConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new DateOnlyToDateTimeOffsetConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new DateOnlyToDateTimeOffsetConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<DateOnly> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<DateOnly>() {
                { new DateOnly(1753, 01, 01) },
                { new DateOnly(1976, 03, 01) },
                { new DateOnly(2022, 12, 12) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(DateOnly input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new DateOnlyToDateTimeOffsetConverter();
            converter.Convert(input, null).Should().Be(new DateTimeOffset(input.ToDateTime(TimeOnly.MinValue)));
            converter.Convert(input, mockProvider.Object).Should().Be(new DateTimeOffset(input.ToDateTime(TimeOnly.MinValue)));

            mockProvider.VerifyAll();
        }
    }
}