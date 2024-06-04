using HandwrittenTextRecognitionSystem.Models;

namespace HandwrittenTextRecognitionSystem.Dtos
{
    public class SolutionDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public byte[] File { get; set; } = null!;
        //adding new prop to store the solution in string attr
        public int AssignmentId { get; set; }
        public string? Assignment { get; set; }//Ask for nullable
        public int StudentId { get; set; }
        public string? Student { get; set; }//Same above Ques.
    }
}
