using System;
using FluentAssertions;
using Xunit;

namespace Elksoft.Samples
{
    public class BoxedTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(127)]
        [InlineData(Byte.MaxValue)]
        public static void Byte_Ctor_AssignsValue(Byte value)
        {
            var instance = new Boxed<Byte>(value);
            instance.Value.Should().Be(value);
        }

        [Theory]
        [InlineData(Int32.MinValue)]
        [InlineData(0)]
        [InlineData(Int32.MaxValue)]
        public static void Int32_Ctor_AssignsValue(Int32 value)
        {
            var instance = new Boxed<Int32>(value);
            instance.Value.Should().Be(value);
        }
    }
}