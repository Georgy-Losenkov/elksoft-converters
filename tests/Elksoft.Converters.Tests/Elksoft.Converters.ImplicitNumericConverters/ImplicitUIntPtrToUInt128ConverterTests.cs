#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUIntPtrToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUIntPtrToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUIntPtrToUInt128Converter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UIntPtr, UInt128> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr, UInt128>() {
                    { UIntPtr.Zero, UInt128.Zero },
                    { new UIntPtr(1), UInt128.One },
                    { new UIntPtr(127), (UInt128)SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), (UInt128)Byte.MaxValue },
                    { new UIntPtr(32767), (UInt128)Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), (UInt128)UInt16.MaxValue },
                    { new UIntPtr(Int32.MaxValue), (UInt128)Int32.MaxValue },
                    { new UIntPtr(UInt32.MaxValue), (UInt128)UInt32.MaxValue },
                };
            }
            else
            {
                return new TheoryData<UIntPtr, UInt128>() {
                    { UIntPtr.Zero, UInt128.Zero },
                    { new UIntPtr(1), UInt128.One },
                    { new UIntPtr(127), (UInt128)SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), (UInt128)Byte.MaxValue },
                    { new UIntPtr(32767), (UInt128)Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), (UInt128)UInt16.MaxValue },
                    { new UIntPtr(Int32.MaxValue), (UInt128)Int32.MaxValue },
                    { new UIntPtr(UInt32.MaxValue), (UInt128)UInt32.MaxValue },
                    { new UIntPtr(Int64.MaxValue), (UInt128)Int64.MaxValue },
                    { new UIntPtr(UInt64.MaxValue), (UInt128)UInt64.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUIntPtrToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
