using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class ViewAssigmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
