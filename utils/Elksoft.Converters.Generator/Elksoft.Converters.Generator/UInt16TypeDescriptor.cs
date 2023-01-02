namespace Elksoft.Converters.Generator
{
    internal sealed class UInt16TypeDescriptor : TypeDescriptor<UInt16>, ITypeDescriptor
    {
        public override Boolean IsSigned => false;
        public override Byte Precision => 16;
        public override String ZeroLiteral => "(UInt16)0";
        public override String OneLiteral => "(UInt16)1";
        public override IEnumerable<Constant<UInt16>> Constants
        {
            get
            {
                yield return new Constant<UInt16>(0, "(UInt16)0");
                yield return new Constant<UInt16>(1, "(UInt16)1");
                yield return new Constant<UInt16>((UInt16)SByte.MaxValue, "(UInt16)SByte.MaxValue");
                yield return new Constant<UInt16>(Byte.MaxValue, "(UInt16)Byte.MaxValue");
                yield return new Constant<UInt16>((UInt16)Int16.MaxValue, "(UInt16)Int16.MaxValue");
                yield return new Constant<UInt16>(UInt16.MaxValue, "UInt16.MaxValue");
            }
        }
    }
}