#nullable enable
using System;
using FluentAssertions;
using Xunit;

namespace Elksoft.Samples
{
    public class SampleStructTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void Ctor_AssignsValue(Int16 value)
        {
            var sample = new SampleStruct(value);
            sample.Value.Should().Be(value);
        }

        [Fact]
        public static void ImplicitOperatorFromInt16ToSampleStruct_ReturnsNull()
        {
            SampleStruct? sample = default(Int16?);
            sample.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ImplicitOperatorFromInt16ToSampleStruct_ReturnsExpected(Int16 value)
        {
            SampleStruct? sample = value;
            sample.HasValue.Should().BeTrue();
            sample.GetValueOrDefault().Value.Should().Be(value);
        }

        [Fact]
        public static void ImplicitOperatorFromSampleStructToInt16_ReturnsNull()
        {
            Int16? sample = default(SampleStruct?);
            sample.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ImplicitOperatorFromSampleStructToInt16_ReturnsExpected(Int16 value)
        {
            Int16? sample = new SampleStruct(value);
            sample.HasValue.Should().BeTrue();
            sample.GetValueOrDefault().Should().Be(value);
        }

        [Fact]
        public static void ExplicitOperatorFromInt32ToSampleStruct_ThrowsArgumentNullException()
        {
            new Action(() => _ = (SampleStruct)default(Int32?))
                .Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData(Int32.MinValue)]
        [InlineData(Int32.MaxValue)]
        public static void ExplicitOperatorFromInt32ToSampleStruct_ThrowsOverflowException(Int32 value)
        {
            new Action(() => _ = (SampleStruct)value)
                .Should().ThrowExactly<OverflowException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ExplicitOperatorFromInt32ToSampleStruct_ReturnsExpected(Int16 value)
        {
            var sample = (SampleStruct)(Int32)value;
            sample.Should().NotBeNull();
            sample!.Value.Should().Be(value);
        }

        [Fact]
        public static void ImplicitOperatorFromSampleStructToInt32_ThrowsArgumentNullException()
        {
            new Action(() => { Int32 sample = default(SampleStruct?); })
                .Should().ThrowExactly<ArgumentNullException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ImplicitOperatorFromSampleStructToInt32_ReturnsExpected(Int16 value)
        {
            Int32? sample = new SampleStruct(value);
            sample.Should().NotBeNull();
            sample.GetValueOrDefault().Should().Be(value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(Byte.MinValue)]
        [InlineData(Byte.MaxValue)]
        public static void ImplicitOperatorFromByteToSampleStruct_ReturnsExpected(Byte value)
        {
            var sample = (SampleStruct)value;
            sample.Should().NotBeNull();
            sample!.Value.Should().Be(value);
        }

        [Theory]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ExplicitOperatorFromSampleStructToByte_ThrowsOverflowException(Int16 value)
        {
            new Action(() => _ = (Byte)new SampleStruct(value))
                .Should().ThrowExactly<OverflowException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(Byte.MinValue)]
        [InlineData(Byte.MaxValue)]
        public static void ExplicitOperatorFromSampleStructToByte_ReturnsExpected(Byte value)
        {
            var sample = (Byte)new SampleStruct(value);
            sample.Should().Be(value);
        }
    }
}