using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public class AssignmentWithTeacherAndCourseSpecification:BaseSpecification<Assignment>
    {
        public AssignmentWithTeacherAndCourseSpecification() : base()
        {
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.Course);
        }
        public AssignmentWithTeacherAndCourseSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Teacher);
            Includes.Add(p => p.Course);
        }
    }
}
