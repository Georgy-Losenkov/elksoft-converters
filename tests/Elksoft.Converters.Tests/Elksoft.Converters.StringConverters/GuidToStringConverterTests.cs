using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StringConverters
{
    public sealed class GuidToStringConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new GuidToStringConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new GuidToStringConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Guid> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Guid>() {
                { Guid.Empty },
                { Guid.NewGuid() },
                { Guid.NewGuid() },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Guid input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new GuidToStringConverter();
            converter.Convert(input, null).Should().Be(input.ToString());
            converter.Convert(input, mockProvider.Object).Should().Be(input.ToString());

            mockProvider.VerifyAll();
        }
    }
}