using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class ViewRequstDto
    {
        public int id { get; set; }
        public bool IsAccepted { get; set; } 
        public int StudentId { get; set; }

        public string studentName { get; set; }
        public DateTime CreatedAt { get; set; } 

    }
}
