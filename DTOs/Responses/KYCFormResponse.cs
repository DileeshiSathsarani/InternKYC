namespace internKYC.DTOs.Responses
{
    public class KYCFormResponse
    {
        public int id { get; set; }
        public string title { get; set; }
        public string full_name { get; set; }
        public string mobile_number { get; set; }
        public string email { get; set; }
        public string NICNumber { get; set; }
        public string nationality { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }



        public List<DocumentResponse> Documents { get; set; }
    }
}
