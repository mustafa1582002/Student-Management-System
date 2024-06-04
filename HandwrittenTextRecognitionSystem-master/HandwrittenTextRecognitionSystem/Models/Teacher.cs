namespace HandwrittenTextRecognitionSystem.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser? ApplicationUser { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<TeacherAssistant> TeacherAssistants { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<Request> Requests { get; set; }


    }
}
