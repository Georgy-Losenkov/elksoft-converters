#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt32ToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt32ToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt32ToUInt128Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt32, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt32, UInt128>() {
                { 0U, UInt128.Zero },
                { 1U, UInt128.One },
                { (UInt32)SByte.MaxValue, (UInt128)SByte.MaxValue },
                { (UInt32)Byte.MaxValue, (UInt128)Byte.MaxValue },
                { (UInt32)Int16.MaxValue, (UInt128)Int16.MaxValue },
                { (UInt32)UInt16.MaxValue, (UInt128)UInt16.MaxValue },
                { (UInt32)Int32.MaxValue, (UInt128)Int32.MaxValue },
                { UInt32.MaxValue, (UInt128)UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt32ToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
