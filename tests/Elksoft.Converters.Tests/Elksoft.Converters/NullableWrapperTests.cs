using System;
using System.Collections;
using System.Collections.Generic;
using Elksoft.Converters.Transformers;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters
{
    public class NullableWrapperTests
    {
        #region GetWrapKind
        public static IEnumerable<Object[]> GetWrapKind_ArgumentNull_TestData()
        {
            var types = new Type[] {
                typeof(Byte),
                typeof(Byte?),
                typeof(Boxed<Byte>)
            };

            yield return new Object[] { null, null, "inType" };

            foreach (var type in types)
            {
                yield return new Object[] { null, type, "inType" };
            }

            foreach (var type in types)
            {
                yield return new Object[] { type, null, "outType" };
            }
        }

        [Theory]
        [MemberData(nameof(GetWrapKind_ArgumentNull_TestData))]
        internal void GetWrapKind_ThrowsArgumentNullException(Type x, Type y, String paramName)
        {
            new Action(() => NullableWrapper.GetWrapKind(x, y))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName(paramName);
        }

        public static IEnumerable<Object[]> GetWrapKind_TestData()
        {
            return new TheoryData<Type, Type, NullableWrapper.WrapKind> {
                { typeof(Byte), typeof(Byte), NullableWrapper.WrapKind.SS },
                { typeof(Byte), typeof(Byte?), NullableWrapper.WrapKind.SN },
                { typeof(Byte), typeof(Int32), NullableWrapper.WrapKind.UN },
                { typeof(Byte), typeof(Int32?), NullableWrapper.WrapKind.UN },
                { typeof(Byte), typeof(Object), NullableWrapper.WrapKind.UN },
                { typeof(Byte), typeof(Boxed<Byte>), NullableWrapper.WrapKind.UN },
                { typeof(Byte), typeof(Boxed<Int32>), NullableWrapper.WrapKind.UN },

                { typeof(Byte?), typeof(Byte), NullableWrapper.WrapKind.NS },
                { typeof(Byte?), typeof(Byte?), NullableWrapper.WrapKind.NN },
                { typeof(Byte?), typeof(Int32), NullableWrapper.WrapKind.UN },
                { typeof(Byte?), typeof(Int32?), NullableWrapper.WrapKind.UN },
                { typeof(Byte?), typeof(Object), NullableWrapper.WrapKind.UN },
                { typeof(Byte?), typeof(Boxed<Byte>), NullableWrapper.WrapKind.UN },
                { typeof(Byte?), typeof(Boxed<Int32>), NullableWrapper.WrapKind.UN },

                { typeof(Int32), typeof(Byte), NullableWrapper.WrapKind.UN },
                { typeof(Int32), typeof(Byte?), NullableWrapper.WrapKind.UN },
                { typeof(Int32), typeof(Int32), NullableWrapper.WrapKind.SS },
                { typeof(Int32), typeof(Int32?), NullableWrapper.WrapKind.SN },
                { typeof(Int32), typeof(Object), NullableWrapper.WrapKind.UN },
                { typeof(Int32), typeof(Boxed<Byte>), NullableWrapper.WrapKind.UN },
                { typeof(Int32), typeof(Boxed<Int32>), NullableWrapper.WrapKind.UN },

                { typeof(Int32?), typeof(Byte), NullableWrapper.WrapKind.UN },
                { typeof(Int32?), typeof(Byte?), NullableWrapper.WrapKind.UN },
                { typeof(Int32?), typeof(Int32), NullableWrapper.WrapKind.NS },
                { typeof(Int32?), typeof(Int32?), NullableWrapper.WrapKind.NN },
                { typeof(Int32?), typeof(Object), NullableWrapper.WrapKind.UN },
                { typeof(Int32?), typeof(Boxed<Byte>), NullableWrapper.WrapKind.UN },
                { typeof(Int32?), typeof(Boxed<Int32>), NullableWrapper.WrapKind.UN },

                { typeof(Object), typeof(Byte), NullableWrapper.WrapKind.UN },
                { typeof(Object), typeof(Byte?), NullableWrapper.WrapKind.UN },
                { typeof(Object), typeof(Int32), NullableWrapper.WrapKind.UN },
                { typeof(Object), typeof(Int32?), NullableWrapper.WrapKind.UN },
                { typeof(Object), typeof(Object), NullableWrapper.WrapKind.CC },
                { typeof(Object), typeof(Boxed<Byte>), NullableWrapper.WrapKind.UN },
                { typeof(Object), typeof(Boxed<Int32>), NullableWrapper.WrapKind.UN },

                { typeof(Boxed<Byte>), typeof(Byte), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Byte>), typeof(Byte?), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Byte>), typeof(Int32), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Byte>), typeof(Int32?), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Byte>), typeof(Object), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Byte>), typeof(Boxed<Byte>), NullableWrapper.WrapKind.CC },
                { typeof(Boxed<Byte>), typeof(Boxed<Int32>), NullableWrapper.WrapKind.UN },

                { typeof(Boxed<Int32>), typeof(Byte), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Int32>), typeof(Byte?), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Int32>), typeof(Int32), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Int32>), typeof(Int32?), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Int32>), typeof(Object), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Int32>), typeof(Boxed<Byte>), NullableWrapper.WrapKind.UN },
                { typeof(Boxed<Int32>), typeof(Boxed<Int32>), NullableWrapper.WrapKind.CC },
            };
        }

        [Theory]
        [MemberData(nameof(GetWrapKind_TestData))]
        internal void GetWrapKind_Invoke_ReturnsExpected(Type x, Type y, NullableWrapper.WrapKind expected)
        {
            NullableWrapper.GetWrapKind(x, y).Should().Be(expected);
        }
        #endregion

        #region Wrap
        [Fact]
        internal void Wrap_ThrowsArgumentNullException()
        {
            var inputType = typeof(Byte?);
            var mockFactory = new Mock<Converter>(MockBehavior.Strict);
            var outputType = typeof(Int16?);

            var helper = new NullableWrapper();

            helper.Invoking(x => x.Wrap(null, mockFactory.Object, outputType))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("inType");
            helper.Invoking(x => x.Wrap(inputType, null, outputType))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("converter");
            helper.Invoking(x => x.Wrap(inputType, mockFactory.Object, null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("outType");

            mockFactory.VerifyAll();
        }

        [Fact]
        internal void Wrap_FactoryInTypeIsNotCompatibleWithInType_ThrowsArgumentException()
        {
            var inputType = typeof(Int16);
            var outputType = typeof(Int64);

            var mockFactory = new Mock<Converter>(MockBehavior.Strict);
            mockFactory.Setup(x => x.InType).Returns(typeof(Int32));

            var helper = new NullableWrapper();
            helper.Invoking(x => x.Wrap(inputType, mockFactory.Object, outputType))
                .Should().ThrowExactly<ArgumentException>().WithParameterName("converter");

            mockFactory.VerifyAll();
        }

        [Fact]
        internal void Wrap_FactoryOutTypeIsNotCompatibleWithOutType_ThrowsArgumentException()
        {
            var inputType = typeof(Int16);
            var outputType = typeof(Int64);

            var mockFactory = new Mock<Converter>(MockBehavior.Strict);
            mockFactory.Setup(x => x.InType).Returns(typeof(Int16));
            mockFactory.Setup(x => x.OutType).Returns(typeof(Int32));

            var helper = new NullableWrapper();
            helper.Invoking(x => x.Wrap(inputType, mockFactory.Object, outputType))
                .Should().ThrowExactly<ArgumentException>().WithParameterName("converter");

            mockFactory.VerifyAll();
        }

        public static TheoryData<Type, Type> Types_CC_CC()
        {
            return new TheoryData<Type, Type> {
                { typeof(String), typeof(IEnumerable) },
                { typeof(IEnumerable), typeof(String) },
            };
        }

        public static TheoryData<Type, Type> Types_CC_SS()
        {
            return new TheoryData<Type, Type> {
                { typeof(String), typeof(Byte) },
                { typeof(String), typeof(Int32) },
            };
        }

        public static TheoryData<Type, Type> Types_SS_CC()
        {
            return new TheoryData<Type, Type> {
                { typeof(Byte), typeof(String) },
                { typeof(Int32), typeof(String) },
            };
        }

        public static TheoryData<Type, Type> Types_SS_SS()
        {
            return new TheoryData<Type, Type> {
                { typeof(Int32), typeof(Byte) },
                { typeof(Byte), typeof(Int32) },
            };
        }

        #region Wrap_CCCC
        private static void Verify_Wrap_AcceptingNullCCCC_Returns_SameConverter<TIn, TOut>()
            where TIn : class
            where TOut : class
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_CC))]
        internal static void Wrap_AcceptingNullCCCC_Returns_SameConverter(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullCCCC_Returns_SameConverter<String, String>);
        }

        private static void Verify_Wrap_RejectingNullCCCC_Returns_RejectingNullWrapperCCCC<TIn, TOut>()
            where TIn : class
            where TOut : class
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut))
                .Should().BeOfType<RejectingNullWrapperCCCC<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_CC))]
        internal static void Wrap_RejectingNullCCCC_Returns_RejectingNullWrapperCCCC(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullCCCC_Returns_RejectingNullWrapperCCCC<String, String>);
        }
        #endregion

        #region Wrap_CCNN
        private static void Verify_Wrap_AcceptingNullCCNN_Returns_SameConverter<TIn, TOut>()
            where TIn : class
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut?>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut?)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_SS))]
        internal static void Wrap_AcceptingNullCCNN_Returns_SameConverter(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullCCNN_Returns_SameConverter<Object, Int32>);
        }

        private static void Verify_Wrap_RejectingNullCCNN_Returns_RejectingNullWrapperCCNN<TIn, TOut>()
            where TIn : class
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut?>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<RejectingNullWrapperCCNN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_SS))]
        internal static void Wrap_RejectingNullCCNN_Returns_RejectingNullWrapperCCNN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullCCNN_Returns_RejectingNullWrapperCCNN<Object, Int32>);
        }
        #endregion

        #region Wrap_CCNS
        private static void Verify_Wrap_AcceptingNullCCNS_Returns_AcceptingNullWrapperCCNS<TIn, TOut>()
            where TIn : class
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut?>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut))
                .Should().BeOfType<AcceptingNullWrapperCCNS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_SS))]
        internal static void Wrap_AcceptingNullCCNS_Returns_AcceptingNullWrapperCCNS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullCCNS_Returns_AcceptingNullWrapperCCNS<Object, Int32>);
        }

        private static void Verify_Wrap_RejectingNullCCNS_Returns_RejectingNullWrapperCCNS<TIn, TOut>()
            where TIn : class
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut?>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut))
                .Should().BeOfType<RejectingNullWrapperCCNS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_SS))]
        internal static void Wrap_RejectingNullCCNS_Returns_RejectingNullWrapperCCNS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullCCNS_Returns_RejectingNullWrapperCCNS<Object, Int32>);
        }
        #endregion

        #region Wrap_CCSN
        private static void Verify_Wrap_AcceptingNullCCSN_Returns_AcceptingNullWrapperCCSN<TIn, TOut>()
            where TIn : class
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<AcceptingNullWrapperCCSN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_SS))]
        internal static void Wrap_AcceptingNullCCSN_Returns_AcceptingNullWrapperCCSN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullCCSN_Returns_AcceptingNullWrapperCCSN<Object, Int32>);
        }

        private static void Verify_Wrap_RejectingNullCCSN_Returns_RejectingNullWrapperCCSN<TIn, TOut>()
            where TIn : class
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<RejectingNullWrapperCCSN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_SS))]
        internal static void Wrap_RejectingNullCCSN_Returns_RejectingNullWrapperCCSN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullCCSN_Returns_RejectingNullWrapperCCSN<Object, Int32>);
        }
        #endregion

        #region Wrap_CCSS
        private static void Verify_Wrap_AcceptingNullCCSS_Returns_SameConverter<TIn, TOut>()
            where TIn : class
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_SS))]
        internal static void Wrap_AcceptingNullCCSS_Returns_SameConverter(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullCCSS_Returns_SameConverter<Object, Int32>);
        }

        private static void Verify_Wrap_RejectingNullCCSS_Returns_RejectingNullWrapperCCSS<TIn, TOut>()
            where TIn : class
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut))
                .Should().BeOfType<RejectingNullWrapperCCSS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_CC_SS))]
        internal static void Wrap_RejectingNullCCSS_Returns_RejectingNullWrapperCCSS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullCCSS_Returns_RejectingNullWrapperCCSS<Object, Int32>);
        }
        #endregion

        #region Wrap_NNCC
        private static void Verify_Wrap_AcceptingNullNNCC_Returns_SameConverter<TIn, TOut>()
            where TIn : struct
            where TOut : class
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_CC))]
        internal static void Wrap_AcceptingNullNNCC_Returns_SameConverter(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullNNCC_Returns_SameConverter<Int32, String>);
        }

        private static void Verify_Wrap_RejectingNullNNCC_Returns_RejectingNullWrapperNNCC<TIn, TOut>()
            where TIn : struct
            where TOut : class
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut))
                .Should().BeOfType<RejectingNullWrapperNNCC<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_CC))]
        internal static void Wrap_RejectingNullNNCC_Returns_RejectingNullWrapperNNCC(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullNNCC_Returns_RejectingNullWrapperNNCC<Int32, String>);
        }
        #endregion

        #region Wrap_NNNN
        private static void Verify_Wrap_AcceptingNullNNNN_Returns_SameConverter<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut?>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut?)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_AcceptingNullNNNN_Returns_SameConverter(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullNNNN_Returns_SameConverter<Int32, Int32>);
        }

        private static void Verify_Wrap_RejectingNullNNNN_Returns_RejectingNullWrapperNNNN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut?>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<RejectingNullWrapperNNNN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_RejectingNullNNNN_Returns_RejectingNullWrapperNNNN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullNNNN_Returns_RejectingNullWrapperNNNN<Int32, Int32>);
        }
        #endregion

        #region Wrap_NNNS
        private static void Verify_Wrap_AcceptingNullNNNS_Returns_AcceptingNullWrapperNNNS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut?>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut))
                .Should().BeOfType<AcceptingNullWrapperNNNS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_AcceptingNullNNNS_Returns_AcceptingNullWrapperNNNS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullNNNS_Returns_AcceptingNullWrapperNNNS<Int32, Int32>);
        }

        private static void Verify_Wrap_RejectingNullNNNS_Returns_RejectingNullWrapperNNNS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut?>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut))
                .Should().BeOfType<RejectingNullWrapperNNNS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_RejectingNullNNNS_Returns_RejectingNullWrapperNNNS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullNNNS_Returns_RejectingNullWrapperNNNS<Int32, Int32>);
        }
        #endregion

        #region Wrap_NNSN
        private static void Verify_Wrap_AcceptingNullNNSN_Returns_AcceptingNullWrapperNNSN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<AcceptingNullWrapperNNSN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_AcceptingNullNNSN_Returns_AcceptingNullWrapperNNSN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullNNSN_Returns_AcceptingNullWrapperNNSN<Int32, Int32>);
        }

        private static void Verify_Wrap_RejectingNullNNSN_Returns_RejectingNullWrapperNNSN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<RejectingNullWrapperNNSN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_RejectingNullNNSN_Returns_RejectingNullWrapperNNSN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullNNSN_Returns_RejectingNullWrapperNNSN<Int32, Int32>);
        }
        #endregion

        #region Wrap_NNSS
        private static void Verify_Wrap_AcceptingNullNNSS_Returns_SameConverter<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(true);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_AcceptingNullNNSS_Returns_SameConverter(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_AcceptingNullNNSS_Returns_SameConverter<Int32, Int32>);
        }

        private static void Verify_Wrap_RejectingNullNNSS_Returns_RejectingNullWrapperNNSS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);
            mockSource.Setup(x => x.AcceptsNull).Returns(false);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut))
                .Should().BeOfType<RejectingNullWrapperNNSS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_RejectingNullNNSS_Returns_RejectingNullWrapperNNSS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_RejectingNullNNSS_Returns_RejectingNullWrapperNNSS<Int32, Int32>);
        }
        #endregion

        #region Wrap_NSCC
        private static void Verify_Wrap_NSCC_Returns_CommonWrapperNSCC<TIn, TOut>()
            where TIn : struct
            where TOut : class
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut))
                .Should().BeOfType<CommonWrapperNSCC<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_CC))]
        internal static void Wrap_NSCC_Returns_CommonWrapperNSCC(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_NSCC_Returns_CommonWrapperNSCC<Int32, String>);
        }
        #endregion

        #region Wrap_NSNN
        private static void Verify_Wrap_NSNN_Returns_CommonWrapperNSNN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut?>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<CommonWrapperNSNN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_NSNN_Returns_CommonWrapperNSNN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_NSNN_Returns_CommonWrapperNSNN<Int32, Int32>);
        }
        #endregion

        #region Wrap_NSNS
        private static void Verify_Wrap_NSNS_Returns_CommonWrapperNSNS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut?>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut))
                .Should().BeOfType<CommonWrapperNSNS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_NSNS_Returns_CommonWrapperNSNS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_NSNS_Returns_CommonWrapperNSNS<Int32, Int32>);
        }
        #endregion

        #region Wrap_NSSN
        private static void Verify_Wrap_NSSN_Returns_CommonWrapperNSSN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<CommonWrapperNSSN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_NSSN_Returns_CommonWrapperNSSN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_NSSN_Returns_CommonWrapperNSSN<Int32, Int32>);
        }
        #endregion

        #region Wrap_NSSS
        private static void Verify_Wrap_NSSS_Returns_CommonWrapperNSSS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn?), mockSource.Object, typeof(TOut))
                .Should().BeOfType<CommonWrapperNSSS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_NSSS_Returns_CommonWrapperNSSS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_NSSS_Returns_CommonWrapperNSSS<Int32, Int32>);
        }
        #endregion

        #region Wrap_SNCC
        private static void Verify_Wrap_SNCC_Returns_CommonWrapperSNCC<TIn, TOut>()
            where TIn : struct
            where TOut : class
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut))
                .Should().BeOfType<CommonWrapperSNCC<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_CC))]
        internal static void Wrap_SNCC_Returns_CommonWrapperSNCC(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SNCC_Returns_CommonWrapperSNCC<Int32, String>);
        }
        #endregion

        #region Wrap_SNNN
        private static void Verify_Wrap_SNNN_Returns_CommonWrapperSNNN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut?>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<CommonWrapperSNNN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_SNNN_Returns_CommonWrapperSNNN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SNNN_Returns_CommonWrapperSNNN<Int32, Int32>);
        }
        #endregion

        #region Wrap_SNNS
        private static void Verify_Wrap_SNNS_Returns_CommonWrapperSNNS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut?>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut))
                .Should().BeOfType<CommonWrapperSNNS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_SNNS_Returns_CommonWrapperSNNS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SNNS_Returns_CommonWrapperSNNS<Int32, Int32>);
        }
        #endregion

        #region Wrap_SNSN
        private static void Verify_Wrap_SNSN_Returns_CommonWrapperSNSN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<CommonWrapperSNSN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_SNSN_Returns_CommonWrapperSNSN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SNSN_Returns_CommonWrapperSNSN<Int32, Int32>);
        }
        #endregion

        #region Wrap_SNSS
        private static void Verify_Wrap_SNSS_Returns_CommonWrapperSNSS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn?, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut))
                .Should().BeOfType<CommonWrapperSNSS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_SNSS_Returns_CommonWrapperSNSS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SNSS_Returns_CommonWrapperSNSS<Int32, Int32>);
        }
        #endregion

        #region Wrap_SSCC
        private static void Verify_Wrap_SSCC_Returns_CommonWrapperSSCC<TIn, TOut>()
            where TIn : struct
            where TOut : class
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_CC))]
        internal static void Wrap_SSCC_Returns_CommonWrapperSSCC(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SSCC_Returns_CommonWrapperSSCC<Int32, String>);
        }
        #endregion

        #region Wrap_SSNN
        private static void Verify_Wrap_SSNN_Returns_CommonWrapperSSNN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut?>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut?)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_SSNN_Returns_CommonWrapperSSNN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SSNN_Returns_CommonWrapperSSNN<Int32, Int32>);
        }
        #endregion

        #region Wrap_SSNS
        private static void Verify_Wrap_SSNS_Returns_CommonWrapperSSNS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut?>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut))
                .Should().BeOfType<CommonWrapperSSNS<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_SSNS_Returns_CommonWrapperSSNS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SSNS_Returns_CommonWrapperSSNS<Int32, Int32>);
        }
        #endregion

        #region Wrap_SSSN
        private static void Verify_Wrap_SSSN_Returns_CommonWrapperSSSN<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut?))
                .Should().BeOfType<CommonWrapperSSSN<TIn, TOut>>()
                .Which.InnerConverter.Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_SSSN_Returns_CommonWrapperSSSN(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SSSN_Returns_CommonWrapperSSSN<Int32, Int32>);
        }
        #endregion

        #region Wrap_SSSS
        private static void Verify_Wrap_SSSS_Returns_CommonWrapperSSSS<TIn, TOut>()
            where TIn : struct
            where TOut : struct
        {
            var mockSource = new Mock<Converter<TIn, TOut>>(MockBehavior.Strict);

            var helper = new NullableWrapper();
            helper.Wrap(typeof(TIn), mockSource.Object, typeof(TOut)).Should().BeSameAs(mockSource.Object);

            mockSource.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types_SS_SS))]
        internal static void Wrap_SSSS_Returns_CommonWrapperSSSS(Type inType, Type outType)
        {
            Utilities.Invoke(inType, outType, Verify_Wrap_SSSS_Returns_CommonWrapperSSSS<Int32, Int32>);
        }
        #endregion
        #endregion
    }
}