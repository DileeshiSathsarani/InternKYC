using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using internKYC.Models;
/*
namespace internKYC.Models
{
    [Table("images")]
    public class FileUploadAPI
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string? KYCForms { get; set; }
        [NotMapped]
        public IFormFile? NIC_front_Image { get; set; }

        [NotMapped]
        public IFormFile? NIC_back_Image { get; set; }

        [NotMapped]
        public IFormFile? selfie_Image { get; set; }

    }
    public class common
    {
        
        public FileUploadAPI _fileAPI { get; set; }
        public KYCFormModel _KYCForm { get; set; }
        public List<KYCFormModel> KYCForms { get; set; }
    }
}*/