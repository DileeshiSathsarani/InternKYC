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
        public string contact_number { get; set; }

        [Required]
        public string otp { get; set; }

        public string OtpLog { get; set; }
        public int id { get; set; }
        public KYCFormModel KYCForm { get; set; }
    }


}