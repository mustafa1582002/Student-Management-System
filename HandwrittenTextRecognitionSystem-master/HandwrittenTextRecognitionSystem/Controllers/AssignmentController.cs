using AutoMapper;
using HandwrittenTextRecognitionSystem.Dtos;
using HandwrittenTextRecognitionSystem.Errors;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Services;
using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.AspNetCore.Mvc;

namespace HandwrittenTextRecognitionSystem.Controllers
{
    public class AssignmentController:ApiBaseController
    {
        private readonly IGenericRepository<Assignment> _assignmentRepo;
        private readonly IMapper _mapper;

        public AssignmentController(IGenericRepository<Assignment> AssignmentRepo, IMapper mapper)
        {
            _assignmentRepo = AssignmentRepo;
            _mapper = mapper;
        }
        [HttpGet("spec")]
        public async Task<ActionResult<IEnumerable<Assignment>>> GetAllAssignmentWithSpec()
        {
            var Spec = new AssignmentWithTeacherAndCourseSpecification();
            var Assignments = await _assignmentRepo.GetAllWithSpecificationAsync(Spec);
            var MappedAssignments = _mapper.Map<IEnumerable<Assignment>, IEnumerable<AssignmentDto>>(Assignments);
            return Ok(MappedAssignments);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Assignment>> GetAssignmentById(int id)
        {
            var Spec = new AssignmentWithTeacherAndCourseSpecification(id);
            var Assignment = await _assignmentRepo.GetByIdWithSpecificationAsync(Spec);
            if (Assignment == null) return NotFound(new ApiResponse(404));
            var MappedAssignment = _mapper.Map<Assignment, AssignmentDto>(Assignment);
            return Ok(MappedAssignment);
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Assignment>> GetAssignmentById(int id)
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Assignment = await _AssignmentRepo.GetByIdAsync(id);
        //    return Ok(Assignment);
        //}
    }

}
