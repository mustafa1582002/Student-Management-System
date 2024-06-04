using HandwrittenTextRecognitionSystem.Models;
using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class GroupDto
    {
        public int Id { get; set; }
        [Required, MaxLength(30)]
        public string GroupName { get; set; }
        public int TeacherId { get; set; }
        public string Teacher { get; set; }
        //public ICollection<TeacherAssistant> TeacherAssistants { get; set; }//ask for many-to-many rela
        //public ICollection<Student> Students { get; set; }
    }
}
