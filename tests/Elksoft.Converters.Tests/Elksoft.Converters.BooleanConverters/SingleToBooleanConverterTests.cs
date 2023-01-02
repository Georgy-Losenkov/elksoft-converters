using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.BooleanConverters
{
    public class SingleToBooleanConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new SingleToBooleanConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new SingleToBooleanConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Single, Boolean> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Boolean>() {
                { Single.NaN, true },
                { Single.NegativeInfinity, true },
                { Single.MinValue, true },
#if NET7_0_OR_GREATER
                { (Single)Half.MinValue, true },
#endif
#if NET7_0_OR_GREATER
                { (Single)Int128.MinValue, true },
#endif
                { Int64.MinValue, true },
                { Int32.MinValue, true },
                { Int16.MinValue, true },
                { SByte.MinValue, true },
                { -1.0f, true },
#if NET7_0_OR_GREATER
                { Single.NegativeZero, false },
#endif
                { 0.0f, false },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, true },
#endif
                { Single.Epsilon, true },
                { 1.0f, true },
#if NET7_0_OR_GREATER
                { (Single)Half.E, true },
#endif
#if NET7_0_OR_GREATER
                { Single.E, true },
#endif
                { SByte.MaxValue, true },
                { Byte.MaxValue, true },
                { Int16.MaxValue, true },
                { UInt16.MaxValue, true },
                { Int32.MaxValue, true },
                { UInt32.MaxValue, true },
                { Int64.MaxValue, true },
                { UInt64.MaxValue, true },
#if NET7_0_OR_GREATER
                { (Single)Int128.MaxValue, true },
#endif
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue, true },
#endif
                { Single.MaxValue, true },
                { Single.PositiveInfinity, true },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Boolean output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new SingleToBooleanConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
