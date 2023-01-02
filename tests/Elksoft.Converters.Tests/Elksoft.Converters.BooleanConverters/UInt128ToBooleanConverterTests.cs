#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class UInt128ToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UInt128ToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new UInt128ToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt128, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt128, Boolean>() {
                { UInt128.Zero, false },
                { UInt128.One, true },
                { (UInt128)SByte.MaxValue, true },
                { (UInt128)Byte.MaxValue, true },
                { (UInt128)Int16.MaxValue, true },
                { (UInt128)UInt16.MaxValue, true },
                { (UInt128)Int32.MaxValue, true },
                { (UInt128)UInt32.MaxValue, true },
                { (UInt128)Int64.MaxValue, true },
                { (UInt128)UInt64.MaxValue, true },
                { (UInt128)Int128.MaxValue, true },
                { UInt128.MaxValue, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UInt128ToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
