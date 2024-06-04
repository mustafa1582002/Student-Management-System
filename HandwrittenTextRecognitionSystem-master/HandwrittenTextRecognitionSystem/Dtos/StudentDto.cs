using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class StudentDto
    {
        public int Id { get; set; }
        public int Level { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }//Added new
        public string ApplicationUserId { get; set; } = null!;
        public string? ApplicationUser { get; set; }
        public string? Department { get; set; }
        public int DepartmentId { get; set; }
        //public ICollection<Solution>? Solutions { get; set; }
        //public ICollection<Group> Groups { get; set; }
        //public ICollection<Post> Posts { get; set; }
    }
}
