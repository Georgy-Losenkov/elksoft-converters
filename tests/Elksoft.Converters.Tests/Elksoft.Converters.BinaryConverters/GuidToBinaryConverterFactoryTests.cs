using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BinaryConverters
{
    public sealed class GuidToBinaryConverterFactoryTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new GuidToBinaryConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new GuidToBinaryConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Guid> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Guid>() {
                { Guid.Empty },
                { Guid.NewGuid() },
                { Guid.NewGuid() },
                { Guid.NewGuid() },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Guid input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new GuidToBinaryConverter();
            converter.Convert(input, null).Should().BeEquivalentTo(input.ToByteArray());
            converter.Convert(input, mockProvider.Object).Should().BeEquivalentTo(input.ToByteArray());

            mockProvider.VerifyAll();
        }
    }
}