#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedIntPtrToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedIntPtrToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedIntPtrToUInt128Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, UInt128> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<IntPtr, UInt128>() {
                    { IntPtr.Zero, UInt128.Zero },
                    { new IntPtr(1), UInt128.One },
                    { new IntPtr(SByte.MaxValue), (UInt128)SByte.MaxValue },
                    { new IntPtr(Byte.MaxValue), (UInt128)Byte.MaxValue },
                    { new IntPtr(Int16.MaxValue), (UInt128)Int16.MaxValue },
                    { new IntPtr(UInt16.MaxValue), (UInt128)UInt16.MaxValue },
                    { new IntPtr(Int32.MaxValue), (UInt128)Int32.MaxValue },
                };
            }
            else
            {
                return new TheoryData<IntPtr, UInt128>() {
                    { IntPtr.Zero, UInt128.Zero },
                    { new IntPtr(1), UInt128.One },
                    { new IntPtr(SByte.MaxValue), (UInt128)SByte.MaxValue },
                    { new IntPtr(Byte.MaxValue), (UInt128)Byte.MaxValue },
                    { new IntPtr(Int16.MaxValue), (UInt128)Int16.MaxValue },
                    { new IntPtr(UInt16.MaxValue), (UInt128)UInt16.MaxValue },
                    { new IntPtr(Int32.MaxValue), (UInt128)Int32.MaxValue },
                    { new IntPtr(UInt32.MaxValue), (UInt128)UInt32.MaxValue },
                    { new IntPtr(Int64.MaxValue), (UInt128)Int64.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedIntPtrToUInt128Converter();
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
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(IntPtr input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedIntPtrToUInt128Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
