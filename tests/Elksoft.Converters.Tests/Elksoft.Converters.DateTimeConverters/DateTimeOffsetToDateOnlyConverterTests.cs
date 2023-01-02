using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.DateTimeConverters
{
    public sealed class DateTimeOffsetToDateOnlyConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new DateTimeOffsetToDateOnlyConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new DateTimeOffsetToDateOnlyConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<DateTimeOffset> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<DateTimeOffset>() {
                { DateTimeOffset.MinValue },
                { DateTimeOffset.MaxValue },
                { new DateTimeOffset(new DateTime(2022, 12, 12, 12, 23, 34), TimeSpan.FromMinutes(45)) },
                { new DateTimeOffset(new DateTime(2022, 12, 12, 12, 23, 34, DateTimeKind.Unspecified), TimeSpan.FromMinutes(45)) },
                { new DateTimeOffset(new DateTime(2022, 12, 12, 12, 23, 34, DateTimeKind.Utc), TimeSpan.Zero) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(DateTimeOffset input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new DateTimeOffsetToDateOnlyConverter();
            converter.Convert(input, null).Should().Be(DateOnly.FromDateTime(input.DateTime));
            converter.Convert(input, mockProvider.Object).Should().Be(DateOnly.FromDateTime(input.DateTime));

            mockProvider.VerifyAll();
        }
    }
}