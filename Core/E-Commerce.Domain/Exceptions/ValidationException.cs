namespace E_Commerce.Domain.Exceptions
{
    public sealed class ValidationException : Exception
    {
        public IEnumerable<string> Errors { get; set; } = [];
        public ValidationException(IEnumerable<string> errors) : base("One Or More Validation Errors Occurred.")
        {
            Errors = errors;
        }
    }
}
