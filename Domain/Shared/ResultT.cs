namespace ChatApplication.Domain.Shared
{
    public class Result<TValue> : Result
    {
        private TValue? _value { get; }

        public Result(TValue value, bool success, Error err) : base(success, err)
        {
            _value = value;
        }
        public TValue? Value() { 
            return isSuccess ? _value : throw new Exception("Invalid Operation");
        }

        public static implicit operator Result<TValue>(TValue value) => new(value, true, Error.None);

    }
}
