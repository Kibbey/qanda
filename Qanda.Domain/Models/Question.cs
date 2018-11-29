using System;

namespace Qanda.Domain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string AskedBy { get; set; }
        public string Text { get; set; }
        public string Answer { get; set; }
        public string ImageUrl { get; set; }
    }
}
