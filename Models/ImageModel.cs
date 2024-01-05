using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace internKYC.Models
{
    public class ImageModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? KYCForms { get; set; }

        [Required]
        public string Base64NICFrontImage { get; set; }

        [Required]
        public string Base64NICBackImage { get; set; }

        [Required]
        public string Base64SelfieImage { get; set; }
    }
}
