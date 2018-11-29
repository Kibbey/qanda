using Qanda.Domain.Exceptions;
using Qanda.Domain.Interfaces;
using Qanda.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Qanda.Domain
{
    public class QAService : IQAService
    {
        private IQARepository qaRepository { get; }

        public QAService(IQARepository qaRepository)
        {
            this.qaRepository = qaRepository;
        }

        public async Task AnswerQuestion(int id, string answer, string imageUrl)
        {
            var question = await qaRepository.GetQuestion(id);
            question.Answer = answer;
            question.ImageUrl = imageUrl;
            await qaRepository.UpdateQuestion(id, question);
        }

        public async Task<int> CreateQA(DateTime start, DateTime end, string host)
        {
            return await qaRepository.CreateQA(start, end, host);
        }

        public async Task<int> CreateQuestion(int qaId, string text, string requestor)
        {
            return await qaRepository.CreateQuestion(qaId, new Question { AskedBy = requestor, Text = text });
        }

        public async Task<QA> GetQA(int qaId)
        {
            var qa = await qaRepository.GetQA(qaId);
            if (qa == null) throw new NotFoundException($"Q and A with id {qaId} not found.");
            return qa;
        }

        public async Task<Question> GetQuestion(int questionId)
        {
            var question = await qaRepository.GetQuestion(questionId);
            if (question == null) throw new NotFoundException($"Question with id {questionId} not found.");
            return question;
        }
    }
}
