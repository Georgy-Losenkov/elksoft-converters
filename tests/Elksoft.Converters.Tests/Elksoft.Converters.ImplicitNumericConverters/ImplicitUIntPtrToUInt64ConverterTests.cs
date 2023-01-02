#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUIntPtrToUInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUIntPtrToUInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUIntPtrToUInt64Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UIntPtr, UInt64> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr, UInt64>() {
                    { UIntPtr.Zero, 0UL },
                    { new UIntPtr(1), 1UL },
                    { new UIntPtr(127), (UInt64)SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), (UInt64)Byte.MaxValue },
                    { new UIntPtr(32767), (UInt64)Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), (UInt64)UInt16.MaxValue },
                    { new UIntPtr(Int32.MaxValue), (UInt64)Int32.MaxValue },
                    { new UIntPtr(UInt32.MaxValue), (UInt64)UInt32.MaxValue },
                };
            }
            else
            {
                return new TheoryData<UIntPtr, UInt64>() {
                    { UIntPtr.Zero, 0UL },
                    { new UIntPtr(1), 1UL },
                    { new UIntPtr(127), (UInt64)SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), (UInt64)Byte.MaxValue },
                    { new UIntPtr(32767), (UInt64)Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), (UInt64)UInt16.MaxValue },
                    { new UIntPtr(Int32.MaxValue), (UInt64)Int32.MaxValue },
                    { new UIntPtr(UInt32.MaxValue), (UInt64)UInt32.MaxValue },
                    { new UIntPtr(Int64.MaxValue), (UInt64)Int64.MaxValue },
                    { new UIntPtr(UInt64.MaxValue), UInt64.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, UInt64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUIntPtrToUInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
