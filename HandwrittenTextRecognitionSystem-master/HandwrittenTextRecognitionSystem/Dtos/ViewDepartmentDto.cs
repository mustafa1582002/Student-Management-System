using System.ComponentModel.DataAnnotations;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class ViewDepartmentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

    }
}
