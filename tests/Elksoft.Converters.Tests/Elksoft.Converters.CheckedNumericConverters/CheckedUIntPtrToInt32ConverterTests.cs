#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUIntPtrToInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUIntPtrToInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUIntPtrToInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UIntPtr, Int32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UIntPtr, Int32>() {
                { UIntPtr.Zero, 0 },
                { new UIntPtr(1), 1 },
                { new UIntPtr(127), SByte.MaxValue },
                { new UIntPtr(Byte.MaxValue), Byte.MaxValue },
                { new UIntPtr(32767), Int16.MaxValue },
                { new UIntPtr(UInt16.MaxValue), UInt16.MaxValue },
                { new UIntPtr(Int32.MaxValue), Int32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, Int32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUIntPtrToInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<UIntPtr> Convert_ThrowsException_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr>() {
                    { new UIntPtr(UInt32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<UIntPtr>() {
                    { new UIntPtr(UInt32.MaxValue) },
                    { new UIntPtr(Int64.MaxValue) },
                    { new UIntPtr(UInt64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(UIntPtr input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUIntPtrToInt32Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
