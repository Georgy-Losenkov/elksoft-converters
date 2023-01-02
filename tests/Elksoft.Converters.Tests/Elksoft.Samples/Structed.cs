namespace Elksoft.Samples
{
    public readonly struct Structed<T> : IBoxed<T>
        where T : struct
    {
        public Structed(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}