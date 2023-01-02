#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDecimalToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDecimalToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDecimalToUIntPtrConverter();
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

            var converter = new UncheckedDecimalToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
