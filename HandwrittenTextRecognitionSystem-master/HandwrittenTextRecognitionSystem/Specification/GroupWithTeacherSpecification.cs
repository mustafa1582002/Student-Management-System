using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public class GroupWithTeacherSpecification:BaseSpecification<Group>
    {
        public GroupWithTeacherSpecification() : base()
        {
            Includes.Add(p => p.Teacher);
        }
        public GroupWithTeacherSpecification(int id) : base(p=>p.Id == id)
        {
            Includes.Add(p => p.Teacher);
        }
    }
}
