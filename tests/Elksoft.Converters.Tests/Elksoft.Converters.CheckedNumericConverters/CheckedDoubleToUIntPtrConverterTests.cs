#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.CheckedNumericConverters
{
    public class CheckedDoubleToUIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new CheckedDoubleToUIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new CheckedDoubleToUIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, UIntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Double, UIntPtr>() {
                    { 0.0, UIntPtr.Zero },
                    { Double.Epsilon, UIntPtr.Zero },
                    { Single.Epsilon, UIntPtr.Zero },
                    { (Double)Half.Epsilon, UIntPtr.Zero },
                    { 1.0, new UIntPtr(1) },
                    { (Double)Half.E, UIntPtr.Parse("2") },
                    { Single.E, UIntPtr.Parse("2") },
                    { Double.E, UIntPtr.Parse("2") },
                    { SByte.MaxValue, new UIntPtr(127) },
                    { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new UIntPtr(32767) },
                    { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                    { UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
                    { (Double)Half.MaxValue, UIntPtr.Parse("65504") },
                };
            }
            else
            {
                return new TheoryData<Double, UIntPtr>() {
                    { 0.0, UIntPtr.Zero },
                    { Double.Epsilon, UIntPtr.Zero },
                    { Single.Epsilon, UIntPtr.Zero },
                    { (Double)Half.Epsilon, UIntPtr.Zero },
                    { 1.0, new UIntPtr(1) },
                    { (Double)Half.E, UIntPtr.Parse("2") },
                    { Single.E, UIntPtr.Parse("2") },
                    { Double.E, UIntPtr.Parse("2") },
                    { SByte.MaxValue, new UIntPtr(127) },
                    { Byte.MaxValue, new UIntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new UIntPtr(32767) },
                    { UInt16.MaxValue, new UIntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new UIntPtr(Int32.MaxValue) },
                    { UInt32.MaxValue, new UIntPtr(UInt32.MaxValue) },
                    { Int64.MaxValue, UIntPtr.Parse("9223372036854775808") },
                    { (Double)Half.MaxValue, UIntPtr.Parse("65504") },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, UIntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDoubleToUIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }

        public static TheoryData<Double> Convert_ThrowsException_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Double>() {
                    { Double.NaN },
                    { Double.NegativeInfinity },
                    { Double.MinValue },
                    { (Double)Int128.MinValue },
                    { Int64.MinValue },
                    { Int32.MinValue },
                    { Int16.MinValue },
                    { SByte.MinValue },
                    { -1.0 },
                    { Int64.MaxValue },
                    { UInt64.MaxValue },
                    { (Double)Int128.MaxValue },
                    { (Double)UInt128.MaxValue },
                    { Single.MaxValue },
                    { Double.MaxValue },
                    { Double.PositiveInfinity },
                };
            }
            else
            {
                return new TheoryData<Double>() {
                    { Double.NaN },
                    { Double.NegativeInfinity },
                    { Double.MinValue },
                    { (Double)Int128.MinValue },
                    { Int64.MinValue },
                    { Int32.MinValue },
                    { Int16.MinValue },
                    { SByte.MinValue },
                    { -1.0 },
                    { UInt64.MaxValue },
                    { (Double)Int128.MaxValue },
                    { (Double)UInt128.MaxValue },
                    { Single.MaxValue },
                    { Double.MaxValue },
                    { Double.PositiveInfinity },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ThrowsException_Data))]
        public static void Convert_ThrowsException(Double input)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new CheckedDoubleToUIntPtrConverter();
            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();
            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();

            mockProvider.VerifyAll();
        }
    }
}
#endif
