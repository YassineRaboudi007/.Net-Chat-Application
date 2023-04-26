namespace ChatApplication.Domain.Shared
{
    public interface IValidationResult
    {
        public static readonly Error ValdiationError = new(
            "400",
            "A Validation problem occured"
        );

        Error[] errors { get; }
    }
}
