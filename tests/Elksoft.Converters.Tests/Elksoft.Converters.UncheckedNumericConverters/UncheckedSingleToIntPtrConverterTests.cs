#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToIntPtrConverter();
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
                    { Single.NegativeZero, IntPtr.Zero },
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
                    { Single.NegativeZero, IntPtr.Zero },
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

            var converter = new UncheckedSingleToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
