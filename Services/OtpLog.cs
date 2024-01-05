using internKYC.Models;

namespace internKYC.Services
{
    internal class OtpLog : ContactNumberModel
    {
        public string ContactNumber { get; set; }
        public string Otp { get; set; }
        public DateTime SentAt { get; set; }
    }
}