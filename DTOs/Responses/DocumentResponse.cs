namespace internKYC.DTOs.Responses
{
    public class DocumentResponse
    {
        public int doc_id { get; set; }
        public string doc_type { get; set; }
        public string Base64NICFrontImage { get; set; }
        public string Base64NICBackImage { get; set; }
        public string Base64SelfieImage { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
}
