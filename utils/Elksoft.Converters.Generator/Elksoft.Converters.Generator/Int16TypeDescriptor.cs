namespace Elksoft.Converters.Generator
{
    internal sealed class Int16TypeDescriptor : TypeDescriptor<Int16>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 16;
        public override String ZeroLiteral => "(Int16)0";
        public override String OneLiteral => "(Int16)1";
        public override IEnumerable<Constant<Int16>> Constants
        {
            get
            {
                yield return new Constant<Int16>(Int16.MinValue, "Int16.MinValue");
                yield return new Constant<Int16>(SByte.MinValue, "SByte.MinValue");
                yield return new Constant<Int16>(-1, "-1");
                yield return new Constant<Int16>(0, "(Int16)0");
                yield return new Constant<Int16>(1, "(Int16)1");
                yield return new Constant<Int16>(SByte.MaxValue, "SByte.MaxValue");
                yield return new Constant<Int16>(Byte.MaxValue, "Byte.MaxValue");
                yield return new Constant<Int16>(Int16.MaxValue, "Int16.MaxValue");
            }
        }
    }
}