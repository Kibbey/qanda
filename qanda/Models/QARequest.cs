using System;

namespace Qanda.Api.Models
{
    public struct QARequest
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Host { get; set; }

        public ValidationResult Validate()
        {
            var validationResult = new ValidationResult();
            if (Start < DateTime.UtcNow)
            {
                validationResult.Errors.Add(nameof(Start), "Start must be a value in the future.");
            }
            if (End < Start)
            {
                validationResult.Errors.Add(nameof(End), "End must be a value greater than Start.");
            }
            if (string.IsNullOrWhiteSpace(Host))
            {
                validationResult.Errors.Add(nameof(Host), "Host is required");
            }
            return validationResult;
        }
    }
}
