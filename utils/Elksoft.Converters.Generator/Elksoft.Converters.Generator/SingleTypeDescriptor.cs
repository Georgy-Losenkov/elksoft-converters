namespace Elksoft.Converters.Generator
{
    internal sealed class SingleTypeDescriptor : TypeDescriptor<Single>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 23;
        public override Boolean IsFinite => false;
        public override String ZeroLiteral => "0.0f";
        public override String OneLiteral => "1.0f";
        public override IEnumerable<Constant<Single>> Constants
        {
            get
            {
                yield return new Constant<Single>(Single.NaN, "Single.NaN");
                yield return new Constant<Single>(Single.NegativeInfinity, "Single.NegativeInfinity");
                yield return new Constant<Single>(Single.MinValue, "Single.MinValue");
                yield return new Constant<Single>((Single)Half.MinValue, "(Single)Half.MinValue") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Single>((Single)Int128.MinValue, "(Single)Int128.MinValue") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Single>(Int64.MinValue, "Int64.MinValue");
                yield return new Constant<Single>(Int32.MinValue, "Int32.MinValue");
                yield return new Constant<Single>(Int16.MinValue, "Int16.MinValue");
                yield return new Constant<Single>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<Single>(-1.0f, "-1.0f");
                yield return new Constant<Single>(Single.NegativeZero, "Single.NegativeZero") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Single>(0.0f, "0.0f");
                yield return new Constant<Single>((Single)Half.Epsilon, "(Single)Half.Epsilon") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Single>(Single.Epsilon, "Single.Epsilon");
                yield return new Constant<Single>(1.0f, "1.0f");
                yield return new Constant<Single>((Single)Half.E, "(Single)Half.E") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Single>(Single.E, "Single.E") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Single>(SByte.MaxValue, "SByte.MaxValue");
                yield return new Constant<Single>(Byte.MaxValue, "Byte.MaxValue");
                yield return new Constant<Single>(Int16.MaxValue, "Int16.MaxValue");
                yield return new Constant<Single>(UInt16.MaxValue, "UInt16.MaxValue");
                yield return new Constant<Single>(Int32.MaxValue, "Int32.MaxValue");
                yield return new Constant<Single>(UInt32.MaxValue, "UInt32.MaxValue");
                yield return new Constant<Single>(Int64.MaxValue, "Int64.MaxValue");
                yield return new Constant<Single>(UInt64.MaxValue, "UInt64.MaxValue");
                yield return new Constant<Single>((Single)Int128.MaxValue, "(Single)Int128.MaxValue") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Single>((Single)Half.MaxValue, "(Single)Half.MaxValue") { MinVersion = NetVersion.Net7 };
                yield return new Constant<Single>(Single.MaxValue, "Single.MaxValue");
                yield return new Constant<Single>(Single.PositiveInfinity, "Single.PositiveInfinity");
            }
        }
    }
}