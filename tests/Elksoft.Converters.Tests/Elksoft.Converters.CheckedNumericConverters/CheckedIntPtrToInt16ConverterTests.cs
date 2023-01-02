#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedIntPtrToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedIntPtrToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedIntPtrToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<IntPtr, Int16>() {
                { new IntPtr(Int16.MinValue), Int16.MinValue },
                { new IntPtr(SByte.MinValue), SByte.MinValue },
                { new IntPtr(-1), -1 },
                { IntPtr.Zero, (Int16)0 },
                { new IntPtr(1), (Int16)1 },
                { new IntPtr(SByte.MaxValue), SByte.MaxValue },
                { new IntPtr(Byte.MaxValue), Byte.MaxValue },
                { new IntPtr(Int16.MaxValue), Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedIntPtrToInt16Converter();
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
                    { new IntPtr(UInt16.MaxValue) },
                    { new IntPtr(Int32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<IntPtr>() {
                    { new IntPtr(Int64.MinValue) },
                    { new IntPtr(Int32.MinValue) },
                    { new IntPtr(UInt16.MaxValue) },
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

            var converter = new CheckedIntPtrToInt16Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
