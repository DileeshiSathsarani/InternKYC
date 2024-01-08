using System.ComponentModel.DataAnnotations;
using internKYC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

namespace internKYC.Models
{
    public class DocumentModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int doc_id { get; set; }
        public string doc_type { get; set; }
        public string? path { get; set; }
        public string Base64NICFrontImage { get; set; }
        public string Base64NICBackImage { get; set; }
        public string Base64SelfieImage { get; set; }
       
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }

       
    }

}
