using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class PostDto
    {
        public int Id { get; set; }
        [Required, MaxLength(400)]
        public string Content { get; set; }
        public int TeacherId { get; set; }
        public string Teacher { get; set; }
    }
}
