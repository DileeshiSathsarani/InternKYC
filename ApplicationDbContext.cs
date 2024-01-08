
using Microsoft.EntityFrameworkCore;
using internKYC.Models;
using System.Reflection.Metadata;
using internKYC.Services;

namespace internKYC
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ContactNumberModel> ContactNumbers { get; set; }

        public DbSet<KYCFormModel> KYCForms { get; set; }
        public DbSet<DocumentModel> Documents { get; set; }

        //public DbSet<ImageModel> Files { get; set; }

        public DbSet<ContactNumberModel> OtpLogs { get; set; }

    }
}
