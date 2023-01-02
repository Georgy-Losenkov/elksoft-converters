#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUInt32ToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUInt32ToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUInt32ToIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt32, IntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UInt32, IntPtr>() {
                    { 0U, IntPtr.Zero },
                    { 1U, new IntPtr(1) },
                    { (UInt32)SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { (UInt32)Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { (UInt32)Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { (UInt32)UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { (UInt32)Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<UInt32, IntPtr>() {
                    { 0U, IntPtr.Zero },
                    { 1U, new IntPtr(1) },
                    { (UInt32)SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { (UInt32)Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { (UInt32)Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { (UInt32)UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { (UInt32)Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                    { UInt32.MaxValue, new IntPtr(UInt32.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt32 input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt32ToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        [Fact]
        public static void Convert_X86_ThrowsException()
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            if (IntPtr.Size == 4)
            {
                var converter = new CheckedUInt32ToIntPtrConverter();
                converter.Invoking(x => x.Convert(UInt32.MaxValue, null)).Should().Throw<Exception>();
                converter.Invoking(x => x.Convert(UInt32.MaxValue, mockProvider.Object)).Should().Throw<Exception>();
            }

            mockProvider.VerifyAll();
        }
    }
}
#endif
