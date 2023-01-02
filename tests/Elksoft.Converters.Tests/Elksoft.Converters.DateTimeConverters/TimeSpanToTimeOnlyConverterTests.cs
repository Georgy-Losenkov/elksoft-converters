using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.DateTimeConverters
{
    public sealed class TimeSpanToTimeOnlyConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new TimeSpanToTimeOnlyConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new TimeSpanToTimeOnlyConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<TimeSpan> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<TimeSpan>() {
                { TimeSpan.Zero },
                { TimeSpan.FromMilliseconds(45.5) },
                { TimeSpan.FromSeconds(45.5) },
                { TimeSpan.FromMinutes(45.5) },
                { TimeSpan.FromHours(23.5) },
                { TimeSpan.FromDays(0.5) },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(TimeSpan input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new TimeSpanToTimeOnlyConverter();
            converter.Convert(input, null).Should().Be(TimeOnly.FromTimeSpan(input));
            converter.Convert(input, mockProvider.Object).Should().Be(TimeOnly.FromTimeSpan(input));

            mockProvider.VerifyAll();
        }

        public static TheoryData<TimeSpan> Convert_ThrowsException_Data()
        {
            return new TheoryData<TimeSpan>() {
                { TimeSpan.MinValue},
                { TimeSpan.FromDays(-1) },
                { TimeSpan.FromMilliseconds(-1) },
                { TimeSpan.FromDays(1) },
                { TimeSpan.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(TimeSpan input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new TimeSpanToTimeOnlyConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}