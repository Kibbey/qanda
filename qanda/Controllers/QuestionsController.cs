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

    [Route("api/[controller]")]
    public class QuestionsController : Controller
    {
        private IQAService qaService;

        public QuestionsController(IQAService qaService)
        {
            this.qaService = qaService;
        }

        // GET api/questions/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(int id)
        {
            return Ok(await qaService.GetQA(id));
        }

        // PATCH api/questions/5/answers
        [HttpPatch("{id}/answers")]
        public async Task<IActionResult> AnswerQuestion(int id, [FromBody] AnswerRequest answerRequest)
        {
            var validationResult = answerRequest.Validate();
            if (!validationResult.Valid) return BadRequest(validationResult.ToString());
            await qaService.AnswerQuestion(id, answerRequest.Text, answerRequest.ImageUrl);
            return Ok(await qaService.GetQuestion(id));
        }
    }
}
