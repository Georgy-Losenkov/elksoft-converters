﻿#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDecimalToIntPtrConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDecimalToIntPtrConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDecimalToIntPtrConverter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Decimal, IntPtr> Convert_ReturnsExpected_Data()
        {
            if (IntPtr.Size == 4)
            {
                return new TheoryData<Decimal, IntPtr>() {
                    { Int32.MinValue, new IntPtr(Int32.MinValue) },
                    { Int16.MinValue, new IntPtr(Int16.MinValue) },
                    { SByte.MinValue, new IntPtr(SByte.MinValue) },
                    { -1m, new IntPtr(-1) },
                    { Decimal.Zero, IntPtr.Zero },
                    { Decimal.One, new IntPtr(1) },
                    { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                };
            }
            else
            {
                return new TheoryData<Decimal, IntPtr>() {
                    { Int64.MinValue, new IntPtr(Int64.MinValue) },
                    { Int32.MinValue, new IntPtr(Int32.MinValue) },
                    { Int16.MinValue, new IntPtr(Int16.MinValue) },
                    { SByte.MinValue, new IntPtr(SByte.MinValue) },
                    { -1m, new IntPtr(-1) },
                    { Decimal.Zero, IntPtr.Zero },
                    { Decimal.One, new IntPtr(1) },
                    { SByte.MaxValue, new IntPtr(SByte.MaxValue) },
                    { Byte.MaxValue, new IntPtr(Byte.MaxValue) },
                    { Int16.MaxValue, new IntPtr(Int16.MaxValue) },
                    { UInt16.MaxValue, new IntPtr(UInt16.MaxValue) },
                    { Int32.MaxValue, new IntPtr(Int32.MaxValue) },
                    { UInt32.MaxValue, new IntPtr(UInt32.MaxValue) },
                    { Int64.MaxValue, new IntPtr(Int64.MaxValue) },
                };
            }
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Decimal input, IntPtr output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDecimalToIntPtrConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
