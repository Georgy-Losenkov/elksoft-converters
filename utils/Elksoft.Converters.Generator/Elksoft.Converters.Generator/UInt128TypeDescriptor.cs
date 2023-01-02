namespace Elksoft.Converters.Generator
{
    internal sealed class UInt128TypeDescriptor : TypeDescriptor<UInt128>, ITypeDescriptor
    {
        public override Boolean IsSigned => false;
        public override Byte Precision => 128;
        public override NetVersion MinVersion => NetVersion.Net7;
        public override String ZeroLiteral => "UInt128.Zero";
        public override String OneLiteral => "UInt128.One";
        public override IEnumerable<Constant<UInt128>> Constants
        {
            get
            {
                yield return new Constant<UInt128>(UInt128.Zero, "UInt128.Zero");
                yield return new Constant<UInt128>(UInt128.One, "UInt128.One");
                yield return new Constant<UInt128>((UInt128)SByte.MaxValue, "(UInt128)SByte.MaxValue");
                yield return new Constant<UInt128>((UInt128)Byte.MaxValue, "(UInt128)Byte.MaxValue");
                yield return new Constant<UInt128>((UInt128)Int16.MaxValue, "(UInt128)Int16.MaxValue");
                yield return new Constant<UInt128>((UInt128)UInt16.MaxValue, "(UInt128)UInt16.MaxValue");
                yield return new Constant<UInt128>((UInt128)Int32.MaxValue, "(UInt128)Int32.MaxValue");
                yield return new Constant<UInt128>((UInt128)UInt32.MaxValue, "(UInt128)UInt32.MaxValue");
                yield return new Constant<UInt128>((UInt128)Int64.MaxValue, "(UInt128)Int64.MaxValue");
                yield return new Constant<UInt128>((UInt128)UInt64.MaxValue, "(UInt128)UInt64.MaxValue");
                yield return new Constant<UInt128>((UInt128)Int128.MaxValue, "(UInt128)Int128.MaxValue");
                yield return new Constant<UInt128>(UInt128.MaxValue, "UInt128.MaxValue");
            }
        }
    }
}