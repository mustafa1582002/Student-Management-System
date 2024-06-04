namespace HandwrittenTextRecognitionSystem.Models
{
    public class TeacherAssistantGroup
    {
        public int TeacherAssistantId { get; set; }
        public TeacherAssistant TeacherAssistant { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
    }
}
