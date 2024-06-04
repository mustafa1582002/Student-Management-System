using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class CreateAndEditAssigmentDto
    {
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public DateTime DeadLine { get; set; }
        public IFormFile FileAssigment { get; set; } = null!;
        public int CourseId { get; set; }
    }
}
