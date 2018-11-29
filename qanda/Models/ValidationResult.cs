using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Qanda.Api.Models
{
    public class ValidationResult
    {
        public ValidationResult() {
            Errors = new Dictionary<string, string>();
        }
        public bool Valid { get { return !Errors.Any(); } }
        public Dictionary<string, string> Errors { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this.Errors);
        }
    }
}
