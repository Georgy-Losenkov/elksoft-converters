using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.DateTimeConverters
{
    public sealed class DateTimeToTimeOnlyConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new DateTimeToTimeOnlyConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new DateTimeToTimeOnlyConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<DateTime> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<DateTime>() {
                { DateTime.MinValue },
                { DateTime.MaxValue },
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

            var converter = new DateTimeToTimeOnlyConverter();
            converter.Convert(input, null).Should().Be(TimeOnly.FromTimeSpan(input.TimeOfDay));
            converter.Convert(input, mockProvider.Object).Should().Be(TimeOnly.FromTimeSpan(input.TimeOfDay));

            mockProvider.VerifyAll();
        }
    }
}