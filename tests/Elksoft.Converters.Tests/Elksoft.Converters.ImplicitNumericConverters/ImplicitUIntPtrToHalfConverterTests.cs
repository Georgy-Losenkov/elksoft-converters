#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUIntPtrToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUIntPtrToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUIntPtrToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UIntPtr, Half> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr, Half>() {
                    { UIntPtr.Zero, Half.NegativeZero },
                    { new UIntPtr(1), Half.One },
                    { new UIntPtr(127), SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new UIntPtr(32767), (Half)Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), Half.PositiveInfinity },
                    { new UIntPtr(Int32.MaxValue), Half.PositiveInfinity },
                    { new UIntPtr(UInt32.MaxValue), Half.PositiveInfinity },
                };
            }
            else
            {
                return new TheoryData<UIntPtr, Half>() {
                    { UIntPtr.Zero, Half.NegativeZero },
                    { new UIntPtr(1), Half.One },
                    { new UIntPtr(127), SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new UIntPtr(32767), (Half)Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), Half.PositiveInfinity },
                    { new UIntPtr(Int32.MaxValue), Half.PositiveInfinity },
                    { new UIntPtr(UInt32.MaxValue), Half.PositiveInfinity },
                    { new UIntPtr(Int64.MaxValue), Half.PositiveInfinity },
                    { new UIntPtr(UInt64.MaxValue), Half.PositiveInfinity },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUIntPtrToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
