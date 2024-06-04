using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class TeacherDto
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public string? ApplicationUser { get; set; }
        public string? Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<TeacherAssistant> TeacherAssistants { get; set; }
        //    public ICollection<Group> Groups { get; set; }
        //    public ICollection<Assignment> Assignments { get; set; }
        //    public ICollection<Post> Posts { get; set; }
    }
}
