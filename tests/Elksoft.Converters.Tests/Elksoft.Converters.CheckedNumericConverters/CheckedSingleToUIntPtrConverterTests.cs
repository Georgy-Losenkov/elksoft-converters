#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, UIntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Single, UIntPtr>() {
                    { Single.NegativeZero, UIntPtr.Zero },
                    { 0.0f, UIntPtr.Zero },
                    { (Single)Half.Epsilon, UIntPtr.Zero },
                    { Single.Epsilon, UIntPtr.Zero },
                    { 1.0f, new UIntPtr(1) },
                    { (Single)Half.E, UIntPtr.Parse("2") },
                    { Single.E, UIntPtr.Parse("2") },
                    { SByte.MaxValue, new UIntPtr(127) },
                    { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new UIntPtr(32767) },
                    { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, UIntPtr.Parse("2147483648") },
                    { (Single)Half.MaxValue, UIntPtr.Parse("65504") },
                };
            }
            else
            {
                return new TheoryData<Single, UIntPtr>() {
                    { Single.NegativeZero, UIntPtr.Zero },
                    { 0.0f, UIntPtr.Zero },
                    { (Single)Half.Epsilon, UIntPtr.Zero },
                    { Single.Epsilon, UIntPtr.Zero },
                    { 1.0f, new UIntPtr(1) },
                    { (Single)Half.E, UIntPtr.Parse("2") },
                    { Single.E, UIntPtr.Parse("2") },
                    { SByte.MaxValue, new UIntPtr(127) },
                    { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new UIntPtr(32767) },
                    { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, UIntPtr.Parse("2147483648") },
                    { UInt32.MaxValue, UIntPtr.Parse("4294967296") },
                    { Int64.MaxValue, UIntPtr.Parse("9223372036854775808") },
                    { (Single)Half.MaxValue, UIntPtr.Parse("65504") },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Single> Convert_ThrowsException_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Single>() {
                    { Single.NaN },
                    { Single.NegativeInfinity },
                    { Single.MinValue },
                    { (Single)Half.MinValue },
                    { (Single)Int128.MinValue },
                    { Int64.MinValue },
                    { Int32.MinValue },
                    { Int16.MinValue },
                    { SByte.MinValue },
                    { -1.0f },
                    { UInt32.MaxValue },
                    { Int64.MaxValue },
                    { UInt64.MaxValue },
                    { (Single)Int128.MaxValue },
                    { Single.MaxValue },
                    { Single.PositiveInfinity },
                };
            }
            else
            {
                return new TheoryData<Single>() {
                    { Single.NaN },
                    { Single.NegativeInfinity },
                    { Single.MinValue },
                    { (Single)Half.MinValue },
                    { (Single)Int128.MinValue },
                    { Int64.MinValue },
                    { Int32.MinValue },
                    { Int16.MinValue },
                    { SByte.MinValue },
                    { -1.0f },
                    { UInt64.MaxValue },
                    { (Single)Int128.MaxValue },
                    { Single.MaxValue },
                    { Single.PositiveInfinity },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Single input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToUIntPtrConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
