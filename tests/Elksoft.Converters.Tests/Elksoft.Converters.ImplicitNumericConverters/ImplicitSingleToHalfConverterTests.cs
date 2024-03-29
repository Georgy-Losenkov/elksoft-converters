﻿#if NET7_0_OR_GREATER
using System;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.ImplicitNumericConverters
{
    public class ImplicitSingleToHalfConverterTests
    {
        [Fact]
        public static void AcceptsNull_IsFalse()
        {
            var converter = new ImplicitSingleToHalfConverter();
            converter.AcceptsNull.Should().BeFalse();
        }

        [Fact]
        public static void IsExplicit_IsFalse()
        {
            var converter = new ImplicitSingleToHalfConverter();
            converter.IsExplicit.Should().BeFalse();
        }

        public static TheoryData<Single, Half> Convert_ReturnsExpected_Data()
        {
            return new TheoryData<Single, Half>() {
                { Single.NaN, Half.NaN },
                { Single.NegativeInfinity, Half.NegativeInfinity },
                { Single.MinValue, Half.NegativeInfinity },
                { (Single)Half.MinValue, Half.MinValue },
                { (Single)Int128.MinValue, Half.NegativeInfinity },
                { Int64.MinValue, Half.NegativeInfinity },
                { Int32.MinValue, Half.NegativeInfinity },
                { Int16.MinValue, (Half)Int16.MinValue },
                { SByte.MinValue, SByte.MinValue },
                { -1.0f, Half.NegativeOne },
                { 0.0f, Half.Zero },
                { (Single)Half.Epsilon, Half.Epsilon },
                { Single.Epsilon, Half.Zero },
                { 1.0f, Half.One },
                { (Single)Half.E, Half.E },
                { Single.E, Half.E },
                { SByte.MaxValue, SByte.MaxValue },
                { Byte.MaxValue, Byte.MaxValue },
                { Int16.MaxValue, (Half)Int16.MaxValue },
                { UInt16.MaxValue, Half.PositiveInfinity },
                { Int32.MaxValue, Half.PositiveInfinity },
                { UInt32.MaxValue, Half.PositiveInfinity },
                { Int64.MaxValue, Half.PositiveInfinity },
                { UInt64.MaxValue, Half.PositiveInfinity },
                { (Single)Int128.MaxValue, Half.PositiveInfinity },
                { (Single)Half.MaxValue, Half.MaxValue },
                { Single.MaxValue, Half.PositiveInfinity },
                { Single.PositiveInfinity, Half.PositiveInfinity },
            };
        }

        [Theory]
        [MemberData(nameof(Convert_ReturnsExpected_Data))]
        public static void Convert_ReturnsExpected(Single input, Half output)
        {
            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            var converter = new ImplicitSingleToHalfConverter();
            converter.Convert(input, null).Should().Be(output);
            converter.Convert(input, mockProvider.Object).Should().Be(output);

            mockProvider.VerifyAll();
        }
    }
}
#endif
