using HandwrittenTextRecognitionSystem.Consts;
using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Models
{
    public class Student
    {
        public int Id { get; set; } 
        public int Level { get; set; }
        [Required,MaxLength(50)]
        public string Name { get; set; }//Added new
        public string ApplicationUserId { get; set; } = null!;
        public ApplicationUser? ApplicationUser { get; set; }
        public Department? Department { get; set; }
        public int DepartmentId { get; set; }
        public ICollection<Solution>? Solutions { get; set; }
        public ICollection<Group> Groups { get; set; }
        public ICollection<Post> Posts { get; set; }

        public ICollection<Request> Requests { get; set; }
        
    }
}