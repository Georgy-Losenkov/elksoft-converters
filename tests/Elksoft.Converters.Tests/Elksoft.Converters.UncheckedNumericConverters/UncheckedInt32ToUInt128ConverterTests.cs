﻿#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedInt32ToUInt128ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedInt32ToUInt128Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedInt32ToUInt128Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Int32, UInt128> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Int32, UInt128>() {
                { 0, UInt128.Zero },
                { 1, UInt128.One },
                { SByte.MaxValue, (UInt128)SByte.MaxValue },
                { Byte.MaxValue, (UInt128)Byte.MaxValue },
                { Int16.MaxValue, (UInt128)Int16.MaxValue },
                { UInt16.MaxValue, (UInt128)UInt16.MaxValue },
                { Int32.MaxValue, (UInt128)Int32.MaxValue },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Int32 input, UInt128 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedInt32ToUInt128Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
