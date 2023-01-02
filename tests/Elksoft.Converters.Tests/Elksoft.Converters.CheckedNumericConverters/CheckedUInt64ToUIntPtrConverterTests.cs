#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedUInt64ToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedUInt64ToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedUInt64ToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<UInt64, UIntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<UInt64, UIntPtr>() {
                    { 0UL, UIntPtr.Zero },
                    { 1UL, new UIntPtr(1) },
                    { (UInt64)SByte.MaxValue, new UIntPtr(127) },
                    { (UInt64)Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { (UInt64)Int16.MaxValue, new UIntPtr(32767) },
                    { (UInt64)UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { (UInt64)Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                    { (UInt64)UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<UInt64, UIntPtr>() {
                    { 0UL, UIntPtr.Zero },
                    { 1UL, new UIntPtr(1) },
                    { (UInt64)SByte.MaxValue, new UIntPtr(127) },
                    { (UInt64)Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { (UInt64)Int16.MaxValue, new UIntPtr(32767) },
                    { (UInt64)UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { (UInt64)Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                    { (UInt64)UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
                    { (UInt64)Int64.MaxValue, new UIntPtr(Int64.MaxValue) },
                    { UInt64.MaxValue, new UIntPtr(UInt64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(UInt64 input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedUInt64ToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        [Fact]
        public static void Convert_X86_ThrowsException()
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            if (IntPtr.Size == 4)
            {
                var converter = new CheckedUInt64ToUIntPtrConverter();
                converter.Invoking(x => x.Convert((UInt64)Int64.MaxValue, null)).Should().Throw<Exception>();
                converter.Invoking(x => x.Convert(UInt64.MaxValue, null)).Should().Throw<Exception>();
                converter.Invoking(x => x.Convert((UInt64)Int64.MaxValue, mockProvider.Object)).Should().Throw<Exception>();
                converter.Invoking(x => x.Convert(UInt64.MaxValue, mockProvider.Object)).Should().Throw<Exception>();
            }

            mockProvider.VerifyAll();
        }
    }
}
#endif
