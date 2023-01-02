#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, IntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Double, IntPtr>() {
                    { Int32.MinValue, new IntPtr(Int32.MinValue) },
                    { Int16.MinValue, new IntPtr(Int16.MinValue) },
                    { SByte.MinValue, new IntPtr(SByte.MinValue) },
                    { -1.0, new IntPtr(-1) },
                    { Double.NegativeZero, IntPtr.Zero },
                    { 0.0, IntPtr.Zero },
                    { Double.Epsilon, IntPtr.Zero },
                    { Single.Epsilon, IntPtr.Zero },
                    { (Double)Half.Epsilon, IntPtr.Zero },
                    { 1.0, new IntPtr(1) },
                    { (Double)Half.E, IntPtr.Parse("2") },
                    { Single.E, IntPtr.Parse("2") },
                    { Double.E, IntPtr.Parse("2") },
                    { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                    { (Double)Half.MaxValue, IntPtr.Parse("65504") },
                };
            }
            else
            {
                return new TheoryData<Double, IntPtr>() {
                    { Int64.MinValue, new IntPtr(Int64.MinValue) },
                    { Int32.MinValue, new IntPtr(Int32.MinValue) },
                    { Int16.MinValue, new IntPtr(Int16.MinValue) },
                    { SByte.MinValue, new IntPtr(SByte.MinValue) },
                    { -1.0, new IntPtr(-1) },
                    { Double.NegativeZero, IntPtr.Zero },
                    { 0.0, IntPtr.Zero },
                    { Double.Epsilon, IntPtr.Zero },
                    { Single.Epsilon, IntPtr.Zero },
                    { (Double)Half.Epsilon, IntPtr.Zero },
                    { 1.0, new IntPtr(1) },
                    { (Double)Half.E, IntPtr.Parse("2") },
                    { Single.E, IntPtr.Parse("2") },
                    { Double.E, IntPtr.Parse("2") },
                    { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                    { UInt32.MaxValue, new IntPtr(UInt32.MaxValue) },
                    { (Double)Half.MaxValue, IntPtr.Parse("65504") },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
