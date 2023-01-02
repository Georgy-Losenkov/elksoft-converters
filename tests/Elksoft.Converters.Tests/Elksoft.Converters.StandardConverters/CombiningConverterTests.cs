using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class CombiningConverterTests
    {
        public static IEnumerable<Object[]> Types()
        {
            var types = new[] { typeof(Int32), typeof(Int32?), typeof(String) };

            foreach (var type1 in types)
            {
                foreach (var type2 in types)
                {
                    foreach (var type3 in types)
                    {
                        yield return new Object[] { type1, type2, type3 };
                    }
                }
            }
        }

        private static void Test<TIn, TMid, TOut>(
            Action<(Mock<Converter<TIn, TMid>> MockFirstConverter, Mock<Converter<TMid, TOut>> MockSecondConverter)> arrange = null,
            Action<(Converter<TIn, TMid> FirstConverter, Converter<TMid, TOut> SecondConverter, CombiningConverter<TIn, TMid, TOut> CombiningConverter)> test = null)
        {
            var mockFirstConverter = new Mock<Converter<TIn, TMid>>(MockBehavior.Strict);
            var mockSecondConverter = new Mock<Converter<TMid, TOut>>(MockBehavior.Strict);

            if (arrange != null)
            {
                arrange((mockFirstConverter, mockSecondConverter));
            }

            var combiningConverter = new CombiningConverter<TIn, TMid, TOut>(mockFirstConverter.Object, mockSecondConverter.Object);

            test((mockFirstConverter.Object, mockSecondConverter.Object, combiningConverter));

            mockFirstConverter.VerifyAll();
            mockSecondConverter.VerifyAll();
        }

        internal static void Verify_AcceptsNull_IsValid<TIn, TMid, TOut>()
        {
            Test<TIn, TMid, TOut>(
                arrange: f => {
                    f.MockFirstConverter.Setup(x => x.AcceptsNull).Returns(true);
                },
                test: f => {
                    f.CombiningConverter.AcceptsNull.Should().BeTrue();
                });

            Test<TIn, TMid, TOut>(
                arrange: f => {
                    f.MockFirstConverter.Setup(x => x.AcceptsNull).Returns(false);
                },
                test: f => {
                    f.CombiningConverter.AcceptsNull.Should().BeFalse();
                });
        }

        internal static void Verify_IsExplicit_IsValid<TIn, TMid, TOut>()
        {
            Test<TIn, TMid, TOut>(
                arrange: f => {
                    f.MockFirstConverter.Setup(x => x.IsExplicit).Returns(true);
                },
                test: f => {
                    f.CombiningConverter.IsExplicit.Should().BeTrue();
                });

            Test<TIn, TMid, TOut>(
                arrange: f => {
                    f.MockFirstConverter.Setup(x => x.IsExplicit).Returns(false);
                    f.MockSecondConverter.Setup(x => x.IsExplicit).Returns(true);
                },
                test: f => {
                    f.CombiningConverter.IsExplicit.Should().BeTrue();
                });

            Test<TIn, TMid, TOut>(
                arrange: f => {
                    f.MockFirstConverter.Setup(x => x.IsExplicit).Returns(false);
                    f.MockSecondConverter.Setup(x => x.IsExplicit).Returns(false);
                },
                test: f => {
                    f.CombiningConverter.IsExplicit.Should().BeFalse();
                });
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void AcceptsNull_IsValid(Type typeIn, Type typeMid, Type typeOut)
        {
            Utilities.Invoke(typeIn, typeMid, typeOut, Verify_AcceptsNull_IsValid<Int32, Int32, Int32>);
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal static void IsExplicit_IsValid(Type typeIn, Type typeMid, Type typeOut)
        {
            Utilities.Invoke(typeIn, typeMid, typeOut, Verify_IsExplicit_IsValid<Int32, Int32, Int32>);
        }

        private static void Verify_Ctor_NullParameter_ThrowsArgumentNullException<TIn, TMid, TOut>()
        {
            var mockFirstConverter = new Mock<Converter<TIn, TMid>>();
            var mockSecondConverter = new Mock<Converter<TMid, TOut>>();

            new Func<Object>(() => new CombiningConverter<TIn, TMid, TOut>(null, null))
                .Should().Throw<ArgumentNullException>().WithParameterName("firstConverter");
            new Func<Object>(() => new CombiningConverter<TIn, TMid, TOut>(null, mockSecondConverter.Object))
                .Should().Throw<ArgumentNullException>().WithParameterName("firstConverter");
            new Func<Object>(() => new CombiningConverter<TIn, TMid, TOut>(mockFirstConverter.Object, null))
                .Should().Throw<ArgumentNullException>().WithParameterName("secondConverter");

            mockFirstConverter.VerifyAll();
            mockSecondConverter.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types))]
        internal void Ctor_NullParameter_ThrowsArgumentNullException(Type typeIn, Type typeMid, Type typeOut)
        {
            Utilities.Invoke(typeIn, typeMid, typeOut, Verify_Ctor_NullParameter_ThrowsArgumentNullException<Int32, Int32, Int32>);
        }

        private static void Verify_Ctor_AssignsProperties<TIn, TMid, TOut>()
        {
            var mockFirstConverter = new Mock<Converter<TIn, TMid>>(MockBehavior.Strict);
            var mockSecondConverter = new Mock<Converter<TMid, TOut>>(MockBehavior.Strict);

            var combiningConverter = new CombiningConverter<TIn, TMid, TOut>(mockFirstConverter.Object, mockSecondConverter.Object);

            combiningConverter.FirstConverter.Should().BeSameAs(mockFirstConverter.Object);
            combiningConverter.SecondConverter.Should().BeSameAs(mockSecondConverter.Object);

            mockFirstConverter.VerifyAll();
            mockSecondConverter.VerifyAll();
        }

        [Theory]
        [MemberData(nameof(Types))]
        public static void Ctor_AssignsProperties(Type typeIn, Type typeMid, Type typeOut)
        {
            Utilities.Invoke(typeIn, typeMid, typeOut, Verify_Ctor_AssignsProperties<Int32, Int32, Int32>);
        }

        [Fact]
        public static void Convert_InvokeConvertMethods()
        {
            var input1 = new Object();
            var input2 = new Object();
            var input3 = new Object();
            var medium1 = new Object();
            var medium2 = new Object();
            var message3 = "second error";
            var output1 = new Object();
            var message2 = "first error";

            Test<Object, Object, Object>(
                arrange: f => {
                    f.MockFirstConverter.Setup(x => x.Convert(input1, null)).Returns(medium1);
                    f.MockFirstConverter.Setup(x => x.Convert(input2, null)).Returns(medium2);
                    f.MockFirstConverter.Setup(x => x.Convert(input3, null)).Throws(() => new NotSupportedException(message3));

                    f.MockSecondConverter.Setup(x => x.Convert(medium1, null)).Returns(output1);
                    f.MockSecondConverter.Setup(x => x.Convert(medium2, null)).Throws(() => new InvalidOperationException(message2));
                },
                test: f => {
                    f.CombiningConverter.Convert(input1, null).Should().BeSameAs(output1);
                    f.CombiningConverter.Invoking(x => x.Convert(input2, null)).Should().ThrowExactly<InvalidOperationException>().WithMessage(message2);
                    f.CombiningConverter.Invoking(x => x.Convert(input3, null)).Should().ThrowExactly<NotSupportedException>().WithMessage(message3);
                });

            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

            Test<Object, Object, Object>(
                arrange: f => {
                    f.MockFirstConverter.Setup(x => x.Convert(input1, mockProvider.Object)).Returns(medium1);
                    f.MockFirstConverter.Setup(x => x.Convert(input2, mockProvider.Object)).Returns(medium2);
                    f.MockFirstConverter.Setup(x => x.Convert(input3, mockProvider.Object)).Throws(() => new NotSupportedException(message3));

                    f.MockSecondConverter.Setup(x => x.Convert(medium1, mockProvider.Object)).Returns(output1);
                    f.MockSecondConverter.Setup(x => x.Convert(medium2, mockProvider.Object)).Throws(() => new InvalidOperationException(message2));
                },
                test: f => {
                    f.CombiningConverter.Convert(input1, mockProvider.Object).Should().BeSameAs(output1);
                    f.CombiningConverter.Invoking(x => x.Convert(input2, mockProvider.Object)).Should().ThrowExactly<InvalidOperationException>().WithMessage(message2);
                    f.CombiningConverter.Invoking(x => x.Convert(input3, mockProvider.Object)).Should().ThrowExactly<NotSupportedException>().WithMessage(message3);
                });

            mockProvider.VerifyAll();
        }
    }
}