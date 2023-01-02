#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToInt128Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, Int128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Int128>() {
                { (Single)Half.MinValue, Int128.Parse("-65504") },
                { (Single)Int128.MinValue, Int128.MinValue },
                { Int64.MinValue, Int64.MinValue },
                { Int32.MinValue, Int32.MinValue },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0f, -1 },
                { Single.NegativeZero, Int128.Zero },
                { 0.0f, Int128.Zero },
                { (Single)Half.Epsilon, Int128.Zero },
                { Single.Epsilon, Int128.Zero },
                { 1.0f, Int128.One },
                { (Single)Half.E, Int128.Parse("2") },
                { Single.E, Int128.Parse("2") },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Int128.Parse("2147483648") },
                { UInt32.MaxValue, Int128.Parse("4294967296") },
                { Int64.MaxValue, Int128.Parse("9223372036854775808") },
                { UInt64.MaxValue, Int128.Parse("18446744073709551616") },
                { (Single)Half.MaxValue, Int128.Parse("65504") },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Int128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToInt128Converter();
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
                { (Single)Int128.MaxValue },
                { Single.MaxValue },
                { Single.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Single input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToInt128Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
