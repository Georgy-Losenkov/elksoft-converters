namespace Elksoft.Converters.Generator
{
    internal sealed class SByteTypeDescriptor : TypeDescriptor<SByte>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 8;
        public override String ZeroLiteral => "(SByte)0";
        public override String OneLiteral => "(SByte)1";
        public override IEnumerable<Constant<SByte>> Constants
        {
            get
            {
                yield return new Constant<SByte>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<SByte>(-1, "(SByte)(-1)");
                yield return new Constant<SByte>(0, "(SByte)0");
                yield return new Constant<SByte>(1, "(SByte)1");
                yield return new Constant<SByte>(SByte.MaxValue, "SByte.MaxValue");
            }
        }
    }
}