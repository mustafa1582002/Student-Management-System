using AutoMapper;
using HandwrittenTextRecognitionSystem.Consts;
using HandwrittenTextRecognitionSystem.Data;
using HandwrittenTextRecognitionSystem.Dtos;
using HandwrittenTextRecognitionSystem.Errors;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Services;
using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HandwrittenTextRecognitionSystem.Controllers
{

    public class SutdentsController : ApiBaseController
    {
        private readonly IGenericRepository<Student> _studentRepo;
        private readonly IMapper _mapper;
        private readonly IGenericRepository<Assignment> _assignmentRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public SutdentsController(IGenericRepository<Student> StudentRepo,IMapper mapper, IGenericRepository<Assignment> AssignmentRepo,UserManager<ApplicationUser>   userManager,ApplicationDbContext context)
        {
            _studentRepo = StudentRepo;
            _mapper = mapper;
            _assignmentRepo = AssignmentRepo;
            _userManager = userManager;
            _context = context;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Student>>> GetAllAsync()
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Students = await _studentRepo.GetAllAsync();
        //    return Ok(Students);
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Student>> GetStudentById(int id)
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Student = await _studentRepo.GetByIdAsync(id);
        //    return Ok(Student);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var Spec = new StudentWithDepartmentSpecification(id);
            var Student = await _studentRepo.GetByIdWithSpecificationAsync(Spec);
            if (Student == null) return NotFound(new ApiResponse(404));

            var MappedStudent = _mapper.Map<Student, StudentDto>(Student);
            return Ok(MappedStudent);
        }
        [HttpGet("spec")]
        public async Task<ActionResult<IEnumerable<Student>>> GetAllStudentWithSpecAsync()
        {
            var Spec = new StudentWithDepartmentSpecification();
            var Students = await _studentRepo.GetAllWithSpecificationAsync(Spec);
            var MappedStudents = _mapper.Map<IEnumerable<Student>, IEnumerable<StudentDto>>(Students);
            return Ok(MappedStudents);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewAssigmentDto>>> GetAllAssignment()
        {
            var assigmnets= await _assignmentRepo.GetAllAsync();
            var mappedAss = _mapper.Map<List<ViewAssigmentDto>>(assigmnets);
           
            return Ok(mappedAss);
        }

        [HttpPost]
        [Authorize(Roles =AppRoles.Student)]
        public async Task<ActionResult> AddSolution([FromForm]CreateSolutionDto dto)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email claim not found in JWT token.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Not found");
            var student = _context.Students.Where(T => T.ApplicationUserId == user.Id).SingleOrDefault();   //Improve this 
            if (student == null)
                return BadRequest("NotFound");
            if (!ModelState.IsValid)
                return BadRequest("Fill All required");
            using var dataStream = new MemoryStream();
            await dto.File.CopyToAsync(dataStream);
            Solution solution = new Solution()
            {
                File = dataStream.ToArray(),
                StudentId = student.Id,
                AssignmentId = dto.AssignmentId,
                Date = DateTime.Now,

            };

          
            try
            {
                _context.Add(solution);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();

        }
        

        [HttpPost]
        [Authorize(Roles = AppRoles.Student)]
        public async Task<ActionResult> AddRequest([FromForm]RequestDto dto)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email claim not found in JWT token.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Not found");
            var student = _context.Students.Where(T => T.ApplicationUserId == user.Id).SingleOrDefault();   //Improve this 
            if (student == null)
                return BadRequest("NotFound");
            if (!ModelState.IsValid)
                return BadRequest("Fill All required");
            Request request = new Request()
            {
                StudentId = student.Id,
                TeacherId = dto.TeacherId,

            };
            _context.Add(request);
            _context.SaveChanges();
            return Ok();

        }

    }
}
