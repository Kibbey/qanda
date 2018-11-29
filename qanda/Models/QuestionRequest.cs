using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Qanda.Api.Models
{
    public struct QuestionRequest
    {
        public string Text { get; set; }
        public string AskedBy { get; set; }

        public ValidationResult Validate()
        {
            var validationResult = new ValidationResult();
            if (string.IsNullOrWhiteSpace(Text))
            {
                validationResult.Errors.Add(nameof(Text), "Text is required");
            }
            if (string.IsNullOrWhiteSpace(AskedBy))
            {
                validationResult.Errors.Add(nameof(AskedBy), "Requestor is required");
            }
            return validationResult;
        }
    }
}
