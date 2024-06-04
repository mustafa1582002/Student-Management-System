using HandwrittenTextRecognitionSystem.Errors;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Services;
using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandwrittenTextRecognitionSystem.Controllers
{
    public class TeacherAssistantsController : ApiBaseController
    {
        private readonly IGenericRepository<TeacherAssistant> _teacherAssistRepo;

        public TeacherAssistantsController(IGenericRepository<TeacherAssistant> TeacherAssistRepo)
        {
            _teacherAssistRepo = TeacherAssistRepo;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<TeacherAssistant>>> GetAllAsync()
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var TeacherAssists = await _teacherAssistRepo.GetAllAsync();
        //    return Ok(TeacherAssists);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<TeacherAssistant>> GetTeacherAssistantById(int id)
        {
            //diff between eagerloading for one and lazyLoading for many 
            var TeacherAssistant = await _teacherAssistRepo.GetByIdAsync(id);
            if (TeacherAssistant == null) return NotFound(new ApiResponse(404));

            return Ok(TeacherAssistant);
        }
        [HttpGet("spec")]
        public async Task<ActionResult<IEnumerable<TeacherAssistant>>> GetAllTeacherAssistantWithSpecAsync()
        {
            var Spec = new BaseSpecification<TeacherAssistant>();
            var TeacherAssistants = await _teacherAssistRepo.GetAllWithSpecificationAsync(Spec);
            return Ok(TeacherAssistants);
        }
    }
}
