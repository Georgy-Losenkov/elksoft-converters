#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt64ToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt64ToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt64ToUInt128Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt64, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, UInt128>() {
                { 0UL, UInt128.Zero },
                { 1UL, UInt128.One },
                { (UInt64)SByte.MaxValue, (UInt128)SByte.MaxValue },
                { (UInt64)Byte.MaxValue, (UInt128)Byte.MaxValue },
                { (UInt64)Int16.MaxValue, (UInt128)Int16.MaxValue },
                { (UInt64)UInt16.MaxValue, (UInt128)UInt16.MaxValue },
                { (UInt64)Int32.MaxValue, (UInt128)Int32.MaxValue },
                { (UInt64)UInt32.MaxValue, (UInt128)UInt32.MaxValue },
                { (UInt64)Int64.MaxValue, (UInt128)Int64.MaxValue },
                { UInt64.MaxValue, (UInt128)UInt64.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt64ToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
