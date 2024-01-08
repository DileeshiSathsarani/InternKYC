
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using internKYC.Models;

namespace internKYC.Models
{
    [Table("kycformdata")]
    public class KYCFormModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string full_name { get; set; }

        [Required]
        [RegularExpression(@"^\+94\d{9}$", ErrorMessage = "Invalid contact number format. Use +94XXXXXXXXX")]
        public string mobile_number { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string NICNumber { get; set; }

        [Required]
        public string nationality { get; set; }


        public string Status { get; set; }


        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }


        public void UpdateStatus(string newStatus)
        {
            Status = newStatus;
            updated_at = DateTime.Now;
        }
    }
}
