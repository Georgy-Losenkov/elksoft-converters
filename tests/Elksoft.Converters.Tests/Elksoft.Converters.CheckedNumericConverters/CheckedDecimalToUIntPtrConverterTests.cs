#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedDecimalToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedDecimalToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedDecimalToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Decimal, UIntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Decimal, UIntPtr>() {
                    { Decimal.Zero, UIntPtr.Zero },
                    { Decimal.One, new UIntPtr(1) },
                    { SByte.MaxValue, new UIntPtr(127) },
                    { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new UIntPtr(32767) },
                    { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                    { UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<Decimal, UIntPtr>() {
                    { Decimal.Zero, UIntPtr.Zero },
                    { Decimal.One, new UIntPtr(1) },
                    { SByte.MaxValue, new UIntPtr(127) },
                    { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new UIntPtr(32767) },
                    { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                    { UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
                    { Int64.MaxValue, new UIntPtr(Int64.MaxValue) },
                    { UInt64.MaxValue, new UIntPtr(UInt64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Decimal input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDecimalToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Decimal> Convert_ThrowsException_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Decimal>() {
                    { Decimal.MinValue },
                    { Int64.MinValue },
                    { Int32.MinValue },
                    { Int16.MinValue },
                    { SByte.MinValue },
                    { -1m },
                    { Int64.MaxValue },
                    { UInt64.MaxValue },
                    { Decimal.MaxValue },
                };
            }
            else
            {
                return new TheoryData<Decimal>() {
                    { Decimal.MinValue },
                    { Int64.MinValue },
                    { Int32.MinValue },
                    { Int16.MinValue },
                    { SByte.MinValue },
                    { -1m },
                    { Decimal.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Decimal input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDecimalToUIntPtrConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
