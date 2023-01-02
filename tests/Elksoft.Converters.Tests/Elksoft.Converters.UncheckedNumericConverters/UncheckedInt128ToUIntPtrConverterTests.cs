#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedInt128ToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedInt128ToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedInt128ToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int128, UIntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Int128, UIntPtr>() {
                    { Int128.Zero, UIntPtr.Zero },
                    { Int128.One, new UIntPtr(1) },
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
                return new TheoryData<Int128, UIntPtr>() {
                    { Int128.Zero, UIntPtr.Zero },
                    { Int128.One, new UIntPtr(1) },
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
        public static void Convert_ReturnsExpected(Int128 input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedInt128ToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
