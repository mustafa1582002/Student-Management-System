using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class CreateSolutionDto
    {
        public IFormFile  File { get; set; } 
        public int AssignmentId { get; set; }
    }
}
