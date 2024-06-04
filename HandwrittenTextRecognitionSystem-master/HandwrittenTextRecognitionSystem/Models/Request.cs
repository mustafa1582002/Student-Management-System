namespace HandwrittenTextRecognitionSystem.Models
{
    public class Request
    {
        public int id { get; set; }
        public bool IsAccepted { get; set; } = false;
        public Student Student { get; set; }
        public int StudentId { get; set; }
        public Teacher Teacher { get; set; }
        public int TeacherId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
