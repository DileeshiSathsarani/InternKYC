using System.ComponentModel.DataAnnotations;

namespace internKYC.DTOs.Requests
{
    public class RegisterKYCFormRequest
    {
        [Required]
        public string title { get; set; }

        [Required]
        public string full_name { get; set; }

        [Required]
        public string mobile_number { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string NICNumber { get; set; }

        [Required]
        public string nationality { get; set; }

    }
}
