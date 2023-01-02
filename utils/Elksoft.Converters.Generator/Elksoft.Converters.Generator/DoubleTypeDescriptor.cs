namespace Elksoft.Converters.Generator
{
    internal sealed class DoubleTypeDescriptor : TypeDescriptor<Double>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 53;
        public override Boolean IsFinite => false;
        public override String ZeroLiteral => "0.0";
        public override String OneLiteral => "1.0";
        public override IEnumerable<Constant<Double>> Constants
        {
            get
            {
                yield return new Constant<Double>(Double.NaN, "Double.NaN");
                yield return new Constant<Double>(Double.NegativeInfinity, "Double.NegativeInfinity");
                yield return new Constant<Double>(Double.MinValue, "Double.MinValue");
                yield return new Constant<Double>((Double)Int128.MinValue, "(Double)Int128.MinValue") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>(Int64.MinValue, "Int64.MinValue");
                yield return new Constant<Double>(Int32.MinValue, "Int32.MinValue");
                yield return new Constant<Double>(Int16.MinValue, "Int16.MinValue");
                yield return new Constant<Double>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<Double>(-1.0, "-1.0");
                // yield return new Constant<Double>(Double.NegativeZero, "Double.NegativeZero") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>(0.0, "0.0");
                yield return new Constant<Double>(Double.Epsilon, "Double.Epsilon");
                yield return new Constant<Double>(Single.Epsilon, "Single.Epsilon");
                yield return new Constant<Double>((Double)Half.Epsilon, "(Double)Half.Epsilon") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>(1.0, "1.0");
                yield return new Constant<Double>((Double)Half.E, "(Double)Half.E") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>(Single.E, "Single.E") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>(Double.E, "Double.E") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>(SByte.MaxValue, "SByte.MaxValue");
                yield return new Constant<Double>(Byte.MaxValue, "Byte.MaxValue");
                yield return new Constant<Double>(Int16.MaxValue, "Int16.MaxValue");
                yield return new Constant<Double>(UInt16.MaxValue, "UInt16.MaxValue");
                yield return new Constant<Double>(Int32.MaxValue, "Int32.MaxValue");
                yield return new Constant<Double>(UInt32.MaxValue, "UInt32.MaxValue");
                yield return new Constant<Double>(Int64.MaxValue, "Int64.MaxValue");
                yield return new Constant<Double>(UInt64.MaxValue, "UInt64.MaxValue");
                yield return new Constant<Double>((Double)Int128.MaxValue, "(Double)Int128.MaxValue") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>((Double)UInt128.MaxValue, "(Double)UInt128.MaxValue") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>((Double)Half.MaxValue, "(Double)Half.MaxValue") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Double>(Single.MaxValue, "Single.MaxValue");
                yield return new Constant<Double>(Double.MaxValue, "Double.MaxValue");
                yield return new Constant<Double>(Double.PositiveInfinity, "Double.PositiveInfinity");
            }
        }
    }
}