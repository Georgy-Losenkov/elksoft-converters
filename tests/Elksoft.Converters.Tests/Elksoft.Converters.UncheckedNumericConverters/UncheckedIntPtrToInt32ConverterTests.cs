﻿#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedIntPtrToInt32ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedIntPtrToInt32Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedIntPtrToInt32Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<IntPtr, Int32> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<IntPtr, Int32>() {
                { new IntPtr(Int32.MinValue), Int32.MinValue },
                { new IntPtr(Int16.MinValue), Int16.MinValue },
                { new IntPtr(SByte.MinValue), SByte.MinValue },
                { new IntPtr(-1), -1 },
                { IntPtr.Zero, 0 },
                { new IntPtr(1), 1 },
                { new IntPtr(SByte.MaxValue), SByte.MaxValue },
                { new IntPtr(Byte.MaxValue), Byte.MaxValue },
                { new IntPtr(Int16.MaxValue), Int16.MaxValue },
                { new IntPtr(UInt16.MaxValue), UInt16.MaxValue },
                { new IntPtr(Int32.MaxValue), Int32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(IntPtr input, Int32 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedIntPtrToInt32Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
