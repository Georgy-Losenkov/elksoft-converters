#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt128ToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt128ToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt128ToUInt64Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt128, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt128, UInt64>() {
                { UInt128.Zero, 0UL },
                { UInt128.One, 1UL },
                { (UInt128)SByte.MaxValue, (UInt64)SByte.MaxValue },
                { (UInt128)Byte.MaxValue, (UInt64)Byte.MaxValue },
                { (UInt128)Int16.MaxValue, (UInt64)Int16.MaxValue },
                { (UInt128)UInt16.MaxValue, (UInt64)UInt16.MaxValue },
                { (UInt128)Int32.MaxValue, (UInt64)Int32.MaxValue },
                { (UInt128)UInt32.MaxValue, (UInt64)UInt32.MaxValue },
                { (UInt128)Int64.MaxValue, (UInt64)Int64.MaxValue },
                { (UInt128)UInt64.MaxValue, UInt64.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt128ToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
