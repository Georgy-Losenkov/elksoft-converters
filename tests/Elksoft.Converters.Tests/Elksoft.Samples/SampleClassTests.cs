#nullable enable
using System;
using FluentAssertions;
using Xunit;

namespace Elksoft.Samples
{
    public class SampleClassTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void Ctor_AssignsValue(Int16 value)
        {
            var sample = new SampleClass(value);
            sample.Value.Should().Be(value);
        }

        [Fact]
        public static void ImplicitOperatorFromInt16ToSampleClass_ReturnsNull()
        {
            SampleClass? sample = default(Int16?);
            sample.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ImplicitOperatorFromInt16ToSampleClass_ReturnsExpected(Int16 value)
        {
            SampleClass? sample = value;
            sample.Should().NotBeNull();
            sample!.Value.Should().Be(value);
        }

        [Fact]
        public static void ImplicitOperatorFromSampleClassToInt16_ReturnsNull()
        {
            Int16? sample = default(SampleClass);
            sample.Should().BeNull();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ImplicitOperatorFromSampleClassToInt16_ReturnsExpected(Int16 value)
        {
            Int16? sample = new SampleClass(value);
            sample.Should().NotBeNull();
            sample.GetValueOrDefault().Should().Be(value);
        }

        [Theory]
        [InlineData(Int32.MinValue)]
        [InlineData(Int32.MaxValue)]
        public static void ExplicitOperatorFromInt32ToSampleClass_ThrowsOverflowException(Int32 value)
        {
            new Action(() => _ = (SampleClass)value)
                .Should().ThrowExactly<OverflowException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ExplicitOperatorFromInt32ToSampleClass_ReturnsExpected(Int16 value)
        {
            var sample = (SampleClass)(Int32)value;
            sample.Should().NotBeNull();
            sample!.Value.Should().Be(value);
        }

        [Fact]
        public static void ImplicitOperatorFromSampleClassToInt32_ThrowsArgumentNullException()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            new Action(() => { Int32? sample = default(SampleClass); })
                .Should().ThrowExactly<ArgumentNullException>();
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-255)]
        [InlineData(255)]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ImplicitOperatorFromSampleClassToInt32_ReturnsExpected(Int16 value)
        {
            Int32? sample = new SampleClass(value);
            sample.Should().NotBeNull();
            sample.GetValueOrDefault().Should().Be(value);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(Byte.MinValue)]
        [InlineData(Byte.MaxValue)]
        public static void ImplicitOperatorFromByteToSampleClass_ReturnsExpected(Byte value)
        {
            var sample = (SampleClass)value;
            sample.Should().NotBeNull();
            sample!.Value.Should().Be(value);
        }

        [Fact]
        public static void ExplicitOperatorFromSampleClassToByte_ThrowsArgumentNullException()
        {
#pragma warning disable CS8604 // Possible null reference argument.
            new Action(() => _ = (Byte)default(SampleClass))
                .Should().ThrowExactly<ArgumentNullException>();
#pragma warning restore CS8604 // Possible null reference argument.
        }

        [Theory]
        [InlineData(Int16.MinValue)]
        [InlineData(Int16.MaxValue)]
        public static void ExplicitOperatorFromSampleClassToByte_ThrowsOverflowException(Int16 value)
        {
            new Action(() => _ = (Byte)new SampleClass(value))
                .Should().ThrowExactly<OverflowException>();
        }

        [Theory]
        [InlineData(0)]
        [InlineData(Byte.MinValue)]
        [InlineData(Byte.MaxValue)]
        public static void ExplicitOperatorFromSampleClassToByte_ReturnsExpected(Byte value)
        {
            var sample = (Byte)new SampleClass(value);
            sample.Should().Be(value);
        }
    }
}