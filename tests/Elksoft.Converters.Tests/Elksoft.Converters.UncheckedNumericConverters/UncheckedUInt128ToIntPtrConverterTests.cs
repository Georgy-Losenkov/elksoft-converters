#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUInt128ToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUInt128ToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUInt128ToIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt128, IntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UInt128, IntPtr>() {
                    { UInt128.Zero, IntPtr.Zero },
                    { UInt128.One, new IntPtr(1) },
                    { (UInt128)SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { (UInt128)Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { (UInt128)Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { (UInt128)UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { (UInt128)Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<UInt128, IntPtr>() {
                    { UInt128.Zero, IntPtr.Zero },
                    { UInt128.One, new IntPtr(1) },
                    { (UInt128)SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { (UInt128)Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { (UInt128)Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { (UInt128)UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { (UInt128)Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                    { (UInt128)UInt32.MaxValue, new IntPtr(UInt32.MaxValue) },
                    { (UInt128)Int64.MaxValue, new IntPtr(Int64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUInt128ToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
