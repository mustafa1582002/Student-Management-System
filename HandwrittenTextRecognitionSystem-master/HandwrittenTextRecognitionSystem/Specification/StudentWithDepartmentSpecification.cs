using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Specification
{
    public class StudentWithDepartmentSpecification : BaseSpecification<Student>
    {
        public StudentWithDepartmentSpecification() : base()
        {
            Includes.Add(p => p.Department);
        }
        public StudentWithDepartmentSpecification(int id) : base(p => p.Id == id)
        {
            Includes.Add(p => p.Department);
        }
    }
}
