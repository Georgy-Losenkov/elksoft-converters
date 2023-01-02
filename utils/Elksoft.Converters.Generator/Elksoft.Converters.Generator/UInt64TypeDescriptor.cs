namespace Elksoft.Converters.Generator
{
    internal sealed class UInt64TypeDescriptor : TypeDescriptor<UInt64>, ITypeDescriptor
    {
        public override Boolean IsSigned => false;
        public override Byte Precision => 64;
        public override String ZeroLiteral => "0UL";
        public override String OneLiteral => "1UL";
        public override IEnumerable<Constant<UInt64>> Constants
        {
            get
            {
                yield return new Constant<UInt64>(0UL, "0UL");
                yield return new Constant<UInt64>(1UL, "1UL");
                yield return new Constant<UInt64>((UInt64)SByte.MaxValue, "(UInt64)SByte.MaxValue");
                yield return new Constant<UInt64>(Byte.MaxValue, "(UInt64)Byte.MaxValue");
                yield return new Constant<UInt64>((UInt64)Int16.MaxValue, "(UInt64)Int16.MaxValue");
                yield return new Constant<UInt64>(UInt16.MaxValue, "(UInt64)UInt16.MaxValue");
                yield return new Constant<UInt64>(Int32.MaxValue, "(UInt64)Int32.MaxValue");
                yield return new Constant<UInt64>(UInt32.MaxValue, "(UInt64)UInt32.MaxValue");
                yield return new Constant<UInt64>(Int64.MaxValue, "(UInt64)Int64.MaxValue");
                yield return new Constant<UInt64>(UInt64.MaxValue, "UInt64.MaxValue");
            }
        }
    }
}