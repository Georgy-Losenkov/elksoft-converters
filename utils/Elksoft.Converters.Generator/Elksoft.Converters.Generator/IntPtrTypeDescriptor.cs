namespace Elksoft.Converters.Generator
{
    internal sealed class IntPtrTypeDescriptor : TypeDescriptor<IntPtr>
    {
        public override Boolean IsSigned => true;
        public override Byte Precision => 64;

        public override NetVersion MinVersion => NetVersion.Net7;

        public override Object MinValueX86 { get { return new IntPtr(Int32.MinValue); } }
        public override Object MaxValueX86 { get { return new IntPtr(Int32.MaxValue); } }

        public override Object MinValueX64 { get { return new IntPtr(Int64.MinValue); } }
        public override Object MaxValueX64 { get { return new IntPtr(Int64.MaxValue); } }

        public override String ZeroLiteral => "IntPtr.Zero";
        public override String OneLiteral => "new IntPtr(1)";
        public override IEnumerable<Constant<IntPtr>> Constants
        {
            get
            {
                yield return new Constant<IntPtr>(new IntPtr(Int64.MinValue), "new IntPtr(Int64.MinValue)") { X64Only = true };
                yield return new Constant<IntPtr>(new IntPtr(Int32.MinValue), "new IntPtr(Int32.MinValue)");
                yield return new Constant<IntPtr>(new IntPtr(Int16.MinValue), "new IntPtr(Int16.MinValue)");
                yield return new Constant<IntPtr>(new IntPtr(SByte.MinValue), "new IntPtr(SByte.MinValue)");
                yield return new Constant<IntPtr>(new IntPtr(-1), "new IntPtr(-1)");
                yield return new Constant<IntPtr>(IntPtr.Zero, "IntPtr.Zero");
                yield return new Constant<IntPtr>(new IntPtr(1), "new IntPtr(1)");
                yield return new Constant<IntPtr>(new IntPtr(SByte.MaxValue), "new IntPtr(SByte.MaxValue)");
                yield return new Constant<IntPtr>(new IntPtr(Byte.MaxValue), "new IntPtr(Byte.MaxValue)");
                yield return new Constant<IntPtr>(new IntPtr(Int16.MaxValue), "new IntPtr(Int16.MaxValue)");
                yield return new Constant<IntPtr>(new IntPtr(UInt16.MaxValue), "new IntPtr(UInt16.MaxValue)");
                yield return new Constant<IntPtr>(new IntPtr(Int32.MaxValue), "new IntPtr(Int32.MaxValue)");
                yield return new Constant<IntPtr>(new IntPtr(UInt32.MaxValue), "new IntPtr(UInt32.MaxValue)") { X64Only = true };
                yield return new Constant<IntPtr>(new IntPtr(Int64.MaxValue), "new IntPtr(Int64.MaxValue)") { X64Only = true };
            }
        }
    }
}