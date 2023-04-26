namespace ChatApplication.Domain.Shared
{
    public class ValidationResult<T>:Result<T>,IValidationResult
    {
        public ValidationResult(Error[] errors) : base(default,false, IValidationResult.ValdiationError)
        {
            this.errors = errors;
        }

        public Error[] errors { get; }

        public static ValidationResult<T> WithErrors(Error[] errors) => new ValidationResult<T>(errors);
    }
}
