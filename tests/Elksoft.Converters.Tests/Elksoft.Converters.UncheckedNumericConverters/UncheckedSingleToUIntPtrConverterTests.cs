#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedSingleToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedSingleToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedSingleToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Single, UIntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Single, UIntPtr>() {
                    { 0.0f, UIntPtr.Zero },
                    { (Single)Half.Epsilon, UIntPtr.Zero },
                    { Single.Epsilon, UIntPtr.Zero },
                    { 1.0f, new UIntPtr(1) },
                    { (Single)Half.E, UIntPtr.Parse("2") },
                    { Single.E, UIntPtr.Parse("2") },
                    { SByte.MaxValue, new UIntPtr(127) },
                    { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new UIntPtr(32767) },
                    { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, UIntPtr.Parse("2147483648") },
                    { (Single)Half.MaxValue, UIntPtr.Parse("65504") },
                };
            }
            else
            {
                return new TheoryData<Single, UIntPtr>() {
                    { 0.0f, UIntPtr.Zero },
                    { (Single)Half.Epsilon, UIntPtr.Zero },
                    { Single.Epsilon, UIntPtr.Zero },
                    { 1.0f, new UIntPtr(1) },
                    { (Single)Half.E, UIntPtr.Parse("2") },
                    { Single.E, UIntPtr.Parse("2") },
                    { SByte.MaxValue, new UIntPtr(127) },
                    { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new UIntPtr(32767) },
                    { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, UIntPtr.Parse("2147483648") },
                    { UInt32.MaxValue, UIntPtr.Parse("4294967296") },
                    { Int64.MaxValue, UIntPtr.Parse("9223372036854775808") },
                    { (Single)Half.MaxValue, UIntPtr.Parse("65504") },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedSingleToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
