using System;
using System.Collections.Generic;

namespace Qanda.Domain.Models
{
    public class QA
    {
        public QA() {
            Questions = new List<Question>();
        }

        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Host { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}
