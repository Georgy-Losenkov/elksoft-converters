#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt64ToInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt64ToInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt64ToInt128Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt64, Int128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, Int128>() {
                { 0UL, Int128.Zero },
                { 1UL, Int128.One },
                { (UInt64)SByte.MaxValue, SByte.MaxValue },
                { (UInt64)Byte.MaxValue, Byte.MaxValue },
                { (UInt64)Int16.MaxValue, Int16.MaxValue },
                { (UInt64)UInt16.MaxValue, UInt16.MaxValue },
                { (UInt64)Int32.MaxValue, Int32.MaxValue },
                { (UInt64)UInt32.MaxValue, UInt32.MaxValue },
                { (UInt64)Int64.MaxValue, Int64.MaxValue },
                { UInt64.MaxValue, UInt64.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, Int128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt64ToInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
