using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public class TeacherWithAppUserSpecification : BaseSpecification<Teacher>
    {
        public TeacherWithAppUserSpecification() : base()
        {
            Includes.Add(p => p.ApplicationUser);
        }
        public TeacherWithAppUserSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.ApplicationUser);
        }
    }
}
