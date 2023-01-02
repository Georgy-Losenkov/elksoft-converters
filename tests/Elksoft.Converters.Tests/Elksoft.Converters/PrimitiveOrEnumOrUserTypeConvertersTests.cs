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
    public class PrimitiveOrEnumOrUserTypeConvertersTests
    {
        private readonly Mock<IPrimitiveOrEnumConverters> m_mockPrimitiveOrEnumConverters;
        private readonly Mock<IUserTypeConverters> m_mockUserTypeConverters;
        private readonly Mock<IIdentityConverters> m_mockIdentityConverters;
        private readonly Mock<IUpCastConverters> m_mockUpCastConverters;

        private readonly PrimitiveOrEnumOrUserTypeConverters m_instance;
        public PrimitiveOrEnumOrUserTypeConvertersTests()
        {
            m_mockPrimitiveOrEnumConverters = new Mock<IPrimitiveOrEnumConverters>(MockBehavior.Strict);
            m_mockUserTypeConverters = new Mock<IUserTypeConverters>(MockBehavior.Strict);
            m_mockIdentityConverters = new Mock<IIdentityConverters>(MockBehavior.Strict);
            m_mockUpCastConverters = new Mock<IUpCastConverters>(MockBehavior.Strict);

            m_instance = new PrimitiveOrEnumOrUserTypeConverters(
                m_mockPrimitiveOrEnumConverters.Object,
                m_mockUserTypeConverters.Object,
                m_mockIdentityConverters.Object,
                m_mockUpCastConverters.Object);

        }

        private void VerifyAll()
        {
            m_mockPrimitiveOrEnumConverters.VerifyAll();
            m_mockUserTypeConverters.VerifyAll();
            m_mockIdentityConverters.VerifyAll();
            m_mockUpCastConverters.VerifyAll();
        }

        [Fact]
        public void Ctor_NullArguments_ThrowsArgumentNullException()
        {
            new Func<Object>(() => new PrimitiveOrEnumOrUserTypeConverters(
                null,
                m_mockUserTypeConverters.Object,
                m_mockIdentityConverters.Object,
                m_mockUpCastConverters.Object))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("primitiveOrEnumConverters");

            new Func<Object>(() => new PrimitiveOrEnumOrUserTypeConverters(
                m_mockPrimitiveOrEnumConverters.Object,
                null,
                m_mockIdentityConverters.Object,
                m_mockUpCastConverters.Object))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("userTypeConverters");

            new Func<Object>(() => new PrimitiveOrEnumOrUserTypeConverters(
                m_mockPrimitiveOrEnumConverters.Object,
                m_mockUserTypeConverters.Object,
                null,
                m_mockUpCastConverters.Object))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("identityConverters");

            new Func<Object>(() => new PrimitiveOrEnumOrUserTypeConverters(
                m_mockPrimitiveOrEnumConverters.Object,
                m_mockUserTypeConverters.Object,
                m_mockIdentityConverters.Object,
                null))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be("upCastConverters");

            VerifyAll();
        }

        [Theory]
        [InlineData(null, null, "inType")]
        [InlineData(null, typeof(Boolean), "inType")]
        [InlineData(typeof(Boolean), null, "outType")]
        public void FindConverterChain_InTypeOrOutTypeIsNull_ThrowsArgumentNullException(Type inType, Type outType, String paramName)
        {
            m_instance.Invoking(x => x.FindConverterChain(inType, outType))
                .Should().ThrowExactly<ArgumentNullException>()
                .Which.ParamName.Should().Be(paramName);

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Boolean?), typeof(Boolean?))]
        [InlineData(typeof(Boolean?), typeof(Boolean))]
        [InlineData(typeof(Boolean), typeof(Boolean?))]
        public void FindConverterChain_InTypeOrOutTypeIsNullable_ReturnsEmpty(Type inType, Type outType)
        {
            m_instance.FindConverterChain(inType, outType)
                .Should().BeEmpty();

            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Boolean))]
        [InlineData(typeof(Byte))]
        public void FindConverterChain_InTypeEqualsToOutType_ReturnsExpected(Type inType)
        {
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);

            m_mockIdentityConverters
                .Setup(x => x.GetIdentityConverter(inType))
                .Returns(mockFactory.Object);

            m_instance.FindConverterChain(inType, inType)
                .Should().BeEquivalentTo(new[] { mockFactory.Object });

            mockFactory.VerifyAll();
            VerifyAll();
        }

        [Theory]
        [InlineData(typeof(Boolean), typeof(Byte))]
        public void FindConverterChain_ThereIsCastToBaseConverterFromInTypeToOutType_ReturnsExpected(Type inType, Type outType)
        {
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inType, outType))
                .Returns(mockFactory.Object);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeEquivalentTo(new[] { mockFactory.Object });

            mockFactory.VerifyAll();
            VerifyAll();
        }

        #region InType: Enum Or Primitive, OutType: Enum Or Primitive
        [Theory]
        [InlineData(typeof(FirstUInt32BasedEnum), typeof(SecondUInt32BasedEnum))]
        public void FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsEnumOrPrimitive_ReturnsExpected(Type inType, Type outType)
        {
            var array = new Converter[3];

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inType, outType))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inType.MakeNotNullable()))
                .Returns(true);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outType.MakeNotNullable()))
                .Returns(true);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.FindConverterChain(inType.MakeNotNullable(), outType.MakeNotNullable()))
                .Returns(array);

            m_instance.FindConverterChain(inType, outType)
                .Should().BeSameAs(array);

            VerifyAll();
        }
        #endregion

        #region InType: Enum Or Primitive, OutType: User Type
        public static IEnumerable<Object[]> FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty_Data()
        {
            foreach (var inType in new[] { typeof(DateTime) })
            {
                foreach (var outType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
                {
                    yield return new Object[] { new EnumOrPrimitive(inType), new UserTypeWithoutConverter(outType) };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty(EnumOrPrimitive inTypeSetup, UserTypeWithoutConverter outTypeSetup)
        {
            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(true);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithoutSuitableConvertersTo_ReturnsEmpty_Data()
        {
            foreach (var inType in new[] { typeof(DateTime) })
            {
                foreach (var outTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
                {
                    yield return new Object[] { new EnumOrPrimitive(inType), outTypeSetup };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithoutSuitableConvertersTo_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithoutSuitableConvertersTo_ReturnsEmpty(EnumOrPrimitive inTypeSetup, UserTypeWithConverter outTypeSetup)
        {
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);
            mockFactory.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(true);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.FindConverterChain(inTypeSetup.Type, outTypeSetup.MidTypeCore))
                .Returns(Array.Empty<Converter>());

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            mockFactory.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithConverterToInTypeCore_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Decimal) })
            {
                foreach (var outTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
                {
                    yield return new Object[] { new EnumOrPrimitive(inType), outTypeSetup };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithConverterToInTypeCore_ReturnsExpected_Data))]
        public void FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithConverterFromInTypeCore_ReturnsExpected(EnumOrPrimitive inTypeSetup, UserTypeWithConverter outTypeSetup)
        {
            inTypeSetup.Type.Should().BeSameAs(outTypeSetup.MidTypeCore);

            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory1.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(true);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockFactory1.Object });

            mockFactory1.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithExplicitConverterToMidType_ReturnsEmpty_Data()
        {
            foreach (var inType in new[] { typeof(DateTime) })
            {
                foreach (var outTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
                {
                    yield return new Object[] { new EnumOrPrimitive(inType), outTypeSetup };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithExplicitConverterToMidType_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithExplicitConverterToMidType_ReturnsEmpty(EnumOrPrimitive inTypeSetup, UserTypeWithConverter outTypeSetup)
        {
            inTypeSetup.Type.Should().NotBeSameAs(outTypeSetup.MidTypeCore);

            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory1.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            var mockFactory2 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory2.Setup(x => x.IsExplicit).Returns(true);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(true);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.FindConverterChain(inTypeSetup.Type, outTypeSetup.MidTypeCore))
                .Returns(new[] { mockFactory2.Object });

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithImplicitConverterToMidType_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(DateTime) })
            {
                foreach (var outTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
                {
                    yield return new Object[] { new EnumOrPrimitive(inType), outTypeSetup };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithImplicitConverterToMidType_ReturnsExpected_Data))]
        public void FindConverterChain_InTypeIsEnumOrPrimitive_OutTypeIsUserTypeWithImplicitConverterToMidType_ReturnsExpected(EnumOrPrimitive inTypeSetup, UserTypeWithConverter outTypeSetup)
        {
            inTypeSetup.Type.Should().NotBeSameAs(outTypeSetup.MidTypeCore);

            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory1.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            var mockFactory2 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory2.Setup(x => x.IsExplicit).Returns(false);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(true);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.FindConverterChain(inTypeSetup.Type, outTypeSetup.MidTypeCore))
                .Returns(new[] { mockFactory2.Object });

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockFactory2.Object, mockFactory1.Object });

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
            VerifyAll();
        }
        #endregion

        #region InType: User Type, OutType: Enum Or Primitive
        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithoutConvertersTo_OutTypeIsEnumOrPrimitive_ReturnsEmpty_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
            {
                foreach (var outType in new[] { typeof(DateTime) })
                {
                    yield return new Object[] { new UserTypeWithoutConverter(inType), new EnumOrPrimitive(outType) };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithoutConvertersTo_OutTypeIsEnumOrPrimitive_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithoutConvertersTo_OutTypeIsEnumOrPrimitive_ReturnsEmpty(UserTypeWithoutConverter inTypeSetup, EnumOrPrimitive outTypeSetup)
        {
            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(true);

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithoutSuitableConvertersTo_OutTypeIsEnumOrPrimitive_ReturnsEmpty_Data()
        {
            foreach (var inTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
            {
                foreach (var outType in new[] { typeof(DateTime) })
                {
                    yield return new Object[] { inTypeSetup, new EnumOrPrimitive(outType) };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithoutSuitableConvertersTo_OutTypeIsEnumOrPrimitive_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithoutSuitableConvertersTo_OutTypeIsEnumOrPrimitive_ReturnsEmpty(UserTypeWithConverter inTypeSetup, EnumOrPrimitive outTypeSetup)
        {
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);
            mockFactory.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(true);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.FindConverterChain(inTypeSetup.MidTypeCore, outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            mockFactory.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithConverterToOutTypeCore_OutTypeIsEnumOrPrimitive_ReturnsExpected_Data()
        {
            foreach (var inTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
            {
                foreach (var outType in new[] { typeof(Decimal) })
                {
                    yield return new Object[] { inTypeSetup, new EnumOrPrimitive(outType) };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithConverterToOutTypeCore_OutTypeIsEnumOrPrimitive_ReturnsExpected_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithConverterToOutTypeCore_OutTypeIsEnumOrPrimitive_ReturnsExpected(UserTypeWithConverter inTypeSetup, EnumOrPrimitive outTypeSetup)
        {
            inTypeSetup.MidTypeCore.Should().BeSameAs(outTypeSetup.Type);

            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory1.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(true);

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockFactory1.Object });

            mockFactory1.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsEnumOrPrimitiveWithExplicitConverterToMidType_ReturnsEmpty_Data()
        {
            foreach (var inTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
            {
                foreach (var outType in new[] { typeof(DateTime) })
                {
                    yield return new Object[] { inTypeSetup, new EnumOrPrimitive(outType) };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsEnumOrPrimitiveWithExplicitConverterToMidType_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsEnumOrPrimitiveWithExplicitConverterToMidType_ReturnsEmpty(UserTypeWithConverter inTypeSetup, EnumOrPrimitive outTypeSetup)
        {
            inTypeSetup.MidTypeCore.Should().NotBeSameAs(outTypeSetup.Type);

            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory1.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            var mockFactory2 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory2.Setup(x => x.IsExplicit).Returns(true);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(true);

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.FindConverterChain(inTypeSetup.MidTypeCore, outTypeSetup.Type))
                .Returns(new[] { mockFactory2.Object });

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsEnumOrPrimitiveWithImplicitConverterToMidType_ReturnsExpected_Data()
        {
            foreach (var inTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
            {
                foreach (var outType in new[] { typeof(DateTime) })
                {
                    yield return new Object[] { inTypeSetup, new EnumOrPrimitive(outType) };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsEnumOrPrimitiveWithImplicitConverterToMidType_ReturnsExpected_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsEnumOrPrimitiveWithImplicitConverterToMidType_ReturnsExpected(UserTypeWithConverter inTypeSetup, EnumOrPrimitive outTypeSetup)
        {
            inTypeSetup.MidTypeCore.Should().NotBeSameAs(outTypeSetup.Type);

            var mockFactory1 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory1.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            var mockFactory2 = new Mock<Converter>(MockBehavior.Strict);
            mockFactory2.Setup(x => x.IsExplicit).Returns(false);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(true);

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockFactory1.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.FindConverterChain(inTypeSetup.MidTypeCore, outTypeSetup.Type))
                .Returns(new[] { mockFactory2.Object });

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockFactory1.Object, mockFactory2.Object });

            mockFactory1.VerifyAll();
            mockFactory2.VerifyAll();
            VerifyAll();
        }
        #endregion

        #region InType: User Type without converters, OutType: User Type without converters
        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithoutConvertersTo_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
            {
                foreach (var outType in new[] { typeof(Boxed<Double>), typeof(Structed<Double>) })
                {
                    if (inType != outType)
                    {
                        yield return new Object[] { new UserTypeWithoutConverter(inType), new UserTypeWithoutConverter(outType) };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithoutConvertersTo_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithoutConvertersTo_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty(UserTypeWithoutConverter inTypeSetup, UserTypeWithoutConverter outTypeSetup)
        {
            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            VerifyAll();
        }
        #endregion

        #region InType: User Type with converters, OutType: User Type without converters
        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithoutSuitableConverter_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty_Data()
        {
            foreach (var inTypeSetup in GetUserTypesWithConverterToPrimitive<Decimal>())
            {
                foreach (var outType in new[] { typeof(Boxed<DateTime>), typeof(Structed<DateTime>) })
                {
                    yield return new Object[] { inTypeSetup, new UserTypeWithoutConverter(outType) };
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithoutSuitableConverter_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithoutSuitableConverter_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsEmpty(UserTypeWithConverter inTypeSetup, UserTypeWithoutConverter outTypeSetup)
        {
            inTypeSetup.MidTypeCore.Should().NotBeSameAs(outTypeSetup.Type);

            var mockInFactory = new Mock<Converter>(MockBehavior.Strict);
            mockInFactory.Setup(x => x.IsExplicit).Returns(inTypeSetup.IsExplicit);
            mockInFactory.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.MidTypeCore, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithConverterToOutType_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Double>), typeof(Structed<Double>) })
            {
                foreach (var midType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>), typeof(Structed<Decimal>?) })
                {
                    foreach (var outType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
                    {
                        if (midType.MakeNotNullable() == outType.MakeNotNullable())
                        {
                            foreach (var isExplicit in new[] { true, false })
                            {
                                yield return new Object[] { new UserTypeWithConverter(inType, midType, isExplicit), new UserTypeWithoutConverter(outType) };
                            }
                        }
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithConverterToOutType_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsExpected_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithConverterToOutType_OutTypeIsUserTypeWithoutConvertersFrom_ReturnsExpected(UserTypeWithConverter inTypeSetup, UserTypeWithoutConverter outTypeSetup)
        {
            inTypeSetup.MidTypeCore.Should().BeSameAs(outTypeSetup.Type);

            var mockInFactory = new Mock<Converter>(MockBehavior.Strict);
            mockInFactory.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());
            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockInFactory.Object });

            mockInFactory.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsUserTypeWithoutConvertersFrom_ThereIsDownCastConverterFromMidTypeToOutType_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
            {
                foreach (var midType in new[] { typeof(Boxed<Double>), typeof(Structed<Double>) })
                {
                    foreach (var isExplicit in new[] { true, false })
                    {
                        var outType = typeof(IBoxed<Double>);
                        yield return new Object[] { new UserTypeWithConverter(inType, midType, isExplicit), new UserTypeWithoutConverter(outType) };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsUserTypeWithoutConvertersFrom_ThereIsDownCastConverterFromMidTypeToOutType_ReturnsExpected_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithConverterToMidType_OutTypeIsUserTypeWithoutConvertersFrom_ThereIsDownCastConverterFromMidTypeToOutType_ReturnsExpected(
            UserTypeWithConverter inTypeSetup,
            UserTypeWithoutConverter outTypeSetup)
        {
            inTypeSetup.MidTypeCore.Should().NotBeSameAs(outTypeSetup.Type);

            var mockInToMidFactory = new Mock<Converter>(MockBehavior.Strict);
            mockInToMidFactory.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            var mockMidToOutFactory = new Mock<Converter>(MockBehavior.Strict);
            var mockResultFactory = new Mock<Converter>(MockBehavior.Strict);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.MidTypeCore, outTypeSetup.Type))
                .Returns(mockMidToOutFactory.Object);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInToMidFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInToMidFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockInToMidFactory.Object, mockMidToOutFactory.Object });

            VerifyAll();
        }
        #endregion

        #region InType: User Type without converters, OutType: User Type with converters
        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithoutConvertersFrom_OutTypeIsUserTypeWithoutSuitableConverter_ReturnsEmpty_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
            {
                foreach (var outType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
                {
                    if (inType != outType)
                    {
                        foreach (var midType in new[] { typeof(Decimal), typeof(Decimal?) })
                        {
                            foreach (var isExplicit in new[] { true, false })
                            {
                                yield return new Object[] { new UserTypeWithoutConverter(inType), new UserTypeWithConverter(outType, midType, isExplicit) };
                            }
                        }
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithoutConvertersFrom_OutTypeIsUserTypeWithoutSuitableConverter_ReturnsEmpty_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithoutConvertersFrom_OutTypeIsUserTypeWithoutSuitableConverter_ReturnsEmpty(UserTypeWithoutConverter inTypeSetup, UserTypeWithConverter outTypeSetup)
        {
            inTypeSetup.Type.Should().NotBeSameAs(outTypeSetup.MidTypeCore);

            var mockOutFactory = new Mock<Converter>(MockBehavior.Strict);
            mockOutFactory.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.MidTypeCore))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockOutFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockOutFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            mockOutFactory.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_InTypeIsUserTypeWithoutConvertersFrom_OutTypeIsUserTypeWithConverterToOutType_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
            {
                foreach (var outType in new[] { typeof(Boxed<Double>), typeof(Structed<Double>) })
                {
                    foreach (var midType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>), typeof(Structed<Decimal>?) })
                    {
                        if (inType.MakeNotNullable() == midType.MakeNotNullable())
                        {
                            foreach (var isExplicit in new[] { true, false })
                            {
                                yield return new Object[] { new UserTypeWithoutConverter(inType), new UserTypeWithConverter(outType, midType, isExplicit) };
                            }
                        }
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_InTypeIsUserTypeWithoutConvertersFrom_OutTypeIsUserTypeWithConverterToOutType_ReturnsExpected_Data))]
        public void FindConverterChain_InTypeIsUserTypeWithoutConvertersFrom_OutTypeIsUserTypeWithConverterToOutType_ReturnsExpected(UserTypeWithoutConverter inTypeSetup, UserTypeWithConverter outTypeSetup)
        {
            inTypeSetup.Type.Should().BeSameAs(outTypeSetup.MidTypeCore);

            var mockOutFactory = new Mock<Converter>(MockBehavior.Strict);
            mockOutFactory.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockOutFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockOutFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockOutFactory.Object });

            mockOutFactory.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_OutTypeIsUserTypeWithoutConvertersFrom_InTypeIsUserTypeWithConverterFromItToMidType_ThereIsDownCastConverterFromInTypeToMidType_ReturnsExpected_Data()
        {
            foreach (var inType in new[] { typeof(Boxed<Decimal>), typeof(Structed<Decimal>) })
            {
                var midType = typeof(IBoxed<Decimal>);

                foreach (var outType in new[] { typeof(Boxed<Double>), typeof(Structed<Double>) })
                {
                    foreach (var isExplicit in new[] { true, false })
                    {
                        yield return new Object[] { new UserTypeWithoutConverter(inType), new UserTypeWithConverter(outType, midType, isExplicit) };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_OutTypeIsUserTypeWithoutConvertersFrom_InTypeIsUserTypeWithConverterFromItToMidType_ThereIsDownCastConverterFromInTypeToMidType_ReturnsExpected_Data))]
        public void FindConverterChain_OutTypeIsUserTypeWithoutConvertersFrom_InTypeIsUserTypeWithConverterFromItToMidType_ThereIsDownCastConverterFromInTypeToMidType_ReturnsExpected(
            UserTypeWithoutConverter inTypeSetup,
            UserTypeWithConverter outTypeSetup)
        {
            inTypeSetup.Type.Should().NotBeSameAs(outTypeSetup.MidTypeCore);

            var mockMidToOutFactory = new Mock<Converter>(MockBehavior.Strict);
            mockMidToOutFactory.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            var mockInToMidFactory = new Mock<Converter>(MockBehavior.Strict);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.MidTypeCore))
                .Returns(mockInToMidFactory.Object);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockMidToOutFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockMidToOutFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockInToMidFactory.Object, mockMidToOutFactory.Object });

            mockInToMidFactory.VerifyAll();
            mockMidToOutFactory.VerifyAll();
            VerifyAll();
        }
        #endregion

        #region InType: User Type with converters, OutType: User Type with converters
        public static IEnumerable<Object[]> FindConverterChain_OutTypeIsUserTypeWithExplicitConvertersFromMidTypeToIt_InTypeIsUserTypeWithExplicitConverterFromItToMidType_ReturnsEmpty_Data()
        {
            foreach (var inTypeSetup in GetUserTypesWithConverterToUserType<Decimal, Single>().Where(x => x.IsExplicit == true))
            {
                foreach (var outTypeSetup in GetUserTypesWithConverterToUserType<Double, Single>().Where(x => x.IsExplicit == true))
                {
                    if (inTypeSetup.MidType.MakeNotNullable() == outTypeSetup.MidType.MakeNotNullable())
                    {
                        yield return new Object[] { inTypeSetup, outTypeSetup };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_OutTypeIsUserTypeWithExplicitConvertersFromMidTypeToIt_InTypeIsUserTypeWithExplicitConverterFromItToMidType_ReturnsEmpty_Data))]
        public void FindConverterChain_OutTypeIsUserTypeWithExplicitConvertersFromMidTypeToIt_InTypeIsUserTypeWithExplicitConverterFromItToMidType_ReturnsEmpty(
            UserTypeWithConverter inTypeSetup,
            UserTypeWithConverter outTypeSetup)
        {
            inTypeSetup.IsExplicit.Should().BeTrue();
            outTypeSetup.IsExplicit.Should().BeTrue();

            var mockInToMidFactory = new Mock<Converter>(MockBehavior.Strict);
            mockInToMidFactory.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            var mockMidToOutFactory = new Mock<Converter>(MockBehavior.Strict);
            mockMidToOutFactory.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.MidTypeCore))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.MidTypeCore, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                .Returns(Array.Empty<Converter>());
            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                .Returns(new[] { mockInToMidFactory.Object });

            m_mockUserTypeConverters
                .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                .Returns(Array.Empty<Converter>());
            m_mockUserTypeConverters
                .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                .Returns(new[] { mockMidToOutFactory.Object });

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            mockInToMidFactory.VerifyAll();
            mockMidToOutFactory.VerifyAll();
            VerifyAll();
        }

        public static IEnumerable<Object[]> FindConverterChain_OutTypeIsUserTypeWithConvertersFromMidTypeToIt_InTypeIsUserTypeWithConverterFromItToMidType_AtLeastOneConverterIsImplicit_ReturnsExpected_Data()
        {
            foreach (var inTypeSetup in GetUserTypesWithConverterToUserType<Decimal, Single>())
            {
                foreach (var outTypeSetup in GetUserTypesWithConverterToUserType<Double, Single>())
                {
                    if (inTypeSetup.MidType.MakeNotNullable() == outTypeSetup.MidType.MakeNotNullable()
                        && (!inTypeSetup.IsExplicit || !outTypeSetup.IsExplicit))
                    {
                        yield return new Object[] { inTypeSetup, outTypeSetup};
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_OutTypeIsUserTypeWithConvertersFromMidTypeToIt_InTypeIsUserTypeWithConverterFromItToMidType_AtLeastOneConverterIsImplicit_ReturnsExpected_Data))]
        public void FindConverterChain_OutTypeIsUserTypeWithConvertersFromMidTypeToIt_InTypeIsUserTypeWithConverterFromItToMidType_AtLeastOneConverterIsImplicit_ReturnsExpected(
            UserTypeWithConverter inTypeSetup,
            UserTypeWithConverter outTypeSetup)
        {
            var mockInToMidFactory = new Mock<Converter>(MockBehavior.Strict);
            mockInToMidFactory.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            var mockMidToOutFactory = new Mock<Converter>(MockBehavior.Strict);
            mockMidToOutFactory.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.MidTypeCore))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.MidTypeCore, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInToMidFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInToMidFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockMidToOutFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockMidToOutFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEquivalentTo(new[] { mockInToMidFactory.Object, mockMidToOutFactory.Object });

            mockInToMidFactory.VerifyAll();
            mockMidToOutFactory.VerifyAll();
            VerifyAll();
        }


        public static IEnumerable<Object[]> FindConverterChain_OutTypeIsUserTypeWithConvertersTo_InTypeIsUserTypeWithConvertersFrom_DifferentMidTypes_ReturnsEmpty_Data()
        {
            foreach (var inTypeSetup in GetUserTypesWithConverterToUserType<Int32, Int64>())
            {
                foreach (var outTypeSetup in GetUserTypesWithConverterToUserType<UInt32, UInt64>())
                {
                    if (!inTypeSetup.IsExplicit || !outTypeSetup.IsExplicit)
                    {
                        yield return new Object[] { inTypeSetup, outTypeSetup };
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(FindConverterChain_OutTypeIsUserTypeWithConvertersTo_InTypeIsUserTypeWithConvertersFrom_DifferentMidTypes_ReturnsEmpty_Data))]
        public void FindConverterChain_OutTypeIsUserTypeWithConvertersTo_InTypeIsUserTypeWithConvertersFrom_DifferentMidTypes_ReturnsEmpty(
            UserTypeWithConverter inTypeSetup,
            UserTypeWithConverter outTypeSetup)
        {
            var mockInToMidFactory = new Mock<Converter>(MockBehavior.Strict);
            mockInToMidFactory.Setup(x => x.OutType).Returns(inTypeSetup.MidType);

            var mockMidToOutFactory = new Mock<Converter>(MockBehavior.Strict);
            mockMidToOutFactory.Setup(x => x.InType).Returns(outTypeSetup.MidType);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.Type, outTypeSetup.MidTypeCore))
                .Returns((Converter)null);

            m_mockUpCastConverters
                .Setup(x => x.FindUpCastConverter(inTypeSetup.MidTypeCore, outTypeSetup.Type))
                .Returns((Converter)null);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(inTypeSetup.Type))
                .Returns(false);

            m_mockPrimitiveOrEnumConverters
                .Setup(x => x.IsSupported(outTypeSetup.Type))
                .Returns(false);

            if (inTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInToMidFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(new[] { mockInToMidFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersFrom(inTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            if (outTypeSetup.IsExplicit)
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockMidToOutFactory.Object });
            }
            else
            {
                m_mockUserTypeConverters
                    .Setup(x => x.GetImplicitConvertersTo(outTypeSetup.Type))
                    .Returns(new[] { mockMidToOutFactory.Object });
                m_mockUserTypeConverters
                    .Setup(x => x.GetExplicitConvertersTo(outTypeSetup.Type))
                    .Returns(Array.Empty<Converter>());
            }

            m_instance.FindConverterChain(inTypeSetup.Type, outTypeSetup.Type)
                .Should().BeEmpty();

            mockInToMidFactory.VerifyAll();
            mockMidToOutFactory.VerifyAll();
            VerifyAll();
        }
        #endregion

        #region helpers
        private static IEnumerable<UserTypeWithConverter> GetUserTypesWithConverterToPrimitive<T>()
            where T : struct
        {
            foreach (var type in new[] { typeof(Boxed<T>), typeof(Structed<T>) })
            {
                foreach (var midType in new[] { typeof(T), typeof(T?) })
                {
                    foreach (var isExplicit in new[] { true, false })
                    {
                        yield return new UserTypeWithConverter(type, midType, isExplicit);
                    }
                }
            }
        }

        private static IEnumerable<UserTypeWithConverter> GetUserTypesWithConverterToUserType<T, TMid>()
            where T : struct
            where TMid : struct
        {
            foreach (var type in new[] { typeof(Boxed<T>), typeof(Structed<T>) })
            {
                foreach (var midType in new[] { typeof(Boxed<TMid>), typeof(Structed<TMid>), typeof(Structed<TMid>?) })
                {
                    foreach (var isExplicit in new[] { true, false })
                    {
                        yield return new UserTypeWithConverter(type, midType, isExplicit);
                    }
                }
            }
        }

        public readonly struct EnumOrPrimitive
        {
            public EnumOrPrimitive(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
            }

            public Type Type { get; }
        }

        public readonly struct UserTypeWithoutConverter
        {
            public UserTypeWithoutConverter(Type type)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
            }

            public Type Type { get; }
        }

        public readonly struct UserTypeWithConverter
        {
            public UserTypeWithConverter(Type type, Type midType, Boolean isExplicit)
            {
                Type = type ?? throw new ArgumentNullException(nameof(type));
                MidType = midType ?? throw new ArgumentNullException(nameof(midType));
                IsExplicit = isExplicit;
            }

            public Type Type { get; }
            public Type MidType { get; }
            public Type MidTypeCore { get { return MidType.MakeNotNullable(); } }
            public Boolean IsExplicit { get; }
        }
        #endregion

        #region Helpers tests
        [Fact]
        public static void EnumOrPrimitive_Ctor_ThrowsArgumentNullException()
        {
            new Func<Object>(() => new EnumOrPrimitive(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");
        }

        [Fact]
        public static void UserTypeWithoutConverter_Ctor_ThrowsArgumentNullException()
        {
            new Func<Object>(() => new UserTypeWithoutConverter(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");
        }

        [Fact]
        public static void UserTypeWithConverter_Ctor_ThrowsArgumentNullException()
        {
            new Func<Object>(() => new UserTypeWithConverter(null, null, true))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");
            new Func<Object>(() => new UserTypeWithConverter(null, typeof(Byte), true))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("type");
            new Func<Object>(() => new UserTypeWithConverter(typeof(Byte), null, true))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("midType");
        }
        #endregion
    }
}