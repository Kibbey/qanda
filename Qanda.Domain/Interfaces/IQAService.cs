using Qanda.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Qanda.Domain.Interfaces
{
    public interface IQAService
    {
        Task<int> CreateQA(DateTime start, DateTime end, string host);
        Task<QA> GetQA(int id);
        Task<int> CreateQuestion(int qaId, string text, string requestor);
        Task<Question> GetQuestion(int questionId);
        Task AnswerQuestion(int questionId, string answer, string imgUrl);
    }
}
