using System;
using Elksoft.Converters.StandardConverters;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters
{
    public class CastToBaseConvertersTests
    {
        private static void Verify_FindCastToBaseConverter_ReturnsDowncastConverter<TIn, TOut>()
            where TIn : TOut
        {
            var instance = new UpCastConverters();

            instance.FindUpCastConverter(typeof(TIn), typeof(TOut))
                .Should().BeOfType<UpCastConverter<TIn, TOut>>();
        }

        [Theory]
        [InlineData(typeof(Byte), typeof(Object))]
        [InlineData(typeof(Byte), typeof(IFormattable))]
        [InlineData(typeof(Byte), typeof(IConvertible))]
        [InlineData(typeof(Byte?), typeof(Object))]
        [InlineData(typeof(String), typeof(Object))]
        [InlineData(typeof(String), typeof(IConvertible))]
        [InlineData(typeof(DateTimeKind), typeof(Enum))]
        public static void FindCastToBaseConverter_ReturnsDowncastConverter(Type typeIn, Type typeOut)
        {
            Utilities.Invoke(typeIn, typeOut, Verify_FindCastToBaseConverter_ReturnsDowncastConverter<Int32, Object>);
        }

        [Theory]
        [InlineData(typeof(Byte), typeof(Boolean))]
        [InlineData(typeof(IConvertible), typeof(IFormattable))]
        [InlineData(typeof(Object), typeof(IConvertible))]
        [InlineData(typeof(Byte?), typeof(Byte))]
        [InlineData(typeof(Enum), typeof(DateTimeKind))]
        public static void FindCastToBaseConverter_ReturnsNull(Type inType, Type outType)
        {
            var instance = new UpCastConverters();

            instance.FindUpCastConverter(inType, outType).Should().BeNull();
        }

        [Fact]
        public static void FindCastToBaseConverter_ThrowsArgumentNullException()
        {
            var instance = new UpCastConverters();

            instance.Invoking(x => x.FindUpCastConverter(null, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("inType");

            instance.Invoking(x => x.FindUpCastConverter(null, typeof(Boolean)))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("inType");

            instance.Invoking(x => x.FindUpCastConverter(typeof(Boolean), null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("outType");
        }
    }
}