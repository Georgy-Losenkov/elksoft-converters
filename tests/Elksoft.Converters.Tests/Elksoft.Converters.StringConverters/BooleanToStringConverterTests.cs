using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class BooleanToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new BooleanToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new BooleanToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Boolean>() {
                { false },
                { true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Boolean input)
        {
            var mockFormatProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new BooleanToStringConverter();
            converter.Convert(input, null).Should().Be(input.ToString());
            converter.Convert(input, mockFormatProvider.Object).Should().Be(input.ToString());

            mockFormatProvider.VerifyAll();
        }
    }
}
