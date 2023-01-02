using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt32ToDoubleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt32ToDoubleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt32ToDoubleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt32, Double> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt32, Double>() {
                { 0U, 0.0 },
                { 1U, 1.0 },
                { (UInt32)SByte.MaxValue, SByte.MaxValue },
                { (UInt32)Byte.MaxValue, Byte.MaxValue },
                { (UInt32)Int16.MaxValue, Int16.MaxValue },
                { (UInt32)UInt16.MaxValue, UInt16.MaxValue },
                { (UInt32)Int32.MaxValue, Int32.MaxValue },
                { UInt32.MaxValue, UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, Double output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt32ToDoubleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
