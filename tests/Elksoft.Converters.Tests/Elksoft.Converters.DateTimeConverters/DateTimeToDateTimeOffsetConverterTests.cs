using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.DateTimeConverters
{
    public sealed class DateTimeToDateTimeOffsetConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new DateTimeToDateTimeOffsetConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new DateTimeToDateTimeOffsetConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<DateTime> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<DateTime>() {
                { new DateTime(DateTime.MinValue.Ticks, DateTimeKind.Utc) },
                { new DateTime(DateTime.MaxValue.Ticks, DateTimeKind.Utc) },
                { new DateTime(2022, 12, 12, 12, 23, 34) },
                { new DateTime(2022, 12, 12, 12, 23, 34, DateTimeKind.Local) },
                { new DateTime(2022, 12, 12, 12, 23, 34, DateTimeKind.Unspecified) },
                { new DateTime(2022, 12, 12, 12, 23, 34, DateTimeKind.Utc) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(DateTime input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new DateTimeToDateTimeOffsetConverter();
            converter.Convert(input, null).Should().Be(input);
            converter.Convert(input, mockProvider.Object).Should().Be(input);

            mockProvider.VerifyAll();
        }
    }
}