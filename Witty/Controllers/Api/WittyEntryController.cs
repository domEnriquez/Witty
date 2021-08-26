using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Witty.Repositories;

namespace Witty.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WittyEntryController : ControllerBase
    {
        private readonly WittyEntryRepository wittyEntryRepository;

        public WittyEntryController(WittyEntryRepository wittyEntryRepository)
        {
            this.wittyEntryRepository = wittyEntryRepository;
        }

        [HttpDelete("{wittyEntryId}&&{responseId}")]
        public IActionResult Delete(string wittyEntryId, string responseId)
        {
            try
            {
                wittyEntryRepository.DeleteResponse(wittyEntryId, responseId);
                wittyEntryRepository.Save();

                return Ok();
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database access failure");
            }
        }

        [Produces("application/json")]
        [HttpGet("search/{keyword}")]
        public IActionResult SearchQuestion(string keyword)
        {
            try
            {
                List<string> questions = wittyEntryRepository.GetMatchingQuestions(keyword);

                return Ok(questions);
            }
            catch
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database access failure");
            }
        }

    }
}
