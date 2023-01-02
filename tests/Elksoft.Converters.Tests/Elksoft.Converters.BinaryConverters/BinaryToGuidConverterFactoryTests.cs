using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BinaryConverters
{
    public sealed class BinaryToGuidConverterFactoryTests
    {
        [Fact]
        public static void AcceptsNull_ReturnsFalse()
        {
            var converter = new BinaryToGuidConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new BinaryToGuidConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Byte[], Guid> Convert_ReturnsExpected_Data()
        {
            var result = new TheoryData<Byte[], Guid>();

            result.Add(null, Guid.Empty);
            result.Add(Array.Empty<Byte>(), Guid.Empty);

            var random = new Random();

            for (var i = 0; i <= 32; i++)
            {
                var sample = new Byte[i];
                random.NextBytes(sample);

                var buffer = new Byte[16];
                Array.Copy(sample, 0, buffer, 0, Math.Min(sample.Length, buffer.Length));

                result.Add(sample, new Guid(buffer));
            }

            return result;
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Byte[] input, Guid output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new BinaryToGuidConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}