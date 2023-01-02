using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt16ToSingleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt16ToSingleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt16ToSingleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt16, Single> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt16, Single>() {
                { (UInt16)0, 0.0f },
                { (UInt16)1, 1.0f },
                { (UInt16)SByte.MaxValue, SByte.MaxValue },
                { (UInt16)Byte.MaxValue, Byte.MaxValue },
                { (UInt16)Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt16 input, Single output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt16ToSingleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
