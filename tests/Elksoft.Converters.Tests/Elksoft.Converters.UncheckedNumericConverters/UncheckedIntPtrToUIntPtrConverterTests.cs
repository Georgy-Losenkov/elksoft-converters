#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedIntPtrToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedIntPtrToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedIntPtrToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, UIntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<IntPtr, UIntPtr>() {
                    { IntPtr.Zero, UIntPtr.Zero },
                    { new IntPtr(1), new UIntPtr(1) },
                    { new IntPtr(SByte.MaxValue), new UIntPtr(127) },
                    { new IntPtr(Byte.MaxValue), new UIntPtr(Byte.MaxValue) },
                    { new IntPtr(Int16.MaxValue), new UIntPtr(32767) },
                    { new IntPtr(UInt16.MaxValue), new UIntPtr(UInt16.MaxValue) },
                    { new IntPtr(Int32.MaxValue), new UIntPtr(Int32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<IntPtr, UIntPtr>() {
                    { IntPtr.Zero, UIntPtr.Zero },
                    { new IntPtr(1), new UIntPtr(1) },
                    { new IntPtr(SByte.MaxValue), new UIntPtr(127) },
                    { new IntPtr(Byte.MaxValue), new UIntPtr(Byte.MaxValue) },
                    { new IntPtr(Int16.MaxValue), new UIntPtr(32767) },
                    { new IntPtr(UInt16.MaxValue), new UIntPtr(UInt16.MaxValue) },
                    { new IntPtr(Int32.MaxValue), new UIntPtr(Int32.MaxValue) },
                    { new IntPtr(UInt32.MaxValue), new UIntPtr(UInt32.MaxValue) },
                    { new IntPtr(Int64.MaxValue), new UIntPtr(Int64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedIntPtrToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
