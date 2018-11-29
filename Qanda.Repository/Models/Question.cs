using System;

namespace Qanda.Repository.Models
{
    public class Question
    {
        public Question()
        { }

        public Question(int qaId, Domain.Models.Question question)
        {
            Id = question.Id;
            AskedBy = question.AskedBy;
            Text = question.Text;
            ImageUrl = question.ImageUrl;
            QAId = qaId;
        }

        public int Id { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string AskedBy { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public int QAId { get; set; }
        public string Answer { get; set; }

        public Domain.Models.Question ToDomain()
        {
            return new Domain.Models.Question
            {
                Id = this.Id,
                ImageUrl = this.ImageUrl,
                Answer = this.Answer,
                Text = this.Text,
                AskedBy = this.AskedBy,
                Created = this.Created,
                Updated = this.Updated
            };
        }
    }
}
