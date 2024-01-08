using System.ComponentModel.DataAnnotations;

namespace internKYC.DTOs.Requests
{
    public class ContactNumberRequest
    {
        [Required]
        public string contact_number { get; set; }


    }
}
