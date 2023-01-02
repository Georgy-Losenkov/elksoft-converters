using System;
using System.Collections.Generic;
using System.Linq;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters
{
    public class PrimitiveOrEnumOrUserTypeOrNullableConvertersTests
    {
        private readonly Mock<IPrimitiveOrEnumOrUserTypeConverters> m_mockPrimitiveOrEnumOrUserTypeConverters;
        private readonly Mock<IIdentityConverters> m_mockIdentityConverters;
        private readonly Mock<ICombiner> m_mockCombiner;
        private readonly Mock<INullableWrapper> m_mockNullableWrapper;

        private readonly PrimitiveOrEnumOrUserTypeOrNullableConverters m_instance;

        public PrimitiveOrEnumOrUserTypeOrNullableConvertersTests()
        {
            m_mockPrimitiveOrEnumOrUserTypeConverters = new Mock<IPrimitiveOrEnumOrUserTypeConverters>(MockBehavior.Strict);
            m_mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);
            m_mockCombiner = new Mock<ICombiner>(MockBehavior.Strict);
            m_mockNullableWrapper = new Mock<INullableWrapper>(MockBehavior.Strict);

            m_instance = new PrimitiveOrEnumOrUserTypeOrNullableConverters(
                m_mockPrimitiveOrEnumOrUserTypeConverters.Object,
                m_mockIdentityConverters.Object,
                m_mockCombiner.Object,
                m_mockNullableWrapper.Object);
        }

        private void VerifyAll()
        {
            m_mockPrimitiveOrEnumOrUserTypeConverters.VerifyAll();
            m_mockIdentityConverters.VerifyAll();
            m_mockCombiner.VerifyAll();
            m_mockNullableWrapper.VerifyAll();
        }

        [Fact]
        public void Ctor_NullArguments_ThrowsArgumentNullException()
        {
            new Func<PrimitiveOrEnumOrUserTypeOrNullableConverters>(() => new PrimitiveOrEnumOrUserTypeOrNullableConverters(
                null,
                m_mockIdentityConverters.Object,
                m_mockCombiner.Object,
                m_mockNullableWrapper.Object))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("primitiveOrEnumOrUserTypeConverters");

            new Func<PrimitiveOrEnumOrUserTypeOrNullableConverters>(() => new PrimitiveOrEnumOrUserTypeOrNullableConverters(
                m_mockPrimitiveOrEnumOrUserTypeConverters.Object,
                null,
                m_mockCombiner.Object,
                m_mockNullableWrapper.Object))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("identityConverters");

            new Func<PrimitiveOrEnumOrUserTypeOrNullableConverters>(() => new PrimitiveOrEnumOrUserTypeOrNullableConverters(
                m_mockPrimitiveOrEnumOrUserTypeConverters.Object,
                m_mockIdentityConverters.Object,
                null,
                m_mockNullableWrapper.Object))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("combiner");

            new Func<PrimitiveOrEnumOrUserTypeOrNullableConverters>(() => new PrimitiveOrEnumOrUserTypeOrNullableConverters(
                m_mockPrimitiveOrEnumOrUserTypeConverters.Object,
                m_mockIdentityConverters.Object,
                m_mockCombiner.Object,
                null))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("nullableWrapper");

            VerifyAll();
        }

        [Theory]
        [InlineData(null, null, "inType")]
        [InlineData(null, typeof(Boolean), "inType")]
        [InlineData(typeof(Boolean), null, "outType")]
        public void FindConverter_InTypeOrOutTypeIsNull_ThrowsArgumentNullException(Type inType, Type outType, String paramName)
        {
            m_instance.Invoking(x => x.FindConverter(inType, outType))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be(paramName);

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Byte))]
        [InlineData(typeof(Byte?))]
        [InlineData(typeof(Boxed<Boolean>))]
        public void FindConverter_InTypeIsEqualToOutType_ReturnsExpected(Type inType)
        {
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);

            m_mockIdentityConverters
                .Setup(x => x.GetIdentityConverter(inType))
                .Returns(mockFactory.Object);

            m_instance.FindConverter(inType, inType)
                .Should().BeSameAs(mockFactory.Object);

            mockFactory.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> SamplePairs()
        {
            yield return new Object[] { typeof(Boxed<Byte>), typeof(Boxed<Int32>) };
            yield return new Object[] { typeof(Boxed<Byte>), typeof(Structed<Int32>) };
            yield return new Object[] { typeof(Boxed<Byte>), typeof(Structed<Int32>?) };
            yield return new Object[] { typeof(Structed<Byte>), typeof(Boxed<Int32>) };
            yield return new Object[] { typeof(Structed<Byte>), typeof(Structed<Int32>) };
            yield return new Object[] { typeof(Structed<Byte>), typeof(Structed<Int32>?) };
            yield return new Object[] { typeof(Structed<Byte>?), typeof(Boxed<Int32>) };
            yield return new Object[] { typeof(Structed<Byte>?), typeof(Structed<Int32>) };
            yield return new Object[] { typeof(Structed<Byte>?), typeof(Structed<Int32>?) };
        }

        [Theory]
        [MemberData(nameof(SamplePairs))]
        public void FindConverter_ChainIsNull_ReturnsNull(Type inType, Type outType)
        {
            m_mockPrimitiveOrEnumOrUserTypeConverters
                .Setup(x => x.FindConverterChain(inType.MakeNotNullable(), outType.MakeNotNullable()))
                .Returns((IEnumerable<Converter>)null);

            m_instance.FindConverter(inType, outType)
                .Should().BeNull();

            VerifyAll();
        }

        [Theory]
        [MemberData(nameof(SamplePairs))]
        public void FindConverter_ChainIsEmpty_ReturnsNull(Type inType, Type outType)
        {
            m_mockPrimitiveOrEnumOrUserTypeConverters
                .Setup(x => x.FindConverterChain(inType.MakeNotNullable(), outType.MakeNotNullable()))
                .Returns(Enumerable.Empty<Converter>());

            m_instance.FindConverter(inType, outType)
                .Should().BeNull();

            VerifyAll();
        }

        [Theory]
        [MemberData(nameof(SamplePairs))]
        public void FindConverter_ChainConsistsOfOneElement_ReturnsExpected(Type inType, Type outType)
        {
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);
            var mockResult = new Mock<Converter>(MockBehavior.Strict);

            m_mockPrimitiveOrEnumOrUserTypeConverters
                .Setup(x => x.FindConverterChain(inType.MakeNotNullable(), outType.MakeNotNullable()))
                .Returns(new[] { mockFactory.Object });

            m_mockNullableWrapper
                .Setup(x => x.Wrap(inType, mockFactory.Object, outType))
                .Returns(mockResult.Object);

            m_instance.FindConverter(inType, outType)
                .Should().BeSameAs(mockResult.Object);

            mockFactory.VerifyAll();
            mockResult.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverter_ChainConsistsOfTwoCombinableElements_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Byte>), typeof(Structed<Byte>), typeof(Structed<Byte>?) })
            {
                var midType = typeof(Structed<Int16>);

                foreach (var outType in new[] { typeof(Boxed<Int32>), typeof(Structed<Int32>), typeof(Structed<Int32>?) })
                {
                    yield return new Object[] { inType, midType, outType };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverter_ChainConsistsOfTwoCombinableElements_ReturnsExpected_Data))]
        public void FindConverter_ChainConsistsOfTwoCombinableElements_ReturnsExpected(Type inType, Type midType, Type outType)
        {
            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory2 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory12 = new Mock<Converter>(MockBehavior.Strict);
            var mockResult = new Mock<Converter>(MockBehavior.Strict);

            mockFactory1.Setup(x => x.OutType).Returns(midType);
            mockFactory2.Setup(x => x.InType).Returns(midType);

            m_mockPrimitiveOrEnumOrUserTypeConverters
                .Setup(x => x.FindConverterChain(inType.MakeNotNullable(), outType.MakeNotNullable()))
                .Returns(new[] { mockFactory1.Object, mockFactory2.Object });

            m_mockCombiner
                .Setup(x => x.Combine(mockFactory1.Object, mockFactory2.Object))
                .Returns(mockFactory12.Object);

            m_mockNullableWrapper
                .Setup(x => x.Wrap(inType, mockFactory12.Object, outType))
                .Returns(mockResult.Object);

            m_instance.FindConverter(inType, outType)
                .Should().BeSameAs(mockResult.Object);

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
            mockFactory12.VerifyAll();
            mockResult.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverter_ChainConsistsOfTwoNonCombinableElements_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Byte>), typeof(Structed<Byte>), typeof(Structed<Byte>?) })
            {
                foreach (var midTypes in new[] {
                    (typeof(Boxed<Int16>), typeof(Boxed<Int16>)),
                    (typeof(Structed<Int16>?), typeof(Structed<Int16>?)),
                    (typeof(Structed<Int16>?), typeof(Structed<Int16>)),
                    (typeof(Structed<Int16>), typeof(Structed<Int16>?)) })
                {
                    foreach (var outType in new[] { typeof(Boxed<Int32>), typeof(Structed<Int32>), typeof(Structed<Int32>?) })
                    {
                        yield return new Object[] { inType, midTypes.Item1, midTypes.Item2, outType };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverter_ChainConsistsOfTwoNonCombinableElements_ReturnsExpected_Data))]
        public void FindConverter_ChainConsistsOfTwoNonCombinableElements_ReturnsExpected(Type inType, Type midType1, Type midType2, Type outType)
        {
            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory2 = new Mock<Converter>(MockBehavior.Strict);
            var mockWrappedFactory1 = new Mock<Converter>(MockBehavior.Strict);
            var mockWrappedFactory2 = new Mock<Converter>(MockBehavior.Strict);
            var mockResult = new Mock<Converter>(MockBehavior.Strict);

            mockFactory1.Setup(x => x.OutType).Returns(midType1);
            mockFactory2.Setup(x => x.InType).Returns(midType2);

            m_mockPrimitiveOrEnumOrUserTypeConverters
                .Setup(x => x.FindConverterChain(inType.MakeNotNullable(), outType.MakeNotNullable()))
                .Returns(new[] { mockFactory1.Object, mockFactory2.Object });

            m_mockNullableWrapper
                .Setup(x => x.Wrap(inType, mockFactory1.Object, midType1.MakeNullable()))
                .Returns(mockWrappedFactory1.Object);

            m_mockNullableWrapper
                .Setup(x => x.Wrap(midType2.MakeNullable(), mockFactory2.Object, outType))
                .Returns(mockWrappedFactory2.Object);

            m_mockCombiner
                .Setup(x => x.Combine(mockWrappedFactory1.Object, mockWrappedFactory2.Object))
                .Returns(mockResult.Object);

            m_instance.FindConverter(inType, outType)
                .Should().BeSameAs(mockResult.Object);

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
            mockWrappedFactory1.VerifyAll();
            mockWrappedFactory2.VerifyAll();
            mockResult.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverter_ChainConsistsOfThreeCombinableElements_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Byte>), typeof(Structed<Byte>), typeof(Structed<Byte>?) })
            {
                var midType1 = typeof(Structed<Int16>);
                var midType2 = typeof(Structed<Int32>);

                foreach (var outType in new[] { typeof(Boxed<Int64>), typeof(Structed<Int64>), typeof(Structed<Int64>?) })
                {
                    yield return new Object[] { inType, midType1, midType2, outType };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverter_ChainConsistsOfThreeCombinableElements_ReturnsExpected_Data))]
        public void FindConverter_ChainConsistsOfThreeCombinableElements_ReturnsExpected(Type inType, Type midType1, Type midType2, Type outType)
        {
            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory2 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory3 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory12 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory123 = new Mock<Converter>(MockBehavior.Strict);
            var mockResult = new Mock<Converter>(MockBehavior.Strict);

            mockFactory1.Setup(x => x.OutType).Returns(midType1);
            mockFactory2.Setup(x => x.InType).Returns(midType1);
            mockFactory3.Setup(x => x.InType).Returns(midType2);
            mockFactory12.Setup(x => x.OutType).Returns(midType2);

            m_mockPrimitiveOrEnumOrUserTypeConverters
                .Setup(x => x.FindConverterChain(inType.MakeNotNullable(), outType.MakeNotNullable()))
                .Returns(new[] { mockFactory1.Object, mockFactory2.Object, mockFactory3.Object });

            m_mockCombiner
                .Setup(x => x.Combine(mockFactory1.Object, mockFactory2.Object))
                .Returns(mockFactory12.Object);

            m_mockCombiner
                .Setup(x => x.Combine(mockFactory12.Object, mockFactory3.Object))
                .Returns(mockFactory123.Object);

            m_mockNullableWrapper
                .Setup(x => x.Wrap(inType, mockFactory123.Object, outType))
                .Returns(mockResult.Object);

            m_instance.FindConverter(inType, outType)
                .Should().BeSameAs(mockResult.Object);

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
            mockFactory3.VerifyAll();
            mockFactory12.VerifyAll();
            mockFactory123.VerifyAll();
            mockResult.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverter_ChainConsistsOfThreeNonCombinableElements_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Byte>), typeof(Structed<Byte>), typeof(Structed<Byte>?) })
            {
                foreach (var (midType1, midType2) in new[] {
                    (typeof(Boxed<Int16>), typeof(Boxed<Int16>)),
                    (typeof(Structed<Int16>?), typeof(Structed<Int16>?)),
                    (typeof(Structed<Int16>?), typeof(Structed<Int16>)),
                    (typeof(Structed<Int16>), typeof(Structed<Int16>?)) })
                {
                    foreach (var (midType3, midType4) in new[] {
                        (typeof(Boxed<Int32>), typeof(Boxed<Int32>)),
                        (typeof(Structed<Int32>?), typeof(Structed<Int32>?)),
                        (typeof(Structed<Int32>?), typeof(Structed<Int32>)),
                        (typeof(Structed<Int32>), typeof(Structed<Int32>?)) })
                    {
                        foreach (var outType in new[] { typeof(Boxed<Int64>), typeof(Structed<Int64>), typeof(Structed<Int64>?) })
                        {
                            yield return new Object[] { inType, midType1, midType2, midType3, midType4, outType };
                        }
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverter_ChainConsistsOfThreeNonCombinableElements_ReturnsExpected_Data))]
        public void FindConverter_ChainConsistsOfThreeNonCombinableElements_ReturnsExpected(Type inType, Type midType1, Type midType2, Type midType3, Type midType4, Type outType)
        {
            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory2 = new Mock<Converter>(MockBehavior.Strict);
            var mockFactory3 = new Mock<Converter>(MockBehavior.Strict);
            var mockWrappedFactory1 = new Mock<Converter>(MockBehavior.Strict);
            var mockWrappedFactory2 = new Mock<Converter>(MockBehavior.Strict);
            var mockWrappedFactory3 = new Mock<Converter>(MockBehavior.Strict);
            var mockWrappedFactory12 = new Mock<Converter>(MockBehavior.Strict);
            var mockWrappedFactory123 = new Mock<Converter>(MockBehavior.Strict);

            mockFactory1.Setup(x => x.OutType).Returns(midType1);
            mockFactory2.Setup(x => x.InType).Returns(midType2);
            mockFactory2.Setup(x => x.OutType).Returns(midType3);
            mockFactory3.Setup(x => x.InType).Returns(midType4);

            m_mockPrimitiveOrEnumOrUserTypeConverters
                .Setup(x => x.FindConverterChain(inType.MakeNotNullable(), outType.MakeNotNullable()))
                .Returns(new[] { mockFactory1.Object, mockFactory2.Object, mockFactory3.Object });

            m_mockNullableWrapper
                .Setup(x => x.Wrap(inType, mockFactory1.Object, midType1.MakeNullable()))
                .Returns(mockWrappedFactory1.Object);

            m_mockNullableWrapper
                .Setup(x => x.Wrap(midType2.MakeNullable(), mockFactory2.Object, midType3.MakeNullable()))
                .Returns(mockWrappedFactory2.Object);

            m_mockNullableWrapper
                .Setup(x => x.Wrap(midType4.MakeNullable(), mockFactory3.Object, outType))
                .Returns(mockWrappedFactory3.Object);

            m_mockCombiner
                .Setup(x => x.Combine(mockWrappedFactory1.Object, mockWrappedFactory2.Object))
                .Returns(mockWrappedFactory12.Object);

            m_mockCombiner
                .Setup(x => x.Combine(mockWrappedFactory12.Object, mockWrappedFactory3.Object))
                .Returns(mockWrappedFactory123.Object);

            m_instance.FindConverter(inType, outType)
                .Should().BeSameAs(mockWrappedFactory123.Object);

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
            mockFactory3.VerifyAll();
            mockWrappedFactory1.VerifyAll();
            mockWrappedFactory2.VerifyAll();
            mockWrappedFactory3.VerifyAll();
            mockWrappedFactory12.VerifyAll();
            mockWrappedFactory123.VerifyAll();
            VerifyAll();
        }
    }
}