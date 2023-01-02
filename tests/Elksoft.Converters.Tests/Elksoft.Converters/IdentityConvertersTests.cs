using System;
using Elksoft.Converters.StandardConverters;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters
{
    public class IdentityConvertersTests
    {
        private static void Verify_GetIdentityConverter_ReturnsIdentityConverter<TIn>()
        {
            var instance = new IdentityConverters();

            instance.GetIdentityConverter(typeof(TIn)).Should().BeOfType<IdentityConverter<TIn>>();
        }

        [Theory]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(Byte?))]
        [InlineData(typeof(String))]
        [InlineData(typeof(IConvertible))]
        public static void GetIdentityConverter_ReturnsIdentityConverter(Type inType)
        {
            Utilities.Invoke(inType, Verify_GetIdentityConverter_ReturnsIdentityConverter<Int32>);
        }

        [Fact]
        public static void GetIdentityConverter_ThrowsArgumentNullException()
        {
            var instance = new IdentityConverters();

            instance.Invoking(x => x.GetIdentityConverter(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("inType");
        }
    }
}