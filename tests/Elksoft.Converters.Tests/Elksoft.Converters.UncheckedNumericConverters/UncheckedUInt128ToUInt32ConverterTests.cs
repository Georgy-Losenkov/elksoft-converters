#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt128ToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt128ToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt128ToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt128, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt128, UInt32>() {
                { UInt128.Zero, 0U },
                { UInt128.One, 1U },
                { (UInt128)SByte.MaxValue, (UInt32)SByte.MaxValue },
                { (UInt128)Byte.MaxValue, (UInt32)Byte.MaxValue },
                { (UInt128)Int16.MaxValue, (UInt32)Int16.MaxValue },
                { (UInt128)UInt16.MaxValue, (UInt32)UInt16.MaxValue },
                { (UInt128)Int32.MaxValue, (UInt32)Int32.MaxValue },
                { (UInt128)UInt32.MaxValue, UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt128ToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
