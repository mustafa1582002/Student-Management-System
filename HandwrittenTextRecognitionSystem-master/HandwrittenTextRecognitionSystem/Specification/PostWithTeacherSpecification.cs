using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public class PostWithTeacherSpecification:BaseSpecification<Post>
    {
        public PostWithTeacherSpecification() : base()
        {
            Includes.Add(p => p.Teacher);
        }
        public PostWithTeacherSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Teacher);
        }
    }
}
