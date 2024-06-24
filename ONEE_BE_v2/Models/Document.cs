namespace ONEE_BE_v2.Models
{
    public class Document
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FileType { get; set; }
        public string Path { get; set; } 
        public int CandidatureId { get; set; }

        // Navigation property back to the Candidature
        public Candidature Candidature { get; set; }
    }
}
