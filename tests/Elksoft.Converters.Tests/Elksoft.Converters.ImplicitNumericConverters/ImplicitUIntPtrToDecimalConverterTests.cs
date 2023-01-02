#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitUIntPtrToDecimalConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitUIntPtrToDecimalConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitUIntPtrToDecimalConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<UIntPtr, Decimal> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UIntPtr, Decimal>() {
                    { UIntPtr.Zero, Decimal.Zero },
                    { new UIntPtr(1), Decimal.One },
                    { new UIntPtr(127), SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new UIntPtr(32767), Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), UInt16.MaxValue },
                    { new UIntPtr(Int32.MaxValue), Int32.MaxValue },
                    { new UIntPtr(UInt32.MaxValue), UInt32.MaxValue },
                };
            }
            else
            {
                return new TheoryData<UIntPtr, Decimal>() {
                    { UIntPtr.Zero, Decimal.Zero },
                    { new UIntPtr(1), Decimal.One },
                    { new UIntPtr(127), SByte.MaxValue },
                    { new UIntPtr(Byte.MaxValue), Byte.MaxValue },
                    { new UIntPtr(32767), Int16.MaxValue },
                    { new UIntPtr(UInt16.MaxValue), UInt16.MaxValue },
                    { new UIntPtr(Int32.MaxValue), Int32.MaxValue },
                    { new UIntPtr(UInt32.MaxValue), UInt32.MaxValue },
                    { new UIntPtr(Int64.MaxValue), Int64.MaxValue },
                    { new UIntPtr(UInt64.MaxValue), UInt64.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UIntPtr input, Decimal output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitUIntPtrToDecimalConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
