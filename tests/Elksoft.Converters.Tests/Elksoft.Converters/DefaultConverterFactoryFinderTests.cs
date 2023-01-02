using System;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters
{
    public class DefaultConverterFinderTests
    {
        private readonly Mock<IPrimitiveOrEnumOrUserTypeOrNullableConverters> m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters;
        private readonly Mock<IIdentityConverters> m_mockIdentityConverters;
        private readonly Mock<IVariantConverters> m_mockFromObjectConverters;
        private readonly DefaultConverterFinder m_instance;

        public DefaultConverterFinderTests()
        {
            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters = new Mock<IPrimitiveOrEnumOrUserTypeOrNullableConverters>(MockBehavior.Strict);
            m_mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);
            m_mockFromObjectConverters = new Mock<IVariantConverters>(MockBehavior.Strict);

            m_instance = new DefaultConverterFinder(
                m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters.Object,
                m_mockIdentityConverters.Object,
                m_mockFromObjectConverters.Object);
        }

        private void VerifyAll()
        {
            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters.VerifyAll();
            m_mockIdentityConverters.VerifyAll();
            m_mockFromObjectConverters.VerifyAll();
        }

        [Fact]
        public void Ctor_ThrowsArgumentNullException()
        {
            new Func<Object>(() => new DefaultConverterFinder(
                null,
                m_mockIdentityConverters.Object,
                m_mockFromObjectConverters.Object))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("primitiveOrEnumOrUserTypeOrNullableConverters");

            new Func<Object>(() => new DefaultConverterFinder(
                m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters.Object,
                null,
                m_mockFromObjectConverters.Object))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("identityConverters");

            new Func<Object>(() => new DefaultConverterFinder(
                m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters.Object,
                m_mockIdentityConverters.Object,
                null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("variantConverters");

            VerifyAll();
        }

        #region InType is null or OutType is null
        [Fact]
        public void FindConverter_InTypeIsNullOrOutTypeIsNull_ThrowsArgumentNullException()
        {
            var mockType = new Mock<Type>(MockBehavior.Strict);

            m_instance.Invoking(x => x.FindConverter(null, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("inType");

            m_instance.Invoking(x => x.FindConverter(null, mockType.Object))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("inType");

            m_instance.Invoking(x => x.FindConverter(mockType.Object, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("outType");

            mockType.VerifyAll();
            VerifyAll();
        }
        #endregion

        #region InType and OutType are the same
        [Theory]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(Byte?))]
        [InlineData(typeof(Boxed<Byte>))]
        [InlineData(typeof(Object))]
        public void FindConverter_InTypeAndOutTypeAreTheSame_ReturnsExpected(Type inType)
        {
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);

            m_mockIdentityConverters
                .Setup(x => x.GetIdentityConverter(inType))
                .Returns(mockFactory.Object);

            for (var i = 0; i < 5; i++)
            {
                m_instance.FindConverter(inType, inType)
                    .Should().BeSameAs(mockFactory.Object);
            }

            m_mockIdentityConverters
                .Verify(x => x.GetIdentityConverter(inType), Times.Once);

            mockFactory.VerifyAll();
            VerifyAll();
        }

        private void Verify_GenericFindConverter_InTypeAndOutTypeAreTheSame_ReturnsExpected<TIn>()
        {
            var mockFactory = new Mock<Converter<TIn, TIn>>(MockBehavior.Strict);

            m_mockIdentityConverters
                .Setup(x => x.GetIdentityConverter(typeof(TIn)))
                .Returns(mockFactory.Object);

            for (var i = 0; i < 5; i++)
            {
                m_instance.FindConverter<TIn, TIn>()
                    .Should().BeSameAs(mockFactory.Object);
            }

            m_mockIdentityConverters
                .Verify(x => x.GetIdentityConverter(typeof(TIn)), Times.Once);

            mockFactory.VerifyAll();
            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(Byte?))]
        [InlineData(typeof(Boxed<Byte>))]
        [InlineData(typeof(Object))]
        public void GenericFindConverter_InTypeAndOutTypeAreTheSame_ReturnsExpected(Type inType)
        {
            Utilities.Invoke(inType, Verify_GenericFindConverter_InTypeAndOutTypeAreTheSame_ReturnsExpected<Int32>);
        }
        #endregion

        #region In Type is Object, Out Type is not Object
        [Theory]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(Byte?))]
        [InlineData(typeof(Boxed<Byte>))]
        public void FindConverter_InTypeIsObject_OutTypeIsNotObject_ReturnsExpected(Type outType)
        {
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);

            m_mockFromObjectConverters
                .Setup(x => x.GetVariantConverter(outType, m_instance))
                .Returns(mockFactory.Object);

            for (var i = 0; i < 5; i++)
            {
                m_instance.FindConverter(typeof(Object), outType)
                    .Should().BeSameAs(mockFactory.Object);
            }

            m_mockFromObjectConverters
                .Verify(x => x.GetVariantConverter(outType, m_instance), Times.Once);

            mockFactory.VerifyAll();
            VerifyAll();
        }

        private void Verify_GenericFindConverter_InTypeIsObject_OutTypeIsNotObject_ReturnsExpected<TOut>()
        {
            var mockFactory = new Mock<Converter<Object, TOut>>(MockBehavior.Strict);

            m_mockFromObjectConverters
                .Setup(x => x.GetVariantConverter(typeof(TOut), m_instance))
                .Returns(mockFactory.Object);

            for (var i = 0; i < 5; i++)
            {
                m_instance.FindConverter(typeof(Object), typeof(TOut))
                    .Should().BeSameAs(mockFactory.Object);
            }

            m_mockFromObjectConverters
                .Verify(x => x.GetVariantConverter(typeof(TOut), m_instance), Times.Once);

            mockFactory.VerifyAll();
            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(Byte?))]
        [InlineData(typeof(Boxed<Byte>))]
        public void GenericFindConverter_InTypeIsObject_OutTypeIsNotObject_ReturnsExpected(Type inType)
        {
            Utilities.Invoke(inType, Verify_GenericFindConverter_InTypeIsObject_OutTypeIsNotObject_ReturnsExpected<Int32>);
        }
        #endregion

        #region InnerFactoryReturnsNull
        [Theory]
        [InlineData(typeof(Decimal), typeof(DateTime))]
        [InlineData(typeof(String), typeof(Object))]
        [InlineData(typeof(Boolean), typeof(Decimal))]
        public void FindConverter_InnerFactoryReturnsNull_ReturnsNull(Type inType, Type outType)
        {
            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters
                .Setup(x => x.FindConverter(inType, outType))
                .Returns((Converter)null);

            for (var i = 0; i < 5; i++)
            {
                m_instance.FindConverter(inType, outType).Should().BeNull();
            }

            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters
                .Verify(x => x.FindConverter(inType, outType), Times.Once);

            VerifyAll();
        }

        private void Verify_GenericFindConverter_InnerFactoryReturnsNull_ReturnsNull<TIn, TOut>()
        {
            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters
                .Setup(x => x.FindConverter(typeof(TIn), typeof(TOut)))
                .Returns((Converter)null);

            for (var i = 0; i < 5; i++)
            {
                m_instance.FindConverter<TIn, TOut>().Should().BeNull();
            }

            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters
                .Verify(x => x.FindConverter(typeof(TIn), typeof(TOut)), Times.Once);

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Decimal), typeof(DateTime))]
        [InlineData(typeof(String), typeof(Object))]
        [InlineData(typeof(Boolean), typeof(Decimal))]
        public void GenericFindConverter_InnerFactoryReturnsNull_ReturnsNull(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_GenericFindConverter_InnerFactoryReturnsNull_ReturnsNull<Int32, Int32>);
        }
        #endregion

        #region InnerFactoryReturnsNotNull
        [Theory]
        [InlineData(typeof(Decimal), typeof(DateTime))]
        [InlineData(typeof(String), typeof(Object))]
        [InlineData(typeof(Boolean), typeof(Decimal))]
        public void FindConverter_InnerFactoryReturnsNotNull_ReturnsExpected(Type inType, Type outType)
        {
            var mockConverter = new Mock<Converter>(MockBehavior.Strict);

            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters
                .Setup(x => x.FindConverter(inType, outType))
                .Returns(mockConverter.Object);

            for (var i = 0; i < 5; i++)
            {
                m_instance.FindConverter(inType, outType).Should().BeSameAs(mockConverter.Object);
            }

            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters
                .Verify(x => x.FindConverter(inType, outType), Times.Once);

            mockConverter.VerifyAll();
            VerifyAll();
        }

        private void Verify_GenericFindConverter_InnerFactoryReturnsNotNull_ReturnsExpected<TIn, TOut>()
        {
            var mockConverter = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);

            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters
                .Setup(x => x.FindConverter(typeof(TIn), typeof(TOut)))
                .Returns(mockConverter.Object);

            for (var i = 0; i < 5; i++)
            {
                m_instance.FindConverter<TIn, TOut>().Should().BeSameAs(mockConverter.Object);
            }

            m_mockPrimitiveOrEnumOrUserTypeOrNullableConverters
                .Verify(x => x.FindConverter(typeof(TIn), typeof(TOut)), Times.Once);

            mockConverter.VerifyAll();
            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Decimal), typeof(DateTime))]
        [InlineData(typeof(String), typeof(Object))]
        [InlineData(typeof(Boolean), typeof(Decimal))]
        public void GenericFindConverter_InnerFactoryReturnsNotNull_ReturnsExpected(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_GenericFindConverter_InnerFactoryReturnsNotNull_ReturnsExpected<Int32, Int32>);
        }
        #endregion

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public static void Create_ReturnsExpected(Boolean useCheckedConverters)
        {
            new Func<Object>(() => DefaultConverterFinder.Create(useCheckedConverters))
                .Should().NotThrow();
        }
    }
}