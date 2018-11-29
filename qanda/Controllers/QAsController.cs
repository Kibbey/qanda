using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qanda.Api.Models;
using Qanda.Domain;
using Qanda.Domain.Interfaces;
using Qanda.Domain.Models;

namespace qanda.Controllers
{

    // Things that should probably be done in a real project
    /* Permissions / identity
     * Allow for deleting QA (soft)
     * Allow for updating QA (dates)
     * Allow question to be updated (with version) or removed (if not answered)
     * Allow answer to be modified (w/history and versioning)
     * Need to deal with CSS attacks for questions
     * Search for qas endpoint
     */
    [Route("api/[controller]")]
    public class QAsController : Controller
    {
        private IQAService qaService;

        public QAsController(IQAService qaService)
        {
            this.qaService = qaService;
        }


        // GET api/qas/5/
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQA(int id)
        {
            var qa = await qaService.GetQA(id);
            var qaResponse = new QAResponse(qa);
            return Ok(qaResponse);
        }

        // POST api/qas/
        [HttpPost]
        public async Task<IActionResult> CreateQA([FromBody] QARequest qa)
        {
            var validationResult = qa.Validate();
            if (!validationResult.Valid) return BadRequest(validationResult.ToString());
            var qaId = await qaService.CreateQA(qa.Start, qa.End, qa.Host);
            return Ok(new QAResponse(await qaService.GetQA(qaId))); 
        }

        // POST api/qas/5/questions
        [HttpPost("{id}/questions")]
        public async Task<IActionResult> CreateQaQuestion(int id, [FromBody] QuestionRequest questionRequest)
        {
            var validationResult = questionRequest.Validate();
            if (!validationResult.Valid) return BadRequest(validationResult.ToString());
            var questionId = await qaService.CreateQuestion(id, questionRequest.Text, questionRequest.AskedBy);
            return Ok(await qaService.GetQuestion(questionId));
        }

        // GET api/qas/5/questions
        [HttpGet("{id}/questions")]
        public async Task<IActionResult> GetQuestions(int id)
        {
            var qa = await qaService.GetQA(id);
            return Ok(qa.Questions);
        }
    }
}
