using System;
using System.Collections.Generic;
using System.Linq;

namespace Qanda.Repository.Models
{
    public class QA
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Host { get; set; }
        public DateTime CreatedAt { get; internal set; }

        public Domain.Models.QA ToDomain(IEnumerable<Question> questions = null)
        {
            return new Domain.Models.QA
            {
                Id = this.Id,
                End = this.End,
                Start = this.Start,
                Host = this.Host,
                Questions = questions != null ? questions.Select(s => s.ToDomain()) : new List<Domain.Models.Question>()
            };
        }
    }
}
