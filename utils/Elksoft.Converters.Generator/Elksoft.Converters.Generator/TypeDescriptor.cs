using System.Numerics;

namespace Elksoft.Converters.Generator
{
    //private class NumericTypeInfo
    //{
    //    public Byte Exponent { get; init; }
    //    public Byte ExponentSize { get; init; }
    //}



    internal interface ITypeDescriptor
    {
        Boolean IsFinite { get; }
        NetVersion MinVersion { get; }
        Type Type { get; }
        String TypeName { get; }
        String MinValueLiteral { get; }
        String MaxValueLiteral { get; }
        String ZeroLiteral { get; }
        String OneLiteral { get; }

        Boolean IsSigned { get; }
        Byte Precision { get; }

        Object MinValueX86 { get; }
        Object MaxValueX86 { get; }

        Object MinValueX64 { get; }
        Object MaxValueX64 { get; }

        IEnumerable<IConstant> Constants { get; }
    }

    internal abstract class TypeDescriptor<T> : ITypeDescriptor
        where T : IMinMaxValue<T>, INumber<T>
    {
        protected TypeDescriptor() { }

        public Type Type { get { return typeof(T); } }
        public String TypeName { get { return typeof(T).Name; } }

        public virtual Boolean IsFinite { get { return true; } }
        public virtual NetVersion MinVersion { get { return NetVersion.NetStandard; } }
        public abstract String ZeroLiteral { get; }
        public abstract String OneLiteral { get; }
        public abstract Boolean IsSigned { get; }
        public abstract Byte Precision { get; }

        public abstract IEnumerable<Constant<T>> Constants { get; }

        public String MinValueLiteral { get { return $"{typeof(T).Name}.MinValue"; } }
        public String MaxValueLiteral { get { return $"{typeof(T).Name}.MaxValue"; } }

        public virtual Object MinValueX86 { get { return T.MinValue; } }
        public virtual Object MaxValueX86 { get { return T.MaxValue; } }

        public virtual Object MinValueX64 { get { return T.MinValue; } }
        public virtual Object MaxValueX64 { get { return T.MaxValue; } }

        IEnumerable<IConstant> ITypeDescriptor.Constants { get { return Constants; } }
    }
}