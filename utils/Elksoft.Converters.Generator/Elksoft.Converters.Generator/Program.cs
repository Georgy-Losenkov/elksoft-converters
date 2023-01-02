using System;
using System.Linq;
using System.Numerics;
using System.Reflection;
using Microsoft.VisualBasic;

namespace Elksoft.Converters.Generator
{
    internal record struct ValidSample(String Input, String Output, NetVersion MinVersion);

    internal record struct InvalidSample(String Input, NetVersion MinVersion);

    internal class CodeGenerator
    {
        private String? m_className;

        public String? Namespace { get; init; }
        public String? InTypeName { get; init; }
        public String? InTypeSuffix { get; init; }
        public String? OutTypeName { get; init; }
        public String? OutTypeSuffix { get; init; }
        public Boolean IsExplicit { get; init; }
        public NetVersion MinVersion { get; init; }
        public String ClassName
        {
            get { return m_className ?? $"{InTypeName}To{OutTypeName}Converter"; }
            init { m_className = value; }
        }

        public IEnumerable<String> GenerateConverter(IEnumerable<String> convertBody)
        {
            var lines = new List<String>(32) {
                $"// <copyright file=\"{ClassName}.cs\" company=\"Georgy Losenkov\">",
                @"// Copyright (c) Georgy Losenkov. All rights reserved.",
                @"// </copyright>",
                @"",
            };

            switch (MinVersion)
            {
                case NetVersion.Net7:
                    lines.Add("#if NET7_0_OR_GREATER");
                    break;
            }

            lines.AddRange(new[] {
                @"using System;",
                @"",
                $"namespace {Namespace}",
                @"{",
                $"    internal sealed class {ClassName} : Converter<{InTypeName}, {OutTypeName}>",
                @"    {",
                @"        public override Boolean AcceptsNull => false;",
                @"",
                $"        public override Boolean IsExplicit => {Program.BooleanLiteral(IsExplicit)};",
                @"",
                $"        public override {OutTypeName}{OutTypeSuffix} Convert({InTypeName}{InTypeSuffix} value, IFormatProvider? formatProvider)",
                @"        {",
            });

            lines.AddRange(convertBody);

            lines.AddRange(new[] {
                @"        }",
                @"    }",
                @"}",
            });

            switch (MinVersion)
            {
                case NetVersion.Net7:
                    lines.Add("#endif");
                    break;
            }

            return lines;
        }

        private IEnumerable<String> Wrap(String line, NetVersion minLineVersion)
        {
            if (minLineVersion > MinVersion)
            {
                switch (minLineVersion)
                {
                    case NetVersion.Net7:
                        yield return "#if NET7_0_OR_GREATER";
                        break;
                }
            }

            yield return line;

            if (minLineVersion > MinVersion)
            {
                switch (minLineVersion)
                {
                    case NetVersion.Net7:
                        yield return "#endif";
                        break;
                }
            }
        }

        public IEnumerable<String> GenerateTests(
            ValidSample[] validSamplesX86,
            ValidSample[] validSamplesX64,
            InvalidSample[] invalidSamplesX86,
            InvalidSample[] invalidSamplesX64)
        {
            var lines = new List<String>(128);

            switch (MinVersion)
            {
                case NetVersion.Net7:
                    lines.Add("#if NET7_0_OR_GREATER");
                    break;
            }

            lines.AddRange(new[] {
                @"using System;",
                @"using FluentAssertions;",
                @"using Moq;",
                @"using Xunit;",
                @"",
                $"namespace {Namespace}",
                @"{",
                $"    public class {ClassName}Tests",
                @"    {",
                @"        [Fact]",
                @"        public static void AcceptsNull_IsFalse()",
                @"        {",
                $"            var converter = new {ClassName}();",
                @"            converter.AcceptsNull.Should().BeFalse();",
                @"        }",
                @"",
                @"        [Fact]",
                $"        public static void IsExplicit_Is{IsExplicit}()",
                @"        {",
                $"            var converter = new {ClassName}();",
                $"            converter.IsExplicit.Should().Be{IsExplicit}();",
                @"        }",
                @"",
                $"        public static TheoryData<{InTypeName}, {OutTypeName}> Convert_ReturnsExpected_Data()",
                @"        {",
            });

            if (validSamplesX86.OrderBy(x => x.Input).ThenBy(x => x.Output).SequenceEqual(validSamplesX64.OrderBy(x => x.Input).ThenBy(x => x.Output)))
            {
                lines.AddRange(new[] {
                    $"            return new TheoryData<{InTypeName}, {OutTypeName}>() {{",
                });
                lines.AddRange(validSamplesX86
                    .SelectMany(x => Wrap($"                {{ {x.Input}, {x.Output} }},", x.MinVersion))
                );
                lines.AddRange(new[] {
                    @"            };",
                });
            }
            else
            {
                lines.AddRange(new[] {
                    @"            if (IntPtr.Size == 4)",
                    @"            {",
                    $"                return new TheoryData<{InTypeName}, {OutTypeName}>() {{",
                });
                lines.AddRange(validSamplesX86
                    .SelectMany(x => Wrap($"                    {{ {x.Input}, {x.Output} }},", x.MinVersion))
                );
                lines.AddRange(new[] {
                    @"                };",
                    @"            }",
                    @"            else",
                    @"            {",
                    $"                return new TheoryData<{InTypeName}, {OutTypeName}>() {{",
                });
                lines.AddRange(validSamplesX64
                    .SelectMany(x => Wrap($"                    {{ {x.Input}, {x.Output} }},", x.MinVersion))
                );
                lines.AddRange(new[] {
                    @"                };",
                    @"            }",
                });
            }

            lines.AddRange(new[] {
                @"        }",
                @"",
                @"        [Theory]",
                @"        [MemberData(nameof(Convert_ReturnsExpected_Data))]",
                $"        public static void Convert_ReturnsExpected({InTypeName} input, {OutTypeName} output)",
                @"        {",
                @"            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);",
                @"",
                $"            var converter = new {ClassName}();",
                @"            converter.Convert(input, null).Should().Be(output);",
                @"            converter.Convert(input, mockProvider.Object).Should().Be(output);",
                @"",
                @"            mockProvider.VerifyAll();",
                @"        }",
            });

            if (IsExplicit)
            {
                if (invalidSamplesX86.Any() && invalidSamplesX64.Any())
                {
                    lines.AddRange(new[] {
                        @"",
                        $"        public static TheoryData<{InTypeName}> Convert_ThrowsException_Data()",
                        @"        {",
                    });

                    if (invalidSamplesX86.OrderBy(x => x.Input).SequenceEqual(invalidSamplesX64.OrderBy(x => x.Input)))
                    {
                        lines.AddRange(new[] {
                            $"            return new TheoryData<{InTypeName}>() {{",
                        });
                        lines.AddRange(invalidSamplesX86
                            .SelectMany(x => Wrap($"                {{ {x.Input} }},", x.MinVersion))
                        );
                        lines.AddRange(new[] {
                            @"            };",
                        });
                    }
                    else
                    {
                        lines.AddRange(new[] {
                            @"            if (IntPtr.Size == 4)",
                            @"            {",
                            $"                return new TheoryData<{InTypeName}>() {{",
                        });
                        lines.AddRange(invalidSamplesX86
                            .SelectMany(x => Wrap($"                    {{ {x.Input} }},", x.MinVersion))
                        );
                        lines.AddRange(new[] {
                            @"                };",
                            @"            }",
                            @"            else",
                            @"            {",
                            $"                return new TheoryData<{InTypeName}>() {{",
                        });
                        lines.AddRange(invalidSamplesX64
                            .SelectMany(x => Wrap($"                    {{ {x.Input} }},", x.MinVersion))
                        );
                        lines.AddRange(new[] {
                            @"                };",
                            @"            }",
                        });
                    }

                    lines.AddRange(new[] {
                        @"        }",
                        @"",
                        @"        [Theory]",
                        @"        [MemberData(nameof(Convert_ThrowsException_Data))]",
                        $"        public static void Convert_ThrowsException({InTypeName} input)",
                        @"        {",
                        @"            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);",
                        @"",
                        $"            var converter = new {ClassName}();",
                        @"            converter.Invoking(x => x.Convert(input, null)).Should().Throw<Exception>();",
                        @"            converter.Invoking(x => x.Convert(input, mockProvider.Object)).Should().Throw<Exception>();",
                        @"",
                        @"            mockProvider.VerifyAll();",
                        @"        }",
                    });
                }
                else
                {
                    if (invalidSamplesX86.Any())
                    {
                        lines.AddRange(new[] {
                            @"",
                            @"        [Fact]",
                            @"        public static void Convert_X86_ThrowsException()",
                            @"        {",
                            @"            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);",
                            @"",
                            @"            if (IntPtr.Size == 4)",
                            @"            {",
                            $"                var converter = new {ClassName}();",
                        });
                        lines.AddRange(invalidSamplesX86
                            .SelectMany(x => Wrap($"                converter.Invoking(x => x.Convert({x.Input}, null)).Should().Throw<Exception>();", x.MinVersion))
                        );
                        lines.AddRange(invalidSamplesX86
                            .SelectMany(x => Wrap($"                converter.Invoking(x => x.Convert({x.Input}, mockProvider.Object)).Should().Throw<Exception>();", x.MinVersion))
                        );
                        lines.AddRange(new[] {
                            @"            }",
                            @"",
                            @"            mockProvider.VerifyAll();",
                            @"        }",
                        });
                    }
                    if (invalidSamplesX64.Any())
                    {
                        lines.AddRange(new[] {
                            @"",
                            @"        [Fact]",
                            @"        public static void Convert_X64_ThrowsException()",
                            @"        {",
                            @"            var mockProvider = new Mock<IFormatProvider>(MockBehavior.Strict);",
                            @"",
                            @"            if (IntPtr.Size == 8)",
                            @"            {",
                            $"                var converter = new {ClassName}();",
                        });
                        lines.AddRange(invalidSamplesX64
                            .SelectMany(x => Wrap($"                converter.Invoking(x => x.Convert({x.Input}, null)).Should().Throw<Exception>();", x.MinVersion))
                        );
                        lines.AddRange(invalidSamplesX64
                            .SelectMany(x => Wrap($"                converter.Invoking(x => x.Convert({x.Input}, mockProvider.Object)).Should().Throw<Exception>();", x.MinVersion))
                        );
                        lines.AddRange(new[] {
                            @"            }",
                            @"",
                            @"            mockProvider.VerifyAll();",
                            @"        }",
                        });
                    }
                }
            }

            lines.AddRange(new[] {
                @"    }",
                @"}",
            });

            switch (MinVersion)
            {
                case NetVersion.Net7:
                    lines.Add("#endif");
                    break;
            }

            return lines;
        }
    }

    internal class Program
    {
        internal static String BooleanLiteral(Boolean value)
        {
            return value ? "true" : "false";
        }

        static Int32 RangeFlags<TIn, TOut>(TIn minInValue, TIn maxInValue, TOut minOutValue, TOut maxOutValue)
            where TIn : struct, INumber<TIn>
            where TOut : struct, INumber<TOut>
        {
            var flags = 0;

            try
            {
                var tmp = TOut.CreateChecked(minInValue);
                if (tmp < minOutValue)
                {
                    flags |= 1;
                }
            }
            catch
            {
                flags |= 1;
            }

            try
            {
                var tmp = TOut.CreateChecked(maxInValue);
                if (maxOutValue < tmp)
                {
                    flags |= 2;
                }
            }
            catch
            {
                flags |= 2;
            }

            try
            {
                var tmp = TIn.CreateChecked(minOutValue);
                if (minInValue < tmp)
                {
                    flags |= 1;
                }
            }
            catch { }

            try
            {
                var tmp = TIn.CreateChecked(maxOutValue);
                if (tmp < maxInValue)
                {
                    flags |= 2;
                }
            }
            catch { }

            return flags;
        }

        static TOut? ConvertChecked<TIn, TOut>(TIn value)
            where TIn : struct, INumber<TIn>
            where TOut : struct, INumber<TOut>
        {
            try
            {
                return TOut.CreateChecked(value);
            }
            catch (Exception)
            {
                return null;
            }
        }

        static TOut? ConvertCheckedRanged<TIn, TOut>(TIn value, TOut minOutValue, TOut maxOutValue)
            where TIn : struct, INumber<TIn>
            where TOut : struct, INumber<TOut>
        {
            try
            {
                var result = TOut.CreateChecked(value);

                if (minOutValue <= result && result <= maxOutValue)
                {
                    return result;
                }
            }
            catch { }

            return null;
        }

        static Boolean IsInRange<TIn>(TIn value, TIn minValue, TIn maxValue)
            where TIn : struct, INumber<TIn>
        {
            return minValue <= value && value <= maxValue;
        }

        const String BooleanConvertersNamespace = "Elksoft.Converters.BooleanConverters";
        const String CheckedNumericConvertersNamespace = "Elksoft.Converters.CheckedNumericConverters";
        const String ImplicitNumericConvertersNamespace = "Elksoft.Converters.ImplicitNumericConverters";
        const String UncheckedNumericConvertersNamespace = "Elksoft.Converters.UncheckedNumericConverters";
        const String StringConvertersNamespace = "Elksoft.Converters.StringConverters";

        static async Task GenerateStringConverterFactoriesAsync(String directory)
        {
            Directory.CreateDirectory(Path.Combine(directory, StringConvertersNamespace));

            var cultureSpecific = s_numericTypes
                .Where(d => d.TypeName != "Char")
                .Select(d => new { d.TypeName, d.MinVersion })
                .Append(new { TypeName = "DateOnly", MinVersion = NetVersion.NetStandard })
                .Append(new { TypeName = "DateTime", MinVersion = NetVersion.NetStandard })
                .Append(new { TypeName = "DateTimeOffset", MinVersion = NetVersion.NetStandard })
                .Append(new { TypeName = "TimeOnly", MinVersion = NetVersion.NetStandard })
                .Append(new { TypeName = "TimeSpan", MinVersion = NetVersion.NetStandard })
                ;

            var cultureInvariant = new[] {
                new { TypeName = "Boolean", MinVersion = NetVersion.NetStandard },
                new { TypeName = "Char", MinVersion = NetVersion.NetStandard },
                new { TypeName = "Guid", MinVersion = NetVersion.NetStandard },
            };

            foreach (var descriptor in cultureSpecific)
            {
                var g = new CodeGenerator {
                    Namespace = StringConvertersNamespace,
                    InTypeName = "String",
                    InTypeSuffix = "?",
                    OutTypeName = descriptor.TypeName,
                    IsExplicit = true,
                    MinVersion = descriptor.MinVersion,
                };

                var convertBody = new[] {
                    $"            return {descriptor.TypeName}.Parse(",
                    @"                Check.NotNull(value, nameof(value)),",
                    @"                formatProvider);",
                };

                var path = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                await File.WriteAllLinesAsync(path, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);
            }

            foreach (var descriptor in cultureInvariant)
            {
                var g = new CodeGenerator {
                    Namespace = StringConvertersNamespace,
                    InTypeName = "String",
                    InTypeSuffix = "?",
                    OutTypeName = descriptor.TypeName,
                    IsExplicit = true,
                    MinVersion = descriptor.MinVersion,
                };

                var convertBody = new[] {
                    $"            return {descriptor.TypeName}.Parse(",
                    @"                Check.NotNull(value, nameof(value)));",
                };

                var path = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                await File.WriteAllLinesAsync(path, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);
            }

            foreach (var descriptor in cultureSpecific)
            {
                var g = new CodeGenerator {
                    Namespace = StringConvertersNamespace,
                    InTypeName = descriptor.TypeName,
                    OutTypeName = "String",
                    OutTypeSuffix = "?",
                    IsExplicit = true,
                    MinVersion = descriptor.MinVersion,
                };

                var convertBody = new[] {
                    $"            return value.ToString(null, formatProvider);"
                };

                var path = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                await File.WriteAllLinesAsync(path, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);
            }

            foreach (var descriptor in cultureInvariant)
            {
                var g = new CodeGenerator {
                    Namespace = StringConvertersNamespace,
                    InTypeName = descriptor.TypeName,
                    OutTypeName = "String",
                    OutTypeSuffix = "?",
                    IsExplicit = true,
                    MinVersion = descriptor.MinVersion,
                };

                var convertBody = new[] {
                    $"            return value.ToString();"
                };

                var path = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                await File.WriteAllLinesAsync(path, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);
            }
        }

        static async Task GenerateBooleanConverterFactoriesAsync(String directory, String testsDirectory)
        {
            Directory.CreateDirectory(Path.Combine(directory, BooleanConvertersNamespace));
            Directory.CreateDirectory(Path.Combine(testsDirectory, BooleanConvertersNamespace));

            var convertMethodInfo = typeof(Program).GetMethod(nameof(ConvertChecked), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!;

            foreach (var descriptor in s_numericTypes)
            {
                var g = new CodeGenerator {
                    Namespace = BooleanConvertersNamespace,
                    InTypeName = "Boolean",
                    OutTypeName = descriptor.TypeName,
                    IsExplicit = false,
                    MinVersion = descriptor.MinVersion,
                };

                var convertBody = new[] {
                    $"            return value ? {descriptor.OneLiteral} : {descriptor.ZeroLiteral};"
                };
                var samples = new[] {
                    new ValidSample("false", descriptor.ZeroLiteral, NetVersion.NetStandard),
                    new ValidSample("true", descriptor.OneLiteral, NetVersion.NetStandard)
                };

                var path1 = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                await File.WriteAllLinesAsync(path1, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);

                var path2 = Path.Combine(testsDirectory, g.Namespace, g.ClassName + "Tests.cs");
                await File.WriteAllLinesAsync(path2, g.GenerateTests(samples, samples, Array.Empty<InvalidSample>(), Array.Empty<InvalidSample>()), System.Text.Encoding.UTF8);
            }

            foreach (var descriptor in s_numericTypes)
            {
                var g = new CodeGenerator {
                    Namespace = BooleanConvertersNamespace,
                    InTypeName = descriptor.TypeName,
                    OutTypeName = "Boolean",
                    IsExplicit = false,
                    MinVersion = descriptor.MinVersion,
                };
                var convertBody = new[] {
                    $"            return value != {descriptor.ZeroLiteral};"
                };
                var zero = convertMethodInfo
                    .MakeGenericMethod(typeof(Int32), descriptor.Type)
                    .Invoke(null, new Object[] { 0 })!;
                var samplesX86 = descriptor.Constants.Where(x => !x.X64Only)
                    .Select(x => new ValidSample(x.Literal, BooleanLiteral(!zero.Equals(x.Value)), x.MinVersion))
                    .ToArray();
                var samplesX64 = descriptor.Constants
                    .Select(x => new ValidSample(x.Literal, BooleanLiteral(!zero.Equals(x.Value)), x.MinVersion))
                    .ToArray();

                var path1 = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                await File.WriteAllLinesAsync(path1, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);

                var path2 = Path.Combine(testsDirectory, g.Namespace, g.ClassName + "Tests.cs");
                await File.WriteAllLinesAsync(path2, g.GenerateTests(samplesX86, samplesX64, Array.Empty<InvalidSample>(), Array.Empty<InvalidSample>()), System.Text.Encoding.UTF8);
            }
        }

        static async Task GenerateCheckedNumericConverterFactoriesAsync(String directory, String testsDirectory)
        {
            Directory.CreateDirectory(Path.Combine(directory, CheckedNumericConvertersNamespace));
            Directory.CreateDirectory(Path.Combine(testsDirectory, CheckedNumericConvertersNamespace));

            var rangeFlagsMethodInfo = typeof(Program).GetMethod(nameof(RangeFlags), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!;

            foreach (var inDescriptor in s_numericTypes)
            {
                foreach (var outDescriptor in s_numericTypes)
                {
                    if (inDescriptor == outDescriptor)
                    {
                        continue;
                    }

                    var rangeFlagsX86 = (Int32)rangeFlagsMethodInfo
                        .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)
                        .Invoke(null, new Object[] { inDescriptor.MinValueX86, inDescriptor.MaxValueX86, outDescriptor.MinValueX86, outDescriptor.MaxValueX86 })!;

                    var rangeFlagsX64 = (Int32)rangeFlagsMethodInfo
                        .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)
                        .Invoke(null, new Object[] { inDescriptor.MinValueX64, inDescriptor.MaxValueX64, outDescriptor.MinValueX64, outDescriptor.MaxValueX64 })!;

                    var isExplicit = outDescriptor.IsFinite && (rangeFlagsX86 | rangeFlagsX64) != 0;
                    if (!isExplicit)
                    {
                        continue;
                    }

                    var g = new CodeGenerator {
                        Namespace = CheckedNumericConvertersNamespace,
                        InTypeName = inDescriptor.TypeName,
                        OutTypeName = outDescriptor.TypeName,
                        ClassName = $"Checked{inDescriptor.TypeName}To{outDescriptor.TypeName}Converter",
                        IsExplicit = true,
                        MinVersion = inDescriptor.MinVersion > outDescriptor.MinVersion ? inDescriptor.MinVersion : outDescriptor.MinVersion,
                    };

                    var convertBody = new[] {
                        $"            return checked(({outDescriptor.TypeName})value);",
                    };

                    var path1 = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                    await File.WriteAllLinesAsync(path1, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);

                    Func<Object, Object?> convert86, convert64;

                    if (outDescriptor.IsFinite)
                    {
                        var methodInfo = typeof(Program)
                            .GetMethod(nameof(ConvertCheckedRanged), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!
                            .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)!;

                        convert86 = x => methodInfo.Invoke(null, new Object[] { x, outDescriptor.MinValueX86, outDescriptor.MaxValueX86 });
                        convert64 = x => methodInfo.Invoke(null, new Object[] { x, outDescriptor.MinValueX64, outDescriptor.MaxValueX64 });
                    }
                    else
                    {
                        var methodInfo = typeof(Program)
                            .GetMethod(nameof(ConvertChecked), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!
                            .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)!;

                        convert86 = x => methodInfo.Invoke(null, new Object[] { x });
                        convert64 = x => methodInfo.Invoke(null, new Object[] { x });
                    }

                    var validSamplesX86 = (
                        from x in inDescriptor.Constants.Where(x => !x.X64Only)
                        let converted = convert86(x.Value)
                        where converted != null
                        let output = outDescriptor.Constants.FirstOrDefault(y => y.Value.Equals(converted))
                        let outputLiteral = output != null ? output.Literal : $"{outDescriptor.TypeName}.Parse(\"{converted}\")"
                        let minVersion = (output != null && output.MinVersion > x.MinVersion) ? output.MinVersion : x.MinVersion
                        select new ValidSample(x.Literal, outputLiteral, minVersion)
                    ).ToArray();

                    var validSamplesX64 = (
                        from x in inDescriptor.Constants
                        let converted = convert64(x.Value)
                        where converted != null
                        let output = outDescriptor.Constants.FirstOrDefault(y => y.Value.Equals(converted))
                        let outputLiteral = output != null ? output.Literal : $"{outDescriptor.TypeName}.Parse(\"{converted}\")"
                        let minVersion = (output != null && output.MinVersion > x.MinVersion) ? output.MinVersion : x.MinVersion
                        select new ValidSample(x.Literal, outputLiteral, minVersion)
                    ).ToArray();

                    InvalidSample[] invalidSamplesX86, invalidSamplesX64;

                    if (outDescriptor.IsFinite)
                    {
                        invalidSamplesX86 = (
                            from x in inDescriptor.Constants.Where(x => !x.X64Only)
                            let converted = convert86(x.Value)
                            where converted == null
                            select new InvalidSample(x.Literal, x.MinVersion)
                        ).ToArray();

                        invalidSamplesX64 = (
                            from x in inDescriptor.Constants
                            let converted = convert64(x.Value)
                            where converted == null
                            select new InvalidSample(x.Literal, x.MinVersion)
                        ).ToArray();
                    }
                    else
                    {
                        invalidSamplesX86 = Array.Empty<InvalidSample>();
                        invalidSamplesX64 = Array.Empty<InvalidSample>();
                    }

                    var path2 = Path.Combine(testsDirectory, g.Namespace, g.ClassName + "Tests.cs");
                    await File.WriteAllLinesAsync(path2, g.GenerateTests(validSamplesX86, validSamplesX64, invalidSamplesX86, invalidSamplesX64), System.Text.Encoding.UTF8);
                }
            }
        }

        static async Task GenerateImplicitNumericConverterFactoriesAsync(String directory, String testsDirectory)
        {
            Directory.CreateDirectory(Path.Combine(directory, ImplicitNumericConvertersNamespace));
            Directory.CreateDirectory(Path.Combine(testsDirectory, ImplicitNumericConvertersNamespace));

            var rangeFlagsMethodInfo = typeof(Program).GetMethod(nameof(RangeFlags), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!;

            foreach (var inDescriptor in s_numericTypes) // .Where(x => x.Type == typeof(Half)))
            {
                foreach (var outDescriptor in s_numericTypes) // .Where(x => x.Type == typeof(UInt16)))
                {
                    if (inDescriptor == outDescriptor)
                    {
                        continue;
                    }

                    var rangeFlagsX86 = (Int32)rangeFlagsMethodInfo
                        .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)
                        .Invoke(null, new Object[] { inDescriptor.MinValueX86, inDescriptor.MaxValueX86, outDescriptor.MinValueX86, outDescriptor.MaxValueX86 })!;

                    var rangeFlagsX64 = (Int32)rangeFlagsMethodInfo
                        .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)
                        .Invoke(null, new Object[] { inDescriptor.MinValueX64, inDescriptor.MaxValueX64, outDescriptor.MinValueX64, outDescriptor.MaxValueX64 })!;

                    var isExplicit = outDescriptor.IsFinite && (rangeFlagsX86 | rangeFlagsX64) != 0;
                    if (isExplicit)
                    {
                        continue;
                    }

                    var g = new CodeGenerator {
                        Namespace = ImplicitNumericConvertersNamespace,
                        InTypeName = inDescriptor.TypeName,
                        OutTypeName = outDescriptor.TypeName,
                        ClassName = $"Implicit{inDescriptor.TypeName}To{outDescriptor.TypeName}Converter",
                        IsExplicit = false,
                        MinVersion = inDescriptor.MinVersion > outDescriptor.MinVersion ? inDescriptor.MinVersion : outDescriptor.MinVersion,
                    };

                    var convertBody = new[] {
                        $"            return unchecked(({outDescriptor.TypeName})value);",
                    };

                    var path1 = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                    await File.WriteAllLinesAsync(path1, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);

                    Func<Object, Object?> convert86, convert64;

                    if (outDescriptor.IsFinite)
                    {
                        var methodInfo = typeof(Program)
                            .GetMethod(nameof(ConvertCheckedRanged), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!
                            .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)!;

                        convert86 = x => methodInfo.Invoke(null, new Object[] { x, outDescriptor.MinValueX86, outDescriptor.MaxValueX86 });
                        convert64 = x => methodInfo.Invoke(null, new Object[] { x, outDescriptor.MinValueX64, outDescriptor.MaxValueX64 });
                    }
                    else
                    {
                        var methodInfo = typeof(Program)
                            .GetMethod(nameof(ConvertChecked), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!
                            .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)!;

                        convert86 = x => methodInfo.Invoke(null, new Object[] { x });
                        convert64 = x => methodInfo.Invoke(null, new Object[] { x });
                    }

                    var validSamplesX86 = (
                        from x in inDescriptor.Constants.Where(x => !x.X64Only)
                        let converted = convert86(x.Value)
                        where converted != null
                        let output = outDescriptor.Constants.FirstOrDefault(y => y.Value.Equals(converted))
                        let outputLiteral = output != null ? output.Literal : $"{outDescriptor.TypeName}.Parse(\"{converted}\")"
                        let minVersion = (output != null && output.MinVersion > x.MinVersion) ? output.MinVersion : x.MinVersion
                        select new ValidSample(x.Literal, outputLiteral, minVersion)
                    ).ToArray();

                    var validSamplesX64 = (
                        from x in inDescriptor.Constants
                        let converted = convert64(x.Value)
                        where converted != null
                        let output = outDescriptor.Constants.FirstOrDefault(y => y.Value.Equals(converted))
                        let outputLiteral = output != null ? output.Literal : $"{outDescriptor.TypeName}.Parse(\"{converted}\")"
                        let minVersion = (output != null && output.MinVersion > x.MinVersion) ? output.MinVersion : x.MinVersion
                        select new ValidSample(x.Literal, outputLiteral, minVersion)
                    ).ToArray();

                    var invalidSamplesX86 = Array.Empty<InvalidSample>();
                    var invalidSamplesX64 = Array.Empty<InvalidSample>();

                    var path2 = Path.Combine(testsDirectory, g.Namespace, g.ClassName + "Tests.cs");
                    await File.WriteAllLinesAsync(path2, g.GenerateTests(validSamplesX86, validSamplesX64, invalidSamplesX86, invalidSamplesX64), System.Text.Encoding.UTF8);
                }
            }
        }

        static async Task GenerateUncheckedNumericConverterFactoriesAsync(String directory, String testsDirectory)
        {
            Directory.CreateDirectory(Path.Combine(directory, UncheckedNumericConvertersNamespace));
            Directory.CreateDirectory(Path.Combine(testsDirectory, UncheckedNumericConvertersNamespace));

            var rangeFlagsMethodInfo = typeof(Program).GetMethod(nameof(RangeFlags), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!;

            foreach (var inDescriptor in s_numericTypes)
            {
                foreach (var outDescriptor in s_numericTypes)
                {
                    if (inDescriptor == outDescriptor)
                    {
                        continue;
                    }

                    var rangeFlagsX86 = (Int32)rangeFlagsMethodInfo
                        .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)
                        .Invoke(null, new Object[] { inDescriptor.MinValueX86, inDescriptor.MaxValueX86, outDescriptor.MinValueX86, outDescriptor.MaxValueX86 })!;

                    var rangeFlagsX64 = (Int32)rangeFlagsMethodInfo
                        .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)
                        .Invoke(null, new Object[] { inDescriptor.MinValueX64, inDescriptor.MaxValueX64, outDescriptor.MinValueX64, outDescriptor.MaxValueX64 })!;

                    var isExplicit = outDescriptor.IsFinite && (rangeFlagsX86 | rangeFlagsX64) != 0;
                    if (!isExplicit)
                    {
                        continue;
                    }

                    var g = new CodeGenerator {
                        Namespace = UncheckedNumericConvertersNamespace,
                        InTypeName = inDescriptor.TypeName,
                        OutTypeName = outDescriptor.TypeName,
                        ClassName = $"Unchecked{inDescriptor.TypeName}To{outDescriptor.TypeName}Converter",
                        IsExplicit = true,
                        MinVersion = inDescriptor.MinVersion > outDescriptor.MinVersion ? inDescriptor.MinVersion : outDescriptor.MinVersion,
                    };

                    var convertBody = new[] {
                        $"            return unchecked(({outDescriptor.TypeName})value);",
                    };

                    var path1 = Path.Combine(directory, g.Namespace, g.ClassName + ".cs");
                    await File.WriteAllLinesAsync(path1, g.GenerateConverter(convertBody), System.Text.Encoding.UTF8);

                    Func<Object, Object?> convert86, convert64;

                    if (outDescriptor.IsFinite)
                    {
                        var methodInfo = typeof(Program)
                            .GetMethod(nameof(ConvertCheckedRanged), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!
                            .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)!;

                        convert86 = x => methodInfo.Invoke(null, new Object[] { x, outDescriptor.MinValueX86, outDescriptor.MaxValueX86 });
                        convert64 = x => methodInfo.Invoke(null, new Object[] { x, outDescriptor.MinValueX64, outDescriptor.MaxValueX64 });
                    }
                    else
                    {
                        var methodInfo = typeof(Program)
                            .GetMethod(nameof(ConvertChecked), BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static)!
                            .MakeGenericMethod(inDescriptor.Type, outDescriptor.Type)!;

                        convert86 = x => methodInfo.Invoke(null, new Object[] { x });
                        convert64 = x => methodInfo.Invoke(null, new Object[] { x });
                    }

                    var validSamplesX86 = (
                        from x in inDescriptor.Constants.Where(x => !x.X64Only)
                        let converted = convert86(x.Value)
                        where converted != null
                        let output = outDescriptor.Constants.FirstOrDefault(y => y.Value.Equals(converted))
                        let outputLiteral = output != null ? output.Literal : $"{outDescriptor.TypeName}.Parse(\"{converted}\")"
                        let minVersion = (output != null && output.MinVersion > x.MinVersion) ? output.MinVersion : x.MinVersion
                        select new ValidSample(x.Literal, outputLiteral, minVersion)
                    ).ToArray();

                    var validSamplesX64 = (
                        from x in inDescriptor.Constants
                        let converted = convert64(x.Value)
                        where converted != null
                        let output = outDescriptor.Constants.FirstOrDefault(y => y.Value.Equals(converted))
                        let outputLiteral = output != null ? output.Literal : $"{outDescriptor.TypeName}.Parse(\"{converted}\")"
                        let minVersion = (output != null && output.MinVersion > x.MinVersion) ? output.MinVersion : x.MinVersion
                        select new ValidSample(x.Literal, outputLiteral, minVersion)
                    ).ToArray();

                    var invalidSamplesX86 = Array.Empty<InvalidSample>();
                    var invalidSamplesX64 = Array.Empty<InvalidSample>();

                    var path2 = Path.Combine(testsDirectory, g.Namespace, g.ClassName + "Tests.cs");
                    await File.WriteAllLinesAsync(path2, g.GenerateTests(validSamplesX86, validSamplesX64, invalidSamplesX86, invalidSamplesX64), System.Text.Encoding.UTF8);
                }
            }
        }

        private static readonly ITypeDescriptor[] s_numericTypes = new ITypeDescriptor[] {
            new CharTypeDescriptor(),
            new DecimalTypeDescriptor(),
            new HalfTypeDescriptor(),
            new SingleTypeDescriptor(),
            new DoubleTypeDescriptor(),
            new ByteTypeDescriptor(),
            new UInt16TypeDescriptor(),
            new UInt32TypeDescriptor(),
            new UInt64TypeDescriptor(),
            new UInt128TypeDescriptor(),
            new UIntPtrTypeDescriptor(),
            new SByteTypeDescriptor(),
            new Int16TypeDescriptor(),
            new Int32TypeDescriptor(),
            new Int64TypeDescriptor(),
            new Int128TypeDescriptor(),
            new IntPtrTypeDescriptor(),
        };

        static async Task Main()
        {
            await GenerateStringConverterFactoriesAsync("C:\\Temp\\Factories\\");
            await GenerateBooleanConverterFactoriesAsync("C:\\Temp\\Factories\\", "C:\\Temp\\FactoryTests\\");
            await GenerateCheckedNumericConverterFactoriesAsync("C:\\Temp\\Factories\\", "C:\\Temp\\FactoryTests\\");
            await GenerateImplicitNumericConverterFactoriesAsync("C:\\Temp\\Factories\\", "C:\\Temp\\FactoryTests\\");
            await GenerateUncheckedNumericConverterFactoriesAsync("C:\\Temp\\Factories\\", "C:\\Temp\\FactoryTests\\");
        }
    }
}