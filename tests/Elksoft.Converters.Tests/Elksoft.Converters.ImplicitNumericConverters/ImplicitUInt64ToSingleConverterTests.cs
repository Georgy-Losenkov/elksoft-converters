using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUInt64ToSingleConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUInt64ToSingleConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUInt64ToSingleConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UInt64, Single> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UInt64, Single>() {
#if NET7_0_OR_GREATER
                { 0UL, Single.NegativeZero },
#endif
                { 1UL, 1.0f },
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
        public static void Convert_ReturnsExpected(UInt64 input, Single output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUInt64ToSingleConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
