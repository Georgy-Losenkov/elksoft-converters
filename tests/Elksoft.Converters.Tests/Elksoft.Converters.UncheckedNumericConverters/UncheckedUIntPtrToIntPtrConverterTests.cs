#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedUIntPtrToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedUIntPtrToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedUIntPtrToIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UIntPtr, IntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr, IntPtr>() {
                    { UIntPtr.Zero, IntPtr.Zero },
                    { new UIntPtr(1), new IntPtr(1) },
                    { new UIntPtr(127), new IntPtr(SByte.MaxValue) },
                    { new UIntPtr(Byte.MaxValue), new IntPtr(Byte.MaxValue) },
                    { new UIntPtr(32767), new IntPtr(Int16.MaxValue) },
                    { new UIntPtr(UInt16.MaxValue), new IntPtr(UInt16.MaxValue) },
                    { new UIntPtr(Int32.MaxValue), new IntPtr(Int32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<UIntPtr, IntPtr>() {
                    { UIntPtr.Zero, IntPtr.Zero },
                    { new UIntPtr(1), new IntPtr(1) },
                    { new UIntPtr(127), new IntPtr(SByte.MaxValue) },
                    { new UIntPtr(Byte.MaxValue), new IntPtr(Byte.MaxValue) },
                    { new UIntPtr(32767), new IntPtr(Int16.MaxValue) },
                    { new UIntPtr(UInt16.MaxValue), new IntPtr(UInt16.MaxValue) },
                    { new UIntPtr(Int32.MaxValue), new IntPtr(Int32.MaxValue) },
                    { new UIntPtr(UInt32.MaxValue), new IntPtr(UInt32.MaxValue) },
                    { new UIntPtr(Int64.MaxValue), new IntPtr(Int64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedUIntPtrToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
