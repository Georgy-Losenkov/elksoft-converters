#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToInt128Converter();
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

            var converter = new UncheckedSingleToInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
