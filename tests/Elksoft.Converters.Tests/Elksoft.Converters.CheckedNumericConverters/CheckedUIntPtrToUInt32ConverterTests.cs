#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUIntPtrToUInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUIntPtrToUInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUIntPtrToUInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UIntPtr, UInt32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<UIntPtr, UInt32>() {
                { UIntPtr.Zero, 0U },
                { new UIntPtr(1), 1U },
                { new UIntPtr(127), (UInt32)SByte.MaxValue },
                { new UIntPtr(Byte.MaxValue), (UInt32)Byte.MaxValue },
                { new UIntPtr(32767), (UInt32)Int16.MaxValue },
                { new UIntPtr(UInt16.MaxValue), (UInt32)UInt16.MaxValue },
                { new UIntPtr(Int32.MaxValue), (UInt32)Int32.MaxValue },
                { new UIntPtr(UInt32.MaxValue), UInt32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, UInt32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUIntPtrToUInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        [Fact]
        public static void Convert_X64_ThrowsException()
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            if (IntPtr.Size == 8)
            {
                var converter = new CheckedUIntPtrToUInt32Converter();
                converter.Invoking(x => x.Convert(new UIntPtr(Int64.MaxValue), null)).Should().Throw<Exception>();
                converter.Invoking(x => x.Convert(new UIntPtr(UInt64.MaxValue), null)).Should().Throw<Exception>();
                converter.Invoking(x => x.Convert(new UIntPtr(Int64.MaxValue), mockProvider.Object)).Should().Throw<Exception>();
                converter.Invoking(x => x.Convert(new UIntPtr(UInt64.MaxValue), mockProvider.Object)).Should().Throw<Exception>();
            }

            mockProvider.VerifyAll();
        }
    }
}
#endif
