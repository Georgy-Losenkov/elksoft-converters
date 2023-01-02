namespace Elksoft.Converters.Generator
{
    internal sealed class UIntPtrTypeDescriptor : TypeDescriptor<UIntPtr>, ITypeDescriptor
    {
        public override Boolean IsSigned => false;
        public override Byte Precision => 64;
        public override NetVersion MinVersion => NetVersion.Net7;

        public override Object MinValueX86 { get { return UIntPtr.Zero; } }
        public override Object MaxValueX86 { get { return new UIntPtr(UInt32.MaxValue); } }

        public override Object MinValueX64 { get { return UIntPtr.Zero; } }
        public override Object MaxValueX64 { get { return new UIntPtr(UInt64.MaxValue); } }

        public override String ZeroLiteral => "UIntPtr.Zero";
        public override String OneLiteral => "new UIntPtr(1)";
        public override IEnumerable<Constant<UIntPtr>> Constants
        {
            get
            {
                yield return new Constant<UIntPtr>(UIntPtr.Zero, "UIntPtr.Zero");
                yield return new Constant<UIntPtr>(new UIntPtr(1), "new UIntPtr(1)");
                yield return new Constant<UIntPtr>(new UIntPtr(127), "new UIntPtr(127)");
                yield return new Constant<UIntPtr>(new UIntPtr(Byte.MaxValue), "new UIntPtr(Byte.MaxValue)");
                yield return new Constant<UIntPtr>(new UIntPtr(32767), "new UIntPtr(32767)");
                yield return new Constant<UIntPtr>(new UIntPtr(UInt16.MaxValue), "new UIntPtr(UInt16.MaxValue)");
                yield return new Constant<UIntPtr>(new UIntPtr(Int32.MaxValue), "new UIntPtr(Int32.MaxValue)");
                yield return new Constant<UIntPtr>(new UIntPtr(UInt32.MaxValue), "new UIntPtr(UInt32.MaxValue)");
                yield return new Constant<UIntPtr>(new UIntPtr(Int64.MaxValue), "new UIntPtr(Int64.MaxValue)") { X64Only = true };
                yield return new Constant<UIntPtr>(new UIntPtr(UInt64.MaxValue), "new UIntPtr(UInt64.MaxValue)") { X64Only = true };
            }
        }
    }
}