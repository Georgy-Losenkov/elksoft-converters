using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToSByteConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToSByteConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToSByteConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, SByte> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, SByte>() {
                { SByte.MinValue, SByte.MinValue },
                { -1.0f, (SByte)(-1) },
                { 0.0f, (SByte)0 },
#if NET7_0_OR_GREATER
                { (Single)Half.Epsilon, (SByte)0 },
#endif
                { Single.Epsilon, (SByte)0 },
                { 1.0f, (SByte)1 },
#if NET7_0_OR_GREATER
                { (Single)Half.E, SByte.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, SByte.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, SByte output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToSByteConverter();
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
                { Byte.MaxValue },
                { Int16.MaxValue },
                { UInt16.MaxValue },
                { Int32.MaxValue },
                { UInt32.MaxValue },
                { Int64.MaxValue },
                { UInt64.MaxValue },
#if NET7_0_OR_GREATER
                { (Single)Int128.MaxValue },
#endif
#if NET7_0_OR_GREATER
                { (Single)Half.MaxValue },
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

            var converter = new CheckedSingleToSByteConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
