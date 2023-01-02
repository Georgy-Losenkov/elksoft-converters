using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitSByteToInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitSByteToInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitSByteToInt32Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<SByte, Int32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<SByte, Int32>() {
                { SByte.MinValue, SByte.MinValue },
                { (SByte)(-1), -1 },
                { (SByte)0, 0 },
                { (SByte)1, 1 },
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(SByte input, Int32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitSByteToInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
