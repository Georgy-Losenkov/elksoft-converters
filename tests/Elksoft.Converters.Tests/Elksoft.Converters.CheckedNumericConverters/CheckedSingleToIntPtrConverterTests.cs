#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedSingleToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedSingleToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedSingleToIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, IntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Single, IntPtr>() {
                    { (Single)Half.MinValue, IntPtr.Parse("-65504") },
                    { Int32.MinValue, new IntPtr(Int32.MinValue) },
                    { Int16.MinValue, new IntPtr(Int16.MinValue) },
                    { SByte.MinValue, new IntPtr(SByte.MinValue) },
                    { -1.0f, new IntPtr(-1) },
                    { 0.0f, IntPtr.Zero },
                    { (Single)Half.Epsilon, IntPtr.Zero },
                    { Single.Epsilon, IntPtr.Zero },
                    { 1.0f, new IntPtr(1) },
                    { (Single)Half.E, IntPtr.Parse("2") },
                    { Single.E, IntPtr.Parse("2") },
                    { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { (Single)Half.MaxValue, IntPtr.Parse("65504") },
                };
            }
            else
            {
                return new TheoryData<Single, IntPtr>() {
                    { (Single)Half.MinValue, IntPtr.Parse("-65504") },
                    { Int64.MinValue, new IntPtr(Int64.MinValue) },
                    { Int32.MinValue, new IntPtr(Int32.MinValue) },
                    { Int16.MinValue, new IntPtr(Int16.MinValue) },
                    { SByte.MinValue, new IntPtr(SByte.MinValue) },
                    { -1.0f, new IntPtr(-1) },
                    { 0.0f, IntPtr.Zero },
                    { (Single)Half.Epsilon, IntPtr.Zero },
                    { Single.Epsilon, IntPtr.Zero },
                    { 1.0f, new IntPtr(1) },
                    { (Single)Half.E, IntPtr.Parse("2") },
                    { Single.E, IntPtr.Parse("2") },
                    { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, IntPtr.Parse("2147483648") },
                    { UInt32.MaxValue, IntPtr.Parse("4294967296") },
                    { (Single)Half.MaxValue, IntPtr.Parse("65504") },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedSingleToIntPtrConverter();
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
                    { (Single)Int128.MinValue },
                    { Int64.MinValue },
                    { Int32.MaxValue },
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
                    { (Single)Int128.MinValue },
                    { Int64.MaxValue },
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

            var converter = new CheckedSingleToIntPtrConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
