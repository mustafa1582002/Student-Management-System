using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string GroupName { get; set; }
        public int TeacherId { get; set; }
        public Teacher Teacher { get; set; }
        public ICollection<TeacherAssistant> TeacherAssistants { get; set; }//ask for many-to-many rela
        public ICollection<Student> Students { get; set; }
    }
}