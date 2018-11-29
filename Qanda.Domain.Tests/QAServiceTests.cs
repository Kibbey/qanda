using Microsoft.VisualStudio.TestTools.UnitTesting;
using Qanda.Domain.Interfaces;
using Qanda.Repository;
using System;
using System.Linq;

namespace Qanda.Domain.Tests
{
    [TestClass]
    public class QAServiceTests
    {
        private IQARepository qaRepository;

        private readonly DateTime start = DateTime.UtcNow;
        private readonly DateTime end = DateTime.UtcNow.AddHours(1);
        private const string host = "test host";
        private const string question1 = "question 1";
        private const string question2 = "question 2";
        private const string requestor1 = "requestor 1";
        private const string requestor2 = "requestor 2";
        private const string answer1 = "answer 1";
        private const string imageUrl = "image url";

        [TestMethod]
        public void CreateQA()
        {
            qaRepository = new QARepositoryInMemory();
            var qaService = new QAService(qaRepository);
            var id =qaService.CreateQA(start,end,host).Result;
            Assert.IsTrue(id > 0);
            var qa = qaRepository.GetQA(id).Result;
            Assert.IsNotNull(qa);
            Assert.AreEqual(qa.Start, start);
            Assert.AreEqual(qa.End, end);
            Assert.AreEqual(qa.Host, host);
        }

        [TestMethod]
        public void GetQA()
        {
            qaRepository = new QARepositoryInMemory();
            var id = qaRepository.CreateQA(start, end, host).Result;
            var qaService = new QAService(qaRepository);
            var qa = qaService.GetQA(id).Result;
            Assert.IsTrue(id > 0);
            Assert.IsNotNull(qa);
            Assert.AreEqual(qa.Start, start);
            Assert.AreEqual(qa.End, end);
            Assert.AreEqual(qa.Host, host);
        }

        [TestMethod]
        public void CreateQuestion()
        {
            qaRepository = new QARepositoryInMemory();
            var qaService = new QAService(qaRepository);
            var id = qaService.CreateQA(start, end, host).Result;
            var now = DateTime.UtcNow;
            var questionId = qaService.CreateQuestion(id, question1, requestor1).Result;
            Assert.IsTrue(questionId > 0);
            var question = qaRepository.GetQuestion(questionId).Result;
            Assert.IsNotNull(question);
            Assert.AreEqual(question.Text, question1);
            Assert.AreEqual(question.AskedBy, requestor1);
            Assert.IsTrue(question.Updated >  now);
        }

        [TestMethod]
        public void GetQuestion()
        {
            qaRepository = new QARepositoryInMemory();
            var id = qaRepository.CreateQA(start, end, host).Result;
            var questionId = qaRepository.CreateQuestion(id, new Models.Question { Text = question1, AskedBy = requestor1 }).Result;
            var qaService = new QAService(qaRepository);
            var question = qaService.GetQuestion(questionId).Result;
            Assert.IsNotNull(question);
            Assert.AreEqual(questionId, question.Id);           
            Assert.AreEqual(question.Text, question1);
            Assert.AreEqual(question.AskedBy, requestor1);
        }

        [TestMethod]
        public void AnswerQuestion()
        {
            qaRepository = new QARepositoryInMemory();
            var id = qaRepository.CreateQA(start, end, host).Result;
            var questionId = qaRepository.CreateQuestion(id, new Models.Question { Text = question1, AskedBy = requestor1 }).Result;
            var qaService = new QAService(qaRepository);
            qaService.AnswerQuestion(questionId, answer1, imageUrl).Wait();
            var question = qaService.GetQuestion(questionId).Result;
            Assert.IsNotNull(question);
            Assert.AreEqual(questionId, question.Id);
            Assert.AreEqual(question.Text, question1);
            Assert.AreEqual(question.AskedBy, requestor1);
            Assert.AreEqual(question.ImageUrl, imageUrl);
            Assert.AreEqual(question.Answer, answer1);
        }

        [TestMethod]
        public void QAwithQuestions()
        {
            qaRepository = new QARepositoryInMemory();
            var id = qaRepository.CreateQA(start, end, host).Result;
            var questionId = qaRepository.CreateQuestion(id, new Models.Question { Text = question1, AskedBy = requestor1 }).Result;
            var questionId2 = qaRepository.CreateQuestion(id, new Models.Question { Text = question1, AskedBy = requestor2 }).Result;
            var qaService = new QAService(qaRepository);
            qaService.AnswerQuestion(questionId, answer1, imageUrl).Wait();
            var qa = qaService.GetQA(questionId).Result;
            Assert.IsNotNull(qa.Questions);
            Assert.AreEqual(qa.Questions.Count(), 2);
            var questions1 = qa.Questions.Single(x => x.Id == questionId);
            var questions2 = qa.Questions.Single(x => x.Id == questionId2);
            Assert.IsNotNull(questions1);
            Assert.IsNotNull(questions2);
            Assert.AreEqual(questions1.Text, question1);
            Assert.AreEqual(questions1.ImageUrl, imageUrl);
            Assert.AreEqual(questions1.AskedBy, requestor1);
            Assert.AreEqual(questions1.Answer, answer1);
        }
    }
}
