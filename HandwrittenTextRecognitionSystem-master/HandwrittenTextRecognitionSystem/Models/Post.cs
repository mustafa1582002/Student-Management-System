using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required, MaxLength(400)]
        public string Content { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
    }
}