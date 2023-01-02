#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUInt128ToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUInt128ToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUInt128ToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt128, UIntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UInt128, UIntPtr>() {
                    { UInt128.Zero, UIntPtr.Zero },
                    { UInt128.One, new UIntPtr(1) },
                    { (UInt128)SByte.MaxValue, new UIntPtr(127) },
                    { (UInt128)Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { (UInt128)Int16.MaxValue, new UIntPtr(32767) },
                    { (UInt128)UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { (UInt128)Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                    { (UInt128)UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<UInt128, UIntPtr>() {
                    { UInt128.Zero, UIntPtr.Zero },
                    { UInt128.One, new UIntPtr(1) },
                    { (UInt128)SByte.MaxValue, new UIntPtr(127) },
                    { (UInt128)Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { (UInt128)Int16.MaxValue, new UIntPtr(32767) },
                    { (UInt128)UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { (UInt128)Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                    { (UInt128)UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
                    { (UInt128)Int64.MaxValue, new UIntPtr(Int64.MaxValue) },
                    { (UInt128)UInt64.MaxValue, new UIntPtr(UInt64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt128 input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt128ToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<UInt128> Convert_ThrowsException_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UInt128>() {
                    { (UInt128)Int64.MaxValue },
                    { (UInt128)UInt64.MaxValue },
                    { (UInt128)Int128.MaxValue },
                    { UInt128.MaxValue },
                };
            }
            else
            {
                return new TheoryData<UInt128>() {
                    { (UInt128)Int128.MaxValue },
                    { UInt128.MaxValue },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(UInt128 input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt128ToUIntPtrConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
