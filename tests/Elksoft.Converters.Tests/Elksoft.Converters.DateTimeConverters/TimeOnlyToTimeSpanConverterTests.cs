using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.DateTimeConverters
{
    public sealed class TimeOnlyToTimeSpanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new TimeOnlyToTimeSpanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new TimeOnlyToTimeSpanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<TimeOnly> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<TimeOnly>() {
                { TimeOnly.MinValue },
                { TimeOnly.MaxValue },
                { new TimeOnly(23, 45, 56) },
                { new TimeOnly(12, 23, 34, 456) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(TimeOnly input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new TimeOnlyToTimeSpanConverter();
            converter.Convert(input, null).Should().Be(input.ToTimeSpan());
            converter.Convert(input, mockProvider.Object).Should().Be(input.ToTimeSpan());

            mockProvider.VerifyAll();
        }
    }
}