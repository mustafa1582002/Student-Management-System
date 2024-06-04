namespace HandwrittenTextRecognitionSystem.Models
{
    public class Solution
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public byte[] File { get; set; } = null!;
        //adding new prop to store the solution in string attr
        public int AssignmentId { get; set; }
        public Assignment? Assignment { get; set; }//Ask for nullable
        public int StudentId { get; set; }
        public Student? Student { get; set; }//Same above Ques.
    }
}
