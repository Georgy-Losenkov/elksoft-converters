using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters
{
    public class PrimitiveOrEnumConvertersTests
    {
        private readonly Mock<IPrimitiveConverters> m_mockPrimitiveConverters;
        private readonly Mock<IEnumConverters> m_mockEnumConverters;
        private readonly Mock<IIdentityConverters> m_mockIdentityConverters;
        private readonly PrimitiveOrEnumConverters m_instance;

        public PrimitiveOrEnumConvertersTests()
        {
            m_mockPrimitiveConverters = new Mock<IPrimitiveConverters>(MockBehavior.Strict);
            m_mockEnumConverters = new Mock<IEnumConverters>(MockBehavior.Strict);
            m_mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);
            m_instance = new PrimitiveOrEnumConverters(
                m_mockPrimitiveConverters.Object,
                m_mockEnumConverters.Object,
                m_mockIdentityConverters.Object);
        }

        private void VerifyAll()
        {
            m_mockPrimitiveConverters.VerifyAll();
            m_mockEnumConverters.VerifyAll();
            m_mockIdentityConverters.VerifyAll();
        }

        [Fact]
        public void Ctor_ThrowsArgumentNullException()
        {
            new Func<Object>(() => new PrimitiveOrEnumConverters(
                null,
                m_mockEnumConverters.Object,
                m_mockIdentityConverters.Object))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("primitiveConverters");

            new Func<Object>(() => new PrimitiveOrEnumConverters(
                m_mockPrimitiveConverters.Object,
                null,
                m_mockIdentityConverters.Object))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("enumConverters");

            new Func<Object>(() => new PrimitiveOrEnumConverters(
                m_mockPrimitiveConverters.Object,
                m_mockEnumConverters.Object,
                null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("identityConverters");

            VerifyAll();
        }

        [Fact]
        public void IsSupported_ThrowsArgumentNullException()
        {
            m_instance.Invoking(x => x.IsSupported(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");

            VerifyAll();
        }

        [Fact]
        public void IsSupported_ReturnsFalse()
        {
            var mockType = new Mock<Type>(MockBehavior.Strict);

            m_mockEnumConverters.Setup(x => x.IsSupported(mockType.Object)).Returns(false);
            m_mockPrimitiveConverters.Setup(x => x.IsSupported(mockType.Object)).Returns(false);

            m_instance.IsSupported(mockType.Object).Should().BeFalse();

            VerifyAll();
            mockType.VerifyAll();
        }

        [Fact]
        public void IsSupported_ArgumentIsEnum_ReturnsTrue()
        {
            var mockType = new Mock<Type>(MockBehavior.Strict);

            m_mockEnumConverters.Setup(x => x.IsSupported(mockType.Object)).Returns(true);
            m_mockPrimitiveConverters.Setup(x => x.IsSupported(mockType.Object)).Returns(false);

            m_instance.IsSupported(mockType.Object).Should().BeTrue();

            VerifyAll();
            mockType.VerifyAll();
        }

        [Fact]
        public void IsSupported_ArgumentIsPrimitive_ReturnsTrue()
        {
            var mockType = new Mock<Type>(MockBehavior.Strict);

            m_mockPrimitiveConverters.Setup(x => x.IsSupported(mockType.Object)).Returns(true);

            m_instance.IsSupported(mockType.Object).Should().BeTrue();

            VerifyAll();
            mockType.VerifyAll();
        }

        [Fact]
        public void FindConverterChain_ThrowsArgumentNullException()
        {
            var mockType = new Mock<Type>(MockBehavior.Strict);

            m_instance.Invoking(x => x.FindConverterChain(null, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("inType");

            m_instance.Invoking(x => x.FindConverterChain(null, mockType.Object))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("inType");

            m_instance.Invoking(x => x.FindConverterChain(mockType.Object, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("outType");

            VerifyAll();
            mockType.VerifyAll();
        }

        private static IEnumerable<Type> GetPrimitiveTypes()
        {
            yield return typeof(Boolean);
            yield return typeof(Byte);
            yield return typeof(Byte[]);
            yield return typeof(Char);
            yield return typeof(DateOnly);
            yield return typeof(DateTime);
            yield return typeof(DateTimeOffset);
            yield return typeof(Decimal);
            yield return typeof(Double);
            yield return typeof(Guid);
            yield return typeof(Int16);
            yield return typeof(Int32);
            yield return typeof(Int64);
            yield return typeof(SByte);
            yield return typeof(Single);
            yield return typeof(String);
            yield return typeof(TimeOnly);
            yield return typeof(TimeSpan);
            yield return typeof(UInt16);
            yield return typeof(UInt32);
            yield return typeof(UInt64);
        }

        private static IEnumerable<Type> GetEnumTypes()
        {
            yield return typeof(DateTimeKind);
            yield return typeof(FirstUInt32BasedEnum);
            yield return typeof(FirstUInt64BasedEnum);
            yield return typeof(SecondUInt32BasedEnum);
            yield return typeof(SecondUInt64BasedEnum);
        }

        public static IEnumerable<Object[]> FindConverterChain_SameSupportedTypes_ReturnsIdentityConverter_Data()
        {
            return GetPrimitiveTypes().Concat(GetEnumTypes()).Select(t => new Object[] { t });
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_SameSupportedTypes_ReturnsIdentityConverter_Data))]
        public void FindConverterChain_SameSupportedTypes_ReturnsIdentityConverter(Type inType)
        {
            var mockConverter = new Mock<Converter>(MockBehavior.Strict);

            m_mockIdentityConverters
                .Setup(x => x.GetIdentityConverter(inType))
                .Returns(mockConverter.Object);

            m_instance.FindConverterChain(inType, inType)
                .Should().BeEquivalentTo(new[] { mockConverter.Object });

            mockConverter.VerifyAll();
            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum), typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(FirstUInt64BasedEnum), typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_EnumToEnum_SameBase_ReturnsExpected(Type inType, Type outType)
        {
            inType.IsEnum.Should().BeTrue();
            outType.IsEnum.Should().BeTrue();
            inType.GetEnumUnderlyingType().Should().BeSameAs(outType.GetEnumUnderlyingType());

            var mockInTypeToBase = new Mock<Converter>(MockBehavior.Strict);
            var mockBaseToOutType = new Mock<Converter>(MockBehavior.Strict);

            mockInTypeToBase
                .Setup(x => x.OutType)
                .Returns(inType.GetEnumUnderlyingType());

            mockBaseToOutType
                .Setup(x => x.InType)
                .Returns(outType.GetEnumUnderlyingType());

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetEnumToUnderlying(inType))
                .Returns(mockInTypeToBase.Object);

            m_mockEnumConverters
                .Setup(x => x.GetUnderlyingToEnum(outType))
                .Returns(mockBaseToOutType.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockInTypeToBase.Object, mockBaseToOutType.Object });

            mockInTypeToBase.VerifyAll();
            mockBaseToOutType.VerifyAll();

            VerifyAll();
        }


        [Theory]
        [InlineData(typeof(FirstUInt64BasedEnum), typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(FirstUInt32BasedEnum), typeof(SecondUInt64BasedEnum))]
        [InlineData(typeof(FirstUInt64BasedEnum), typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(SecondUInt32BasedEnum), typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_EnumToEnum_IncompatibleBases_ReturnsEmpty(Type inType, Type outType)
        {
            inType.IsEnum.Should().BeTrue();
            outType.IsEnum.Should().BeTrue();
            inType.GetEnumUnderlyingType().Should().NotBeSameAs(outType.GetEnumUnderlyingType());

            var mockInTypeToBase = new Mock<Converter>(MockBehavior.Strict);
            var mockBaseToOutType = new Mock<Converter>(MockBehavior.Strict);

            mockInTypeToBase
                .Setup(x => x.OutType)
                .Returns(inType.GetEnumUnderlyingType());

            mockBaseToOutType
                .Setup(x => x.InType)
                .Returns(outType.GetEnumUnderlyingType());

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetEnumToUnderlying(inType))
                .Returns(mockInTypeToBase.Object);

            m_mockEnumConverters
                .Setup(x => x.GetUnderlyingToEnum(outType))
                .Returns(mockBaseToOutType.Object);

            m_mockPrimitiveConverters
                .Setup(x => x.FindConverter(inType.GetEnumUnderlyingType(), outType.GetEnumUnderlyingType()))
                .Returns((Converter)null);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEmpty();

            mockInTypeToBase.VerifyAll();
            mockBaseToOutType.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt64BasedEnum), typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(FirstUInt32BasedEnum), typeof(SecondUInt64BasedEnum))]
        [InlineData(typeof(FirstUInt64BasedEnum), typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(SecondUInt32BasedEnum), typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_EnumToEnum_CompatibleBases_ReturnsExpected(Type inType, Type outType)
        {
            inType.IsEnum.Should().BeTrue();
            outType.IsEnum.Should().BeTrue();
            inType.GetEnumUnderlyingType().Should().NotBeSameAs(outType.GetEnumUnderlyingType());

            var mockInTypeToBase = new Mock<Converter>(MockBehavior.Strict);
            var mockBaseToOutType = new Mock<Converter>(MockBehavior.Strict);
            var mockBaseToBase = new Mock<Converter>(MockBehavior.Strict);

            mockInTypeToBase
                .Setup(x => x.OutType)
                .Returns(inType.GetEnumUnderlyingType());

            mockBaseToOutType
                .Setup(x => x.InType)
                .Returns(outType.GetEnumUnderlyingType());

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetEnumToUnderlying(inType))
                .Returns(mockInTypeToBase.Object);

            m_mockEnumConverters
                .Setup(x => x.GetUnderlyingToEnum(outType))
                .Returns(mockBaseToOutType.Object);

            m_mockPrimitiveConverters
                .Setup(x => x.FindConverter(inType.GetEnumUnderlyingType(), outType.GetEnumUnderlyingType()))
                .Returns(mockBaseToBase.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockInTypeToBase.Object, mockBaseToBase.Object, mockBaseToOutType.Object });

            mockInTypeToBase.VerifyAll();
            mockBaseToOutType.VerifyAll();
            mockBaseToBase.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_EnumToString_ReturnsExpected(Type inType)
        {
            var outType = typeof(String);

            var mockInTypeToString = new Mock<Converter>(MockBehavior.Strict);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetEnumToString(inType))
                .Returns(mockInTypeToString.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockInTypeToString.Object });

            mockInTypeToString.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_EnumToBase_ReturnsExpected(Type inType)
        {
            inType.IsEnum.Should().BeTrue();
            var outType = inType.GetEnumUnderlyingType();

            var mockInTypeToBase = new Mock<Converter>(MockBehavior.Strict);

            mockInTypeToBase
                .Setup(x => x.OutType)
                .Returns(outType);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetEnumToUnderlying(inType))
                .Returns(mockInTypeToBase.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockInTypeToBase.Object });

            mockInTypeToBase.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum), typeof(Int32))]
        [InlineData(typeof(FirstUInt64BasedEnum), typeof(Int32))]
        [InlineData(typeof(SecondUInt32BasedEnum), typeof(Int32))]
        [InlineData(typeof(SecondUInt64BasedEnum), typeof(Int32))]
        public void FindConverterChain_EnumToPrimitive_ReturnsExpected(Type inType, Type outType)
        {
            var mockInTypeToBase = new Mock<Converter>(MockBehavior.Strict);
            var mockBaseToOutType = new Mock<Converter>(MockBehavior.Strict);

            mockInTypeToBase
                .Setup(x => x.OutType)
                .Returns(inType.GetEnumUnderlyingType());

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetEnumToUnderlying(inType))
                .Returns(mockInTypeToBase.Object);

            m_mockPrimitiveConverters
                .Setup(x => x.FindConverter(inType.GetEnumUnderlyingType(), outType))
                .Returns(mockBaseToOutType.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockInTypeToBase.Object, mockBaseToOutType.Object });

            mockInTypeToBase.VerifyAll();
            mockBaseToOutType.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum), typeof(Int32))]
        [InlineData(typeof(FirstUInt64BasedEnum), typeof(Int32))]
        [InlineData(typeof(SecondUInt32BasedEnum), typeof(Int32))]
        [InlineData(typeof(SecondUInt64BasedEnum), typeof(Int32))]
        public void FindConverterChain_EnumToPrimitive_ReturnsEmpty(Type inType, Type outType)
        {
            var mockInTypeToBase = new Mock<Converter>(MockBehavior.Strict);

            mockInTypeToBase
                .Setup(x => x.OutType)
                .Returns(inType.GetEnumUnderlyingType());

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetEnumToUnderlying(inType))
                .Returns(mockInTypeToBase.Object);

            m_mockPrimitiveConverters
                .Setup(x => x.FindConverter(inType.GetEnumUnderlyingType(), outType))
                .Returns((Converter)null);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEmpty();

            mockInTypeToBase.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_StringToEnum_ReturnsExpected(Type outType)
        {
            var inType = typeof(String);

            var mockStringToOutType = new Mock<Converter>(MockBehavior.Strict);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(false);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetStringToEnum(outType))
                .Returns(mockStringToOutType.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockStringToOutType.Object });

            mockStringToOutType.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_BaseToEnum_ReturnsExpected(Type outType)
        {
            outType.IsEnum.Should().BeTrue();
            var inType = outType.GetEnumUnderlyingType();

            var mockBaseToOutType = new Mock<Converter>(MockBehavior.Strict);

            mockBaseToOutType
                .Setup(x => x.InType)
                .Returns(outType);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(false);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockPrimitiveConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetUnderlyingToEnum(outType))
                .Returns(mockBaseToOutType.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockBaseToOutType.Object });

            mockBaseToOutType.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Int32), typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_PrimitiveToEnum_ReturnsExpected(Type inType, Type outType)
        {
            var mockInTypeToBase = new Mock<Converter>(MockBehavior.Strict);
            var mockBaseToOutType = new Mock<Converter>(MockBehavior.Strict);

            mockBaseToOutType
                .Setup(x => x.InType)
                .Returns(outType.GetEnumUnderlyingType());

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetUnderlyingToEnum(outType))
                .Returns(mockBaseToOutType.Object);

            m_mockPrimitiveConverters
                .Setup(x => x.FindConverter(inType, outType.GetEnumUnderlyingType()))
                .Returns(mockInTypeToBase.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockInTypeToBase.Object, mockBaseToOutType.Object });

            mockInTypeToBase.VerifyAll();
            mockBaseToOutType.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Int32), typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_PrimitiveToEnum_ReturnsEmpty(Type inType, Type outType)
        {
            var mockBaseToOutType = new Mock<Converter>(MockBehavior.Strict);

            mockBaseToOutType
                .Setup(x => x.InType)
                .Returns(outType.GetEnumUnderlyingType());

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.GetUnderlyingToEnum(outType))
                .Returns(mockBaseToOutType.Object);

            m_mockPrimitiveConverters
                .Setup(x => x.FindConverter(inType, outType.GetEnumUnderlyingType()))
                .Returns((Converter)null);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEmpty();

            mockBaseToOutType.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum), typeof(Int32))]
        [InlineData(typeof(FirstUInt64BasedEnum), typeof(Int32))]
        [InlineData(typeof(SecondUInt32BasedEnum), typeof(Int32))]
        [InlineData(typeof(SecondUInt64BasedEnum), typeof(Int32))]
        public void FindConverterChain_EnumToOther_ReturnsEmpty(Type inType, Type outType)
        {
            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(true);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(false);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEmpty();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Int32), typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_OtherToEnum_ReturnsEmpty(Type inType, Type outType)
        {
            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(false);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(true);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEmpty();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Int32), typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_PrimitiveToPrimitive_ReturnsExpected(Type inType, Type outType)
        {
            var mockInTypeToOutType = new Mock<Converter>(MockBehavior.Strict);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(false);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.FindConverter(inType, outType))
                .Returns(mockInTypeToOutType.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockInTypeToOutType.Object });

            mockInTypeToOutType.VerifyAll();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Int32), typeof(FirstUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(FirstUInt64BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt32BasedEnum))]
        [InlineData(typeof(Int32), typeof(SecondUInt64BasedEnum))]
        public void FindConverterChain_PrimitiveToPrimitive_ReturnsEmpty(Type inType, Type outType)
        {
            m_mockEnumConverters
                .Setup(x => x.IsSupported(inType))
                .Returns(false);

            m_mockEnumConverters
                .Setup(x => x.IsSupported(outType))
                .Returns(false);

            m_mockPrimitiveConverters
                .Setup(x => x.FindConverter(inType, outType))
                .Returns((Converter)null);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEmpty();

            VerifyAll();
        }
    }
}