using AutoMapper;
using HandwrittenTextRecognitionSystem.Consts;
using HandwrittenTextRecognitionSystem.Data;
using HandwrittenTextRecognitionSystem.Dtos;
using HandwrittenTextRecognitionSystem.Errors;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Services;
using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Security.Claims;

namespace HandwrittenTextRecognitionSystem.Controllers
{
    public class TeachersController : ApiBaseController
    {
        private readonly IGenericRepository<Teacher> _teacherRepo;
        private readonly IGenericRepository<Request> _requstRepo;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly IGenericRepository<Course> _courseRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthenticationService _authenticationService;

        public TeachersController(IGenericRepository<Teacher> TeacherRepo, IGenericRepository<Request> requstRepo, IMapper mapper,ApplicationDbContext context, IGenericRepository<Course> CourseRepo,UserManager<ApplicationUser> userManager )
        {
            _teacherRepo = TeacherRepo;
            _requstRepo = requstRepo;
            _mapper = mapper;
            _context = context;
            _courseRepo = CourseRepo;
            _userManager = userManager;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Teacher>>> GetAllAsync() 
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Teachers = await _teacherRepo.GetAllAsync();
        //    return Ok(Teachers);
        //}
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Teacher = await _teacherRepo.GetByIdAsync(id);
        //    return Ok(Teacher);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<Teacher>> GetTeacherById(int id)
        {
            var Spec = new TeacherWithAppUserSpecification(id);
            var Teacher = await _teacherRepo.GetByIdWithSpecificationAsync(Spec);
            if (Teacher == null) return NotFound(new ApiResponse(404));
            var MappedTeacher = _mapper.Map<Teacher, TeacherDto>(Teacher);
            return Ok(MappedTeacher);
        }
        [HttpGet("spec")]
        public async Task<ActionResult<IEnumerable<Teacher>>> GetAllTeacherWithSpecAsync()
        {
            var Spec = new TeacherWithAppUserSpecification();
            var Teachers = await _teacherRepo.GetAllWithSpecificationAsync(Spec);
            var MappedTeachers = _mapper.Map<IEnumerable<Teacher>, IEnumerable<TeacherDto>>(Teachers);
            return Ok(MappedTeachers);
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseDto>>> GetAllCoursesWithSpecAsync()
        {
            var courses =await _courseRepo.GetAllAsync();
            var mapped = _mapper.Map<List<CourseDto>>(courses);
            return Ok(mapped);
        }
        

        [Authorize(Roles = AppRoles.Doctor)]
        [HttpPost]
        public async Task<ActionResult> CreateAssignment([FromForm] CreateAndEditAssigmentDto dto)
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email claim not found in JWT token.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Not found");
            var teacher = _context.Teachers.Where(T => T.ApplicationUserId == user.Id).SingleOrDefault();   //Improve this 
            if(teacher == null)
                return BadRequest("NotFound");  
            if (!ModelState.IsValid)
                return BadRequest("Fill All required");
            using var dataStream = new MemoryStream();
            await dto.FileAssigment.CopyToAsync(dataStream);
            var mappedData = _mapper.Map<Assignment>(dto);
            mappedData.File=dataStream.ToArray();
            mappedData.TeacherId = teacher.Id;
            mappedData.CreatedOn=DateTime.Now;
            if (mappedData.DeadLine < DateTime.Now )
                return BadRequest("Not Valid DeadLine");
            try
            {
                _context.Add(mappedData);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
            return Ok();
        }


        [Authorize(Roles = AppRoles.Doctor)]
        [HttpPut("{id}")]
        public async Task<ActionResult> EditAssignment([FromForm] CreateAndEditAssigmentDto dto,int id)
        {
            if (id == 0) return BadRequest("Enter id");
           
            if (!ModelState.IsValid)
                return BadRequest("Fill All required");
            using var dataStream = new MemoryStream();
            await dto.FileAssigment.CopyToAsync(dataStream);
            var assignment = await _context.Assignments.FindAsync(id);
            if (assignment == null) return BadRequest("Not Found Assignment");
            assignment.File=dataStream.ToArray();
            assignment.CourseId = dto.CourseId;
            assignment.CreatedOn=DateTime.Now;
            assignment.DeadLine = dto.DeadLine;
            assignment.Name=dto.Name;
            
            if (assignment.DeadLine < DateTime.Now )
                return BadRequest("Not Valid DeadLine");
            try
            {
                _context.Update(assignment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok();
        }

        [HttpGet]
        [Authorize(Roles = AppRoles.Doctor)]

        public async Task<ActionResult<IEnumerable<ViewRequstDto>>> GetAllRequstsTeacherHave()
        {
            var email = User?.FindFirstValue(ClaimTypes.Email);
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email claim not found in JWT token.");
            }

            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return BadRequest("Not found");
            var teacher = _context.Teachers.Where(T => T.ApplicationUserId == user.Id).SingleOrDefault();   //Improve this 
            if (teacher == null)
                return BadRequest("NotFound");
            if (!ModelState.IsValid)
                return BadRequest("Fill All required");

            var requests = _context.Requests.Where(x => x.TeacherId == teacher.Id).Include(r=>r.Student).Where(r=>r.IsAccepted==false).ToList();
            var mappedReq=_mapper.Map<List<ViewRequstDto>>(requests);
           
            return Ok(mappedReq);
        }


        [HttpPut("{requestId}")]
        [Authorize(Roles = AppRoles.Doctor)]
        public async Task<ActionResult> AcceptRequests(int requestId)
        {
          
            var requst =  await _requstRepo.GetByIdAsync(requestId);
            if (requst == null)
                return BadRequest("Not Found Requstt");
            requst.IsAccepted = true;
            _context.Update(requst);
            _context.SaveChanges();
            
            return Ok();

        }
    }
}
