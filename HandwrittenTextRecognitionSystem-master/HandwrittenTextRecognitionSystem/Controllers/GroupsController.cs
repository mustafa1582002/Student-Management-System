using AutoMapper;
using HandwrittenTextRecognitionSystem.Data;
using HandwrittenTextRecognitionSystem.Dtos;
using HandwrittenTextRecognitionSystem.Errors;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Services;
using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HandwrittenTextRecognitionSystem.Controllers
{

    public class GroupsController : ApiBaseController
    {
        private readonly IGenericRepository<Group> _groupRepo;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public GroupsController(IGenericRepository<Group> GroupRepo,IMapper mapper,ApplicationDbContext context)
        {
            _groupRepo = GroupRepo;
            _mapper = mapper;
            _context = context;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Group>>> GetAllAsync()
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Groups =await _groupRepo.GetAllAsync();
        //    return Ok(Groups);
        //}
        [HttpGet("spec")]
        public async Task<ActionResult<IEnumerable<Group>>> GetAllGroupWithSpec()
        {
            var Spec = new GroupWithTeacherSpecification();
            var Groups =await _groupRepo.GetAllWithSpecificationAsync(Spec);
            var MappedGroups = _mapper.Map<IEnumerable<Group>, IEnumerable<GroupDto>>(Groups);
            return Ok(MappedGroups);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Group>> GetGroupById(int id)
        {
            var Spec = new GroupWithTeacherSpecification(id);
            var Group = await _groupRepo.GetByIdWithSpecificationAsync(Spec);
            if (Group == null) return NotFound(new ApiResponse(404));

            var MappedGroup = _mapper.Map<Group, GroupDto>(Group);
            return Ok(MappedGroup);
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Group>> GetGroupById(int id)
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Group = await _groupRepo.GetByIdAsync(id);
        //    return Ok(Group);
        //}
    }
}
