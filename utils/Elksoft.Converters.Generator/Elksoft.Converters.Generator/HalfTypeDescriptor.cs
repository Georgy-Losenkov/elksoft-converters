namespace Elksoft.Converters.Generator
{
    internal sealed class HalfTypeDescriptor : TypeDescriptor<Half>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 11;
        public override Boolean IsFinite => false;
        public override NetVersion MinVersion => NetVersion.Net7;
        public override String ZeroLiteral => "Half.Zero";
        public override String OneLiteral => "Half.One";
        public override IEnumerable<Constant<Half>> Constants
        {
            get
            {
                yield return new Constant<Half>(Half.NaN, "Half.NaN");
                yield return new Constant<Half>(Half.NegativeInfinity, "Half.NegativeInfinity");
                yield return new Constant<Half>(Half.MinValue, "Half.MinValue");
                yield return new Constant<Half>((Half)Int16.MinValue, "(Half)Int16.MinValue");
                yield return new Constant<Half>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<Half>(Half.NegativeOne, "Half.NegativeOne");
                yield return new Constant<Half>(Half.NegativeZero, "Half.NegativeZero");
                yield return new Constant<Half>(Half.Zero, "Half.Zero");
                yield return new Constant<Half>(Half.Epsilon, "Half.Epsilon");
                yield return new Constant<Half>(Half.One, "Half.One");
                yield return new Constant<Half>(Half.E, "Half.E");
                yield return new Constant<Half>(SByte.MaxValue, "SByte.MaxValue");
                yield return new Constant<Half>(Byte.MaxValue, "Byte.MaxValue");
                yield return new Constant<Half>((Half)Int16.MaxValue, "(Half)Int16.MaxValue");
                yield return new Constant<Half>(Half.MaxValue, "Half.MaxValue");
                yield return new Constant<Half>(Half.PositiveInfinity, "Half.PositiveInfinity");
            }
        }
    }
}