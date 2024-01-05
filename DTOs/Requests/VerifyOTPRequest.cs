using System.ComponentModel.DataAnnotations;
using internKYC.DTOs.Responses;

namespace internKYC.DTOs.Requests
{
    public class VerifyOTPRequest
    {

        [Required]
        public string otp { get; set; }

        public string contact_number { get; set; }

        
    }
}
