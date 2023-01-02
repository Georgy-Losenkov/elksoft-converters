#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt128ToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt128ToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt128ToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt128, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt128, UInt16>() {
                { UInt128.Zero, (UInt16)0 },
                { UInt128.One, (UInt16)1 },
                { (UInt128)SByte.MaxValue, (UInt16)SByte.MaxValue },
                { (UInt128)Byte.MaxValue, (UInt16)Byte.MaxValue },
                { (UInt128)Int16.MaxValue, (UInt16)Int16.MaxValue },
                { (UInt128)UInt16.MaxValue, UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt128ToUInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
