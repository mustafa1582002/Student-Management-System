using HandwrittenTextRecognitionSystem.Errors;
using HandwrittenTextRecognitionSystem.Models;
using HandwrittenTextRecognitionSystem.Services;
using HandwrittenTextRecognitionSystem.Specification;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HandwrittenTextRecognitionSystem.Controllers
{
    public class PostsController : ApiBaseController
    {
        private readonly IGenericRepository<Post> _postRepo;

        public PostsController(IGenericRepository<Post> PostRepo)
        {
            _postRepo = PostRepo;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<Post>>> GetAllAsync()
        //{
        //    //diff between eagerloading for one and lazyLoading for many 
        //    var Posts = await _postRepo.GetAllAsync();
        //    return Ok(Posts);
        //}
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPostById(int id)
        {
            var Spec = new PostWithTeacherSpecification(id);
            var Post = await _postRepo.GetByIdWithSpecificationAsync(Spec);
            if (Post == null) return NotFound(new ApiResponse(404));
            return Ok(Post);
        }
        [HttpGet("spec")]
        public async Task<ActionResult<IEnumerable<Post>>> GetAllPostWithSpecAsync()
        {
            var Spec = new PostWithTeacherSpecification();
            var Posts = await _postRepo.GetAllWithSpecificationAsync(Spec);
            return Ok(Posts);
        }
    }
}
