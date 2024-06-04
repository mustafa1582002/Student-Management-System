using HandwrittenTextRecognitionSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class CourseDto
    {
        public int Id { get; set; }
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int DoctorId { get; set; }
    }
}
