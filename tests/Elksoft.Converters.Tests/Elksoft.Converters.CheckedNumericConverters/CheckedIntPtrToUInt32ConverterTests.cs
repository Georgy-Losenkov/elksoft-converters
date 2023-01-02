#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedIntPtrToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedIntPtrToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedIntPtrToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, UInt32> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<IntPtr, UInt32>() {
                    { IntPtr.Zero, 0U },
                    { new IntPtr(1), 1U },
                    { new IntPtr(SByte.MaxValue), (UInt32)SByte.MaxValue },
                    { new IntPtr(Byte.MaxValue), (UInt32)Byte.MaxValue },
                    { new IntPtr(Int16.MaxValue), (UInt32)Int16.MaxValue },
                    { new IntPtr(UInt16.MaxValue), (UInt32)UInt16.MaxValue },
                    { new IntPtr(Int32.MaxValue), (UInt32)Int32.MaxValue },
                };
            }
            else
            {
                return new TheoryData<IntPtr, UInt32>() {
                    { IntPtr.Zero, 0U },
                    { new IntPtr(1), 1U },
                    { new IntPtr(SByte.MaxValue), (UInt32)SByte.MaxValue },
                    { new IntPtr(Byte.MaxValue), (UInt32)Byte.MaxValue },
                    { new IntPtr(Int16.MaxValue), (UInt32)Int16.MaxValue },
                    { new IntPtr(UInt16.MaxValue), (UInt32)UInt16.MaxValue },
                    { new IntPtr(Int32.MaxValue), (UInt32)Int32.MaxValue },
                    { new IntPtr(UInt32.MaxValue), UInt32.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedIntPtrToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<IntPtr> Convert_ThrowsException_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<IntPtr>() {
                    { new IntPtr(Int32.MinValue) },
                    { new IntPtr(Int16.MinValue) },
                    { new IntPtr(SByte.MinValue) },
                    { new IntPtr(-1) },
                };
            }
            else
            {
                return new TheoryData<IntPtr>() {
                    { new IntPtr(Int64.MinValue) },
                    { new IntPtr(Int32.MinValue) },
                    { new IntPtr(Int16.MinValue) },
                    { new IntPtr(SByte.MinValue) },
                    { new IntPtr(-1) },
                    { new IntPtr(Int64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(IntPtr input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedIntPtrToUInt32Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
