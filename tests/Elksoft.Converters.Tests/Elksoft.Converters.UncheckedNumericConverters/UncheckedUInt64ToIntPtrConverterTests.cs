#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt64ToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt64ToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt64ToIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt64, IntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UInt64, IntPtr>() {
                    { 0UL, IntPtr.Zero },
                    { 1UL, new IntPtr(1) },
                    { (UInt64)SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { (UInt64)Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { (UInt64)Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { (UInt64)UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { (UInt64)Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<UInt64, IntPtr>() {
                    { 0UL, IntPtr.Zero },
                    { 1UL, new IntPtr(1) },
                    { (UInt64)SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { (UInt64)Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { (UInt64)Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { (UInt64)UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { (UInt64)Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                    { (UInt64)UInt32.MaxValue, new IntPtr(UInt32.MaxValue) },
                    { (UInt64)Int64.MaxValue, new IntPtr(Int64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt64ToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
