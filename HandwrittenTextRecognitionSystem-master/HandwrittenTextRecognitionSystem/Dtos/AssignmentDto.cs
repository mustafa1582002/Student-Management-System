using HandwrittenTextRecognitionSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class AssignmentDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public DateTime CreatedOn { get; set; }
        public DateTime DeadLine { get; set; }
        public byte[] File { get; set; } = null!;
        public string? Path { get; set; }
        public string? Course { get; set; }
        public int CourseId { get; set; }
        //public ICollection<Solution>? Solutions { get; set; }
        public int TeacherId { get; set; }
        public string Teacher { get; set; }
    }
}
