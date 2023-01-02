#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedIntPtrToUInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedIntPtrToUInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedIntPtrToUInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, UInt16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<IntPtr, UInt16>() {
                { IntPtr.Zero, (UInt16)0 },
                { new IntPtr(1), (UInt16)1 },
                { new IntPtr(SByte.MaxValue), (UInt16)SByte.MaxValue },
                { new IntPtr(Byte.MaxValue), (UInt16)Byte.MaxValue },
                { new IntPtr(Int16.MaxValue), (UInt16)Int16.MaxValue },
                { new IntPtr(UInt16.MaxValue), UInt16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, UInt16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedIntPtrToUInt16Converter();
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
                    { new IntPtr(Int32.MaxValue) },
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
                    { new IntPtr(Int32.MaxValue) },
                    { new IntPtr(UInt32.MaxValue) },
                    { new IntPtr(Int64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(IntPtr input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedIntPtrToUInt16Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
