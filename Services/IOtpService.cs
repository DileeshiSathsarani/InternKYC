namespace internKYC.Services
{
    public interface IOtpService
    {
        string GenerateOtp();
        bool VerifyOtp(string contact_number, string otp);
        void ResendOtp(string contact_number);
    }
}