using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public class SolutionWithAssignmentAndStudentSpecification : BaseSpecification<Solution>
    {
        public SolutionWithAssignmentAndStudentSpecification() : base()
        {
            Includes.Add(p => p.Assignment);
            Includes.Add(p => p.Student);
        }
        public SolutionWithAssignmentAndStudentSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Assignment);
            Includes.Add(p => p.Student);
        }
    }
}
