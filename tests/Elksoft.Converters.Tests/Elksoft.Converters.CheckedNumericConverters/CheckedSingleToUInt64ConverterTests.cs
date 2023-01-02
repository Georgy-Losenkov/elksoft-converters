using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToUInt64Converter();
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

            var converter = new CheckedSingleToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Single> Convert_ThrowsException_Data()
        {
            return new TheoryData<Single>() {
                { Single.NaN },
                { Single.NegativeInfinity },
                { Single.MinValue },
#if NET7_0_OR_GREATER
                { (Single)Half.MinValue },
#endif
#if NET7_0_OR_GREATER
                { (Single)Int128.MinValue },
#endif
                { Int64.MinValue },
                { Int32.MinValue },
                { Int16.MinValue },
                { SByte.MinValue },
                { -1.0f },
                { UInt64.MaxValue },
#if NET7_0_OR_GREATER
                { (Single)Int128.MaxValue },
#endif
                { Single.MaxValue },
                { Single.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Single input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToUInt64Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
