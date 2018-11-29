using Qanda.Domain.Interfaces;
using Qanda.Domain.Models;
using Qanda.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Qanda.Repository
{
    public class QARepositoryInMemory : IQARepository
    {
        public QARepositoryInMemory()
        {
            if (qasStorage == null)
            {
                qasId = 1;
                qasStorage = new Dictionary<int, Models.QA>();
            }
            if (questionsStorage == null)
            {
                questionsId = 1;
                questionsStorage = new Dictionary<int, Models.Question>();
            }
        }

        public async Task<int> CreateQA(DateTime start, DateTime end, string host)
        {
            var qa = new Models.QA { End = end, Start = start, Host = host, CreatedAt = DateTime.UtcNow, Id = GetQAId() };
            qasStorage.Add(qa.Id, qa);
            return qa.Id;
        }

        public async Task<QA> GetQA(int qaId)
        {
            if (!qasStorage.ContainsKey(qaId)) throw new NotFoundException($"QA for id {qaId} not found.");
            var qa = qasStorage[qaId];
            var questions = questionsStorage.Where(x => x.Value.QAId == qaId).Select(s => s.Value);
            return qa.ToDomain(questions);
        }

        public async Task<int> CreateQuestion(int qaId, Question question)
        {
            if (!qasStorage.ContainsKey(qaId)) throw new NotFoundException($"QA for id {qaId} not found.");
            question.Id = GetQuestionsId();
            var modelQuestion = new Models.Question(qaId, question);
            modelQuestion.Created = DateTime.UtcNow;
            modelQuestion.Updated = DateTime.UtcNow;
            questionsStorage.Add(question.Id, modelQuestion);

            return question.Id;
        }

        public async Task UpdateQuestion(int questionId, Question question)
        {
            if (!questionsStorage.ContainsKey(questionId)) throw new NotFoundException($"Question id {questionId} not found.");
            var existingQuestion = questionsStorage[questionId];
            existingQuestion.ImageUrl = question.ImageUrl;
            existingQuestion.Answer = question.Answer;
            existingQuestion.Updated = DateTime.UtcNow;
            questionsStorage[questionId] = existingQuestion;
        }


        public async Task<Question> GetQuestion(int questionId)
        {
            if (!questionsStorage.ContainsKey(questionId)) throw new NotFoundException($"Question id {questionId} not found.");
            return questionsStorage[questionId].ToDomain();
        }

        private static Dictionary<int, Models.QA> qasStorage { get; set; }
        private int GetQAId()
        {
            return qasId++;
        }
        private static int qasId { get; set; }
        private static Dictionary<int, Models.Question> questionsStorage { get; set; }
        private int GetQuestionsId()
        {
            return questionsId++;
        }
        private static int questionsId { get; set; }
    }
}
