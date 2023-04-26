namespace ChatApplication.Domain.Shared
{
    public class ValidationResult : Result, IValidationResult
    {
        public ValidationResult(Error[] errors): base(false,IValidationResult.ValdiationError)
        {
            this.errors = errors;
        }

        public Error[] errors {get;}

        public static ValidationResult WithErrors(Error[] errors) => new ValidationResult(errors);
    }
}
