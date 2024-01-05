using System.ComponentModel.DataAnnotations;

namespace internKYC.DTOs.Requests
{
    public class ResendOTPRequest
    {
        [Required]
        public string contact_number { get; set; }
    }
}
