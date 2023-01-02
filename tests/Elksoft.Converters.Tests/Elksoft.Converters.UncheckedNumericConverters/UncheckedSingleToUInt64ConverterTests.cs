using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToUInt64Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, UInt64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, UInt64>() {
                { 0.0f, 0UL },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, 0UL },
#endif
                { Single.Epsilon, 0UL },
                { 1.0f, 1UL },
#if NET7_0_OR_GREATER
                { (Single)Half.E, UInt64.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, UInt64.Parse("2") },
#endif
                { SByte.MaxValue, (UInt64)SByte.MaxValue },
                { Byte.MaxValue, (UInt64)Byte.MaxValue },
                { Int16.MaxValue, (UInt64)Int16.MaxValue },
                { UInt16.MaxValue, (UInt64)UInt16.MaxValue },
                { Int32.MaxValue, UInt64.Parse("2147483648") },
                { UInt32.MaxValue, UInt64.Parse("4294967296") },
                { Int64.MaxValue, UInt64.Parse("9223372036854775808") },
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue, UInt64.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedSingleToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
