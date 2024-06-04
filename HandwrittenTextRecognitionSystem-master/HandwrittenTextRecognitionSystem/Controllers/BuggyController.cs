using HandwrittenTextRecognitionSystem.Data;
using Microsoft.AspNetCore.Mvc;

namespace HandwrittenTextRecognitionSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ApiBaseController
    {
        private readonly ApplicationDbContext _dbContext;

        public BuggyController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet("NotFound")]
        public ActionResult GetNotFoundRequest()
        {
            var Group = _dbContext.Groups.Find(100);
            if (Group is null) return NotFound();
            return Ok(Group);
        }
        [HttpGet("ServerError")]
        public ActionResult GetServerError()
        {
            var Group = _dbContext.Groups.Find(100);
            var GroupDto = Group.ToString();
            return Ok(GroupDto);
        }
        [HttpGet("BadRequest")]
        public ActionResult GetBadRequest()
        {
            return BadRequest();
        }
        [HttpGet("BadRequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {
            return Ok();
        }
    }
}
