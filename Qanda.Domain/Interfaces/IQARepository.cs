using Qanda.Domain.Models;
using System;
using System.Threading.Tasks;

namespace Qanda.Domain.Interfaces
{
    public interface IQARepository
    {
        Task<int> CreateQA(DateTime start, DateTime end, string host);
        Task<QA> GetQA(int qaId);
        Task<int> CreateQuestion(int qaId, Question question);
        Task<Question> GetQuestion(int questionId);
        Task UpdateQuestion(int questionId, Question question);
    }
}
