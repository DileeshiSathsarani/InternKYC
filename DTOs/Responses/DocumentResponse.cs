namespace internKYC.DTOs.Responses
{
    public class DocumentResponse
    {
        public int doc_id { get; set; }
        public string doc_type { get; set; }
        public string path { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
