using System;
using Elksoft.Converters.StandardConverters;
using Elksoft.Samples;
using FluentAssertions;
using Xunit;

namespace Elksoft.Converters
{
    public class EnumConvertersTests
    {
        [Fact]
        public static void IsSupported_ThrowsArgumentNullException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.IsSupported(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("type");
        }

        [Theory]
        [InlineData(typeof(Int32), false)]
        [InlineData(typeof(FirstUInt64BasedEnum), true)]
        public static void IsSupported_RetunsExpected(Type type, Boolean result)
        {
            var converters = new EnumConverters();

            converters.IsSupported(type).Should().Be(result);
        }

        [Fact]
        public static void GetUnderlyingToEnum_ThrowsArgumentNullException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.GetUnderlyingToEnum(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("enumType");
        }

        [Fact]
        public static void GetUnderlyingToEnum_ThrowsArgumentException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.GetUnderlyingToEnum(typeof(Byte)))
                .Should().ThrowExactly<ArgumentException>()
                .WithParameterName("enumType");
        }

        [Fact]
        public static void GetUnderlyingToEnum_ReturnsExpected()
        {
            var converters = new EnumConverters();

            var converter = converters.GetUnderlyingToEnum(typeof(FirstUInt64BasedEnum))
                .Should().BeOfType<CultureInvariantDelegateConverter<UInt64, FirstUInt64BasedEnum>>()
                .Subject;

            converter.AcceptsNull.Should().BeFalse();
            converter.IsExplicit.Should().BeFalse();
            converter.Func.Should().BeSameAs(EnumDelegates<FirstUInt64BasedEnum>.UnderlyingToEnumDelegate);
        }

        [Fact]
        public static void GetEnumToUnderlying_ThrowsArgumentNullException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.GetEnumToUnderlying(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("enumType");
        }

        [Fact]
        public static void GetEnumToUnderlying_ThrowsArgumentException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.GetEnumToUnderlying(typeof(Byte)))
                .Should().ThrowExactly<ArgumentException>()
                .WithParameterName("enumType");
        }

        [Fact]
        public static void GetEnumToUnderlying_ReturnsExpected()
        {
            var converters = new EnumConverters();

            var converter = converters.GetEnumToUnderlying(typeof(FirstUInt64BasedEnum))
                .Should().BeOfType<CultureInvariantDelegateConverter<FirstUInt64BasedEnum, UInt64>>()
                .Subject;

            converter.AcceptsNull.Should().BeFalse();
            converter.IsExplicit.Should().BeFalse();
            converter.Func.Should().BeSameAs(EnumDelegates<FirstUInt64BasedEnum>.EnumToUnderlyingDelegate);
        }

        [Fact]
        public static void GetEnumToString_ThrowsArgumentNullException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.GetEnumToString(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("enumType");
        }

        [Fact]
        public static void GetEnumToString_ThrowsArgumentException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.GetEnumToString(typeof(Byte)))
                .Should().ThrowExactly<ArgumentException>()
                .WithParameterName("enumType");
        }

        [Fact]
        public static void GetEnumToString_ReturnsExpected()
        {
            var converters = new EnumConverters();

            var converter = converters.GetEnumToString(typeof(FirstUInt64BasedEnum))
                .Should().BeOfType<EnumToStringConverter<FirstUInt64BasedEnum>>()
                .Subject;

            converter.AcceptsNull.Should().BeFalse();
            converter.IsExplicit.Should().BeTrue();
        }

        [Fact]
        public static void GetStringToEnum_ThrowsArgumentNullException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.GetStringToEnum(null))
                .Should().ThrowExactly<ArgumentNullException>()
                .WithParameterName("enumType");
        }

        [Fact]
        public static void GetStringToEnum_ThrowsArgumentException()
        {
            var converters = new EnumConverters();

            converters.Invoking(x => x.GetStringToEnum(typeof(Byte)))
                .Should().ThrowExactly<ArgumentException>()
                .WithParameterName("enumType");
        }

        [Fact]
        public static void GetStringToEnum_ReturnsExpected()
        {
            var converters = new EnumConverters();

            var converter = converters.GetStringToEnum(typeof(FirstUInt64BasedEnum))
                .Should().BeOfType<StringToEnumConverter<FirstUInt64BasedEnum>>()
                .Subject;

            converter.AcceptsNull.Should().BeFalse();
            converter.IsExplicit.Should().BeTrue();
        }
    }
}