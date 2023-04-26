namespace ChatApplication.Domain.Shared
{
    public class Result
    {
        public bool isSuccess { get; }
        public bool isFailure => !isSuccess;
        public Error error { get; }


        public Result(bool isSuccess, Error error)
        {
            this.isSuccess = isSuccess;
            this.error = error;
        }

        public static Result Success() => new(true, Error.None);
        public static Result Failure(Error err) => new(false, err);
        public static Result<TValue> Failure<TValue>(TValue? value,Error err) => new(value,false, err);
        public static Result<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);


    }
}
