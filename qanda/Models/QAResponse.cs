using Qanda.Domain.Models;
using System;

namespace Qanda.Api.Models
{
    public class QAResponse
    {
        public QAResponse(QA qa)
        {
            Id = qa.Id;
            Start = qa.Start;
            End = qa.End;
            Host = qa.Host;
        }
        public int Id { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public string Host { get; }
    }
}
