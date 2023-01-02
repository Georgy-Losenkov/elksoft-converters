using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToDecimalConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToDecimalConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToDecimalConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, Decimal> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Decimal>() {
#if NET7_0_OR_GREATER
                { (Single)Half.MinValue, Decimal.Parse("-65504") },
#endif
                { Int64.MinValue, Decimal.Parse("-9223372000000000000") },
                { Int32.MinValue, Decimal.Parse("-2147484000") },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0f, -1m },
                { 0.0f, Decimal.Zero },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, Decimal.Parse("0.00000005960464") },
#endif
                { Single.Epsilon, Decimal.Zero },
                { 1.0f, Decimal.One },
#if NET7_0_OR_GREATER
                { (Single)Half.E, Decimal.Parse("2.71875") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Decimal.Parse("2.718282") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Decimal.Parse("2147484000") },
                { UInt32.MaxValue, Decimal.Parse("4294967000") },
                { Int64.MaxValue, Decimal.Parse("9223372000000000000") },
                { UInt64.MaxValue, Decimal.Parse("18446740000000000000") },
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue, Decimal.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Decimal output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToDecimalConverter();
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
                { (Single)Int128.MinValue },
#endif
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

            var converter = new CheckedSingleToDecimalConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
