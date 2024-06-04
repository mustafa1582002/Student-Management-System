namespace HandwrittenTextRecognitionSystem.Models
{
    public class TeacherAssistant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Teacher> Teachers { get; set; }
        public ICollection<Group> Groups { get; set; }
    }
}
