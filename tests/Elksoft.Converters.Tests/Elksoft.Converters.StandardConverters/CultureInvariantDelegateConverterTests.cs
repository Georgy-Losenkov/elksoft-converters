using System;
using System.Collections.Generic;
using System.Reflection;
using Elksoft.Samples;
using FluentAssertions;
using Moq;
using Xunit;

namespace Elksoft.Converters.StandardConverters
{
    public class CultureInvariantDelegateConverterTests
    {
        #region TryCreateUserTypeConversionFactory
        [Fact]
        internal static void TryCreateUserTypeConversionFactory_ThrowsArgumentNullException()
        {
            new Action(() => CultureInvariantDelegateConverter.TryCreateUserTypeConverter(null))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("method");
        }

        public static IEnumerable<Object[]> TryCreateUserTypeConversionFactory_Data(Boolean valid)
        {
            Func<MemberInfo, Boolean> filter;
            if (valid)
            {
                filter = method => method.Name == "op_Implicit" || method.Name == "op_Explicit";
            }
            else
            {
                filter = method => method.Name != "op_Implicit" && method.Name != "op_Explicit";
            }

            foreach (var type in new[] { typeof(SampleClass), typeof(SampleStruct), typeof(ClassWithoutOperators), typeof(StructWithoutOperators) })
            {
                foreach (var method in type.GetMethods())
                {
                    if (filter(method))
                    {
                        yield return new Object[] { method };
                        if (method.IsGenericMethod)
                        {
                            yield return new Object[] { method.GetGenericMethodDefinition() };
                        }
                    }
                }
            }
        }

        [Theory]
        [MemberData(nameof(TryCreateUserTypeConversionFactory_Data), false)]
        internal static void TryCreateUserTypeConversionFactory_ReturnsNull(MethodInfo method)
        {
            CultureInvariantDelegateConverter.TryCreateUserTypeConverter(method).Should().BeNull();
        }

        [Theory]
        [MemberData(nameof(TryCreateUserTypeConversionFactory_Data), true)]
        internal static void TryCreateUserTypeConversionFactory_ReturnsExpected(MethodInfo method)
        {
            var converter = CultureInvariantDelegateConverter.TryCreateUserTypeConverter(method);
            converter.Should().NotBeNull();

            converter.GetType().GetGenericTypeDefinition().Should().BeSameAs(typeof(CultureInvariantDelegateConverter<,>));

            ((IDelegateConverter)converter).Func.Method.Should().BeSameAs(method);

            (converter.InType.MakeNotNullable() == method.DeclaringType || converter.OutType.MakeNotNullable() == method.DeclaringType).Should().BeTrue();
        }
        #endregion

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        internal void Ctor_NullParameter_ThrowsArgumentNullException(Boolean acceptsNull, Boolean isExplicit)
        {
            new Func<Object>(() => new CultureInvariantDelegateConverter<Object, Object>(null, isExplicit, acceptsNull))
                .Should().ThrowExactly<ArgumentNullException>().WithParameterName("func");
        }

        private static void Test<TIn, TOut>(
            Boolean acceptsNull,
            Boolean isExplicit,
            Action<Mock<Func<TIn, TOut>>> setupPhase,
            Action<Func<TIn, TOut>, CultureInvariantDelegateConverter<TIn, TOut>> testPhase)
        {
            var mockDelegate = new Mock<Func<TIn, TOut>>(MockBehavior.Strict);

            if (setupPhase != null)
            {
                setupPhase(mockDelegate);
            }

            var func = mockDelegate.Object;
            var converter = new CultureInvariantDelegateConverter<TIn, TOut>(func, isExplicit, acceptsNull);

            if (testPhase != null)
            {
                testPhase(func, converter);
            }

            mockDelegate.VerifyAll();
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        internal void Ctor_Assigns_AcceptsNull(Boolean acceptsNull, Boolean isExplicit)
        {
            Test<Object, Object>(
                acceptsNull: acceptsNull,
                isExplicit: isExplicit,
                setupPhase: _ => { },
                testPhase: (func, converter) => {
                    converter.AcceptsNull.Should().Be(acceptsNull);
                });
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        internal void Ctor_Assigns_IsExplicit(Boolean acceptsNull, Boolean isExplicit)
        {
            Test<Object, Object>(
                acceptsNull: acceptsNull,
                isExplicit: isExplicit,
                setupPhase: _ => { },
                testPhase: (func, converter) => {
                    converter.IsExplicit.Should().Be(isExplicit);
                });
        }

        [Theory]
        [InlineData(false, false)]
        [InlineData(false, true)]
        [InlineData(true, false)]
        [InlineData(true, true)]
        internal void Convert_WorksAsExpected(Boolean acceptsNull, Boolean isExplicit)
        {
            var input1 = new Object();
            var input2 = new Object();
            var input3 = new Object();
            var input4 = new Object();
            var input5 = new Object();
            var input6 = new Object();
            var output1 = new Object();
            var output2 = new Object();
            var output3 = default(Object);
            var output4 = default(Object);
            var message5 = "first message";
            var message6 = "second message";

            Test<Object, Object>(
                acceptsNull: acceptsNull,
                isExplicit: isExplicit,
                setupPhase: mock => {
                    mock.Setup(x => x.Invoke(input1)).Returns(output1);
                    mock.Setup(x => x.Invoke(input2)).Returns(output2);
                    mock.Setup(x => x.Invoke(input3)).Returns(output3);
                    mock.Setup(x => x.Invoke(input4)).Returns(output4);
                    mock.Setup(x => x.Invoke(input5)).Throws(() => new ArgumentException(message5));
                    mock.Setup(x => x.Invoke(input6)).Throws(() => new Exception(message6));
                },
                testPhase: (innerConverter, wrappedConverter) => {
                    wrappedConverter.Convert(input1, null).Should().BeSameAs(output1);
                    wrappedConverter.Convert(input2, null).Should().BeSameAs(output2);
                    wrappedConverter.Convert(input3, null).Should().BeSameAs(output3);
                    wrappedConverter.Convert(input4, null).Should().BeSameAs(output4);
                    wrappedConverter.Invoking(x => x.Convert(input5, null)).Should().ThrowExactly<ArgumentException>().WithMessage(message5);
                    wrappedConverter.Invoking(x => x.Convert(input6, null)).Should().ThrowExactly<Exception>().WithMessage(message6);

                    var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);

                    wrappedConverter.Convert(input1, mockProvider.Object).Should().BeSameAs(output1);
                    wrappedConverter.Convert(input2, mockProvider.Object).Should().BeSameAs(output2);
                    wrappedConverter.Convert(input3, mockProvider.Object).Should().BeSameAs(output3);
                    wrappedConverter.Convert(input4, mockProvider.Object).Should().BeSameAs(output4);
                    wrappedConverter.Invoking(x => x.Convert(input5, mockProvider.Object)).Should().ThrowExactly<ArgumentException>().WithMessage(message5);
                    wrappedConverter.Invoking(x => x.Convert(input6, mockProvider.Object)).Should().ThrowExactly<Exception>().WithMessage(message6);

                    mockProvider.VerifyAll();
                });
        }
    }
}