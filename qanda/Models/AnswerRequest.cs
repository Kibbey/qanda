namespace Qanda.Api.Models
{
    public class AnswerRequest
    {
        public string Text { get; set; }
        public string AnsweredBy { get; set; }
        public string ImageUrl { get; set; }

        public ValidationResult Validate()
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Text) && string.IsNullOrWhiteSpace(AnsweredBy))
            {
                validationResult.Errors.Add(nameof(Text), "Answer or ImageUrl is required");
            }
            if (string.IsNullOrWhiteSpace(AnsweredBy))
            {
                validationResult.Errors.Add(nameof(AnsweredBy), "AnsweredBy is required");
            }
            return validationResult;
        }
    }
}
