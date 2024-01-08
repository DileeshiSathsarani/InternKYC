using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using internKYC.Models;

namespace internKYC.Models
{

    public class ContactNumberModel
    {
        [Key]
        public int ContactNumberId { get; set; }

        [Required]
        [RegularExpression(@"^\+94\d{9}$", ErrorMessage = "Invalid contact number format. Use +94XXXXXXXXX")]
        public string contact_number { get; set; }

        [Required]
        public string otp { get; set; }

        public string? OtpLog { get; set; }

        public int? id { get; set; }

        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

    }


}