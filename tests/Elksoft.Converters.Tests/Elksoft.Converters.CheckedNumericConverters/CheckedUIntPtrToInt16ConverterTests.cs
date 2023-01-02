#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUIntPtrToInt16ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUIntPtrToInt16Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUIntPtrToInt16Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UIntPtr, Int16> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UIntPtr, Int16>() {
                { UIntPtr.Zero, (Int16)0 },
                { new UIntPtr(1), (Int16)1 },
                { new UIntPtr(127), SByte.MaxValue },
                { new UIntPtr(Byte.MaxValue), Byte.MaxValue },
                { new UIntPtr(32767), Int16.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, Int16 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUIntPtrToInt16Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<UIntPtr> Convert_ThrowsException_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr>() {
                    { new UIntPtr(UInt16.MaxValue) },
                    { new UIntPtr(Int32.MaxValue) },
                    { new UIntPtr(UInt32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<UIntPtr>() {
                    { new UIntPtr(UInt16.MaxValue) },
                    { new UIntPtr(Int32.MaxValue) },
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

            var converter = new CheckedUIntPtrToInt16Converter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
