using AutoMapper;
using HandwrittenTextRecognitionSystem.Consts;
using HandwrittenTextRecognitionSystem.Data;
using HandwrittenTextRecognitionSystem.Dtos;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Services;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace HandwrittenTextRecognitionSystem.Controllers
{
    
    public class AuthenticationsController : ApiBaseController
    {
        private readonly IAuthenticationService _authentication;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IGenericRepository<Department> _departmentRepo;
        private readonly ApplicationDbContext _context;
        private readonly IImageService _imageService;
        public AuthenticationsController(IAuthenticationService authentication,IMapper mapper, UserManager<ApplicationUser> userManager, IGenericRepository<Department> departmentRepo, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment, IImageService imageService)
        {
            _authentication = authentication;
            _mapper = mapper;
            _userManager = userManager;
            _departmentRepo = departmentRepo;
            _context = context;
            _imageService = imageService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync(LoginDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                ModelState.AddModelError(nameof(model.Email), AppErrors.DataWrong);
                return BadRequest(ModelState);
            }

            var authorized = await _authentication.LoginAsync(user);

            return Ok(authorized);
        }
        [HttpPost("Register")]
        [Authorize(Roles = AppRoles.Admin)]
        public async Task<IActionResult> RegisterAsync(RegisterDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email
            };

            var CreateResult = await _userManager.CreateAsync(user, model.Password);

            if (!CreateResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, string.Join(',', CreateResult.Errors.Select(e => e.Description).ToList()));
                return BadRequest(ModelState);
            }

            var RoleResult = await _userManager.AddToRoleAsync(user, model.Role);

            if (!RoleResult.Succeeded)
            {
                ModelState.AddModelError(string.Empty, string.Join(',', RoleResult.Errors.Select(e => e.Description).ToList()));
                return BadRequest(ModelState);
            }

            return Ok();
        }
        [HttpPost("EditTecher")]
        [Authorize(Roles = "Doctor,Assistant")]
        public async Task<IActionResult> EditTeacherAsync([FromForm] EditTeacherDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                ModelState.AddModelError(nameof(user.Id), AppErrors.DataWrong);
                return BadRequest(ModelState);
            }

            var applicationUser = await _userManager.FindByNameAsync(model.UserName);

            if (applicationUser is not null && applicationUser.Id != user.Id)
            {
                ModelState.AddModelError(nameof(applicationUser.UserName), AppErrors.DataWrong);
                return BadRequest(ModelState);
            }

            var department = await _context.Departments.AsNoTracking().SingleOrDefaultAsync(d => d.Id == model.DepartmentId);
            if (department is null)
            {
                ModelState.AddModelError(nameof(model.DepartmentId), AppErrors.DataWrong);
                return BadRequest(ModelState);
            }


            var Teacher = await _context.Set<Teacher>().AsNoTracking().SingleOrDefaultAsync(d => d.ApplicationUserId == user.Id) ?? new Teacher();
            Teacher.ApplicationUserId ??= user.Id;
            Teacher.DepartmentId = department.Id;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;


            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _userManager.UpdateAsync(user);

                if (Teacher.Id.Equals(0))
                    _context.Add(Teacher);
                else
                    _context.Update(Teacher);

                _context.SaveChanges();


                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                ModelState.AddModelError(string.Empty, AppErrors.DatabaseFailure);
                return BadRequest(ModelState);
            }

            if (model.Image is not null)
                await _imageService.CreateImageAsync(user.Id, model.Image);

            return Ok(new { userId = user.Id });
        }
        [HttpPost("EditStudent")]
        [Authorize(Roles = AppRoles.Student)]
        public async Task<IActionResult> EditStudentAsync([FromForm] EditStudentDto model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var userId = User.FindFirstValue("uid");
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                ModelState.AddModelError(nameof(user.Id), AppErrors.DataWrong);
                return BadRequest(ModelState);
            }

            var applicationUser = await _userManager.FindByNameAsync(model.UserName);

            if (applicationUser is not null && applicationUser.Id != user.Id)
            {
                ModelState.AddModelError(nameof(applicationUser.UserName), AppErrors.DataWrong);
                return BadRequest(ModelState);
            }

            var department = await _context.Departments.AsNoTracking().SingleOrDefaultAsync(d => d.Id == model.DepartmentId);
            if (department is null)
            {
                ModelState.AddModelError(nameof(model.DepartmentId), AppErrors.DataWrong);
                return BadRequest(ModelState);
            }


            var student = await _context.Set<Student>().AsNoTracking().SingleOrDefaultAsync(d => d.ApplicationUserId == user.Id) ?? new Student();
            student.ApplicationUserId ??= user.Id;
            student.DepartmentId = department.Id;
            student.Level = model.Level;

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.UserName = model.UserName;
            user.PhoneNumber = model.PhoneNumber;


            using var transaction = _context.Database.BeginTransaction();
            try
            {
                await _userManager.UpdateAsync(user);

                if (student.Id.Equals(0))
                    _context.Add(student);
                else
                    _context.Update(student);

                _context.SaveChanges();


                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                ModelState.AddModelError(string.Empty, AppErrors.DatabaseFailure);
                return BadRequest(ModelState);
            }

            if (model.Image is not null)
                await _imageService.CreateImageAsync(user.Id, model.Image);

            return Ok(new { userId = user.Id });
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ViewDepartmentDto>>> GetAllDepatment()
        {
            var departments =await _departmentRepo.GetAllAsync();
            var mappedDepatment = _mapper.Map<List<ViewDepartmentDto>>(departments);

            return Ok(mappedDepatment);
        }



        [Authorize(Roles =AppRoles.Admin)]
        [HttpPost]
        public async Task<ActionResult> AddStudent([FromForm]AddStudentDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not Valid data");
            ApplicationUser appuser = new ApplicationUser()
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                UserName = dto.UserName,

            };
            var result = await _userManager.CreateAsync(appuser, dto.Password);
            if (!result.Succeeded)
                return BadRequest("Error In AppUser");
            await _userManager.AddToRoleAsync(appuser, AppRoles.Student);
            Student student = new Student()
            {
                Level = dto.Level,
                DepartmentId = dto.DepartmentId,
                Name = dto.Name,
                ApplicationUser = appuser,
               
            };
            try{
                _context.Students.Add(student); 
                _context.SaveChanges();
            }
            catch(Exception ex) { 
                return BadRequest(ex.Message);  
            }
            return Ok();
        }
    }
}
