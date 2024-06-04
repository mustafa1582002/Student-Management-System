using AutoMapper;
using HandwrittenTextRecognitionSystem.Dtos;
using HandwrittenTextRecognitionSystem.Errors;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Services;
using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HandwrittenTextRecognitionSystem.Controllers
{
    public class SolutionsController : ApiBaseController
    {
        private readonly IGenericRepository<Solution> _solutionRepo;
        private readonly IMapper _mapper;

        public SolutionsController(IGenericRepository<Solution> SolutionRepo,IMapper mapper)
        {
            _solutionRepo = SolutionRepo;
            _mapper = mapper;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Solution>>> GetAllAsync()
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Solutions = await _solutionRepo.GetAllAsync();
        //    return Ok(Solutions);
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Solution>> GetSolutionById(int id)
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Solution = await _solutionRepo.GetByIdAsync(id);
        //    return Ok(Solution);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<Solution>> GetSolutionById(int id)
        {
            var Spec = new SolutionWithAssignmentAndStudentSpecification(id);
            var Solution = await _solutionRepo.GetByIdWithSpecificationAsync(Spec);
            if (Solution == null) return NotFound(new ApiResponse(404));

            var MappedSolution = _mapper.Map<Solution, SolutionDto>(Solution);
            return Ok(MappedSolution);
        }
        [HttpGet("spec")]
        public async Task<ActionResult<IEnumerable<Solution>>> GetAllSolutionWithSpecAsync()
        {
            var Spec = new SolutionWithAssignmentAndStudentSpecification();
            var Solutions = await _solutionRepo.GetAllWithSpecificationAsync(Spec);
            var MappedSolutions = _mapper.Map<IEnumerable<Solution>, IEnumerable<SolutionDto>>(Solutions);
            return Ok(MappedSolutions);
        }
    }
}
