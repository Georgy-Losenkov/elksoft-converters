﻿using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.UncheckedNumericConverters
{
    public class UncheckedDoubleToInt64ConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new UncheckedDoubleToInt64Converter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsTrue()
        {
            var converter = new UncheckedDoubleToInt64Converter();
            converter.IsExplicit.Should().BeTrue();
        }

        public static TheoryData<Double, Int64> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Double, Int64>() {
                { Int64.MinValue, Int64.MinValue },
                { Int32.MinValue, Int32.MinValue },
                { Int16.MinValue, Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0, -1L },
                { 0.0, 0L },
                { Double.Epsilon, 0L },
                { Single.Epsilon, 0L },
#if NET7_0_OR_GREATER
                { (Double)Half.Epsilon, 0L },
#endif
                { 1.0, 1L },
#if NET7_0_OR_GREATER
                { (Double)Half.E, Int64.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Single.E, Int64.Parse("2") },
#endif
#if NET7_0_OR_GREATER
                { Double.E, Int64.Parse("2") },
#endif
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, Int16.MaxValue },
                { UInt16.MaxValue, UInt16.MaxValue },
                { Int32.MaxValue, Int32.MaxValue },
                { UInt32.MaxValue, UInt32.MaxValue },
#if NET7_0_OR_GREATER
                { (Double)Half.MaxValue, Int64.Parse("65504") },
#endif
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Double input, Int64 output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new UncheckedDoubleToInt64Converter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
