using System;
using Twilio.Types;
using internKYC.Models;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using internKYC;

namespace internKYC.Services
{
    public class OtpService : IOtpService
    {
        private readonly ApplicationDbContext context;
        private readonly string US84150b94a541d43dcea4974ff4ff2cd7;
        private readonly string a90064dbf15f56a565ae8c58577d9;
        private readonly string XXXXX1830;

        public OtpService(ApplicationDbContext dbContext, string accountSid, string authToken, string contact_number)
        {
            context = dbContext;
            US84150b94a541d43dcea4974ff4ff2cd7 = accountSid;
            a90064dbf15f56a565ae8c58577d9 = authToken;
            XXXXX1830 = contact_number;

            TwilioClient.Init("US84150b94a541d43dcea4974ff4ff2cd7", "a90064dbf15f56a565ae8c58577d9");

        }
        public string GenerateOtp()
        {

            Random random = new Random();
            string otpCode = random.Next(10000, 99999).ToString();
            return otpCode;
        }

        public bool VerifyOtp(string contact_number, string otp)
        {

            var storedOtp = context.ContactNumbers
                                .Where(cn => cn.contact_number == contact_number)
                                .Select(cn => cn.otp)
                                .FirstOrDefault();

            bool isOtpValid = !string.IsNullOrEmpty(storedOtp) && otp == storedOtp;

            return isOtpValid;
        }


        public void ResendOtp(string contact_number)
        {

            string newOtp = GenerateOtp();


            var contactNumber = context.ContactNumbers.FirstOrDefault(cn => cn.contact_number == contact_number);

            if (contactNumber != null)
            {
                contactNumber.otp = newOtp;
                context.SaveChanges();
            }

            SendOtpViaSms(contact_number, newOtp);
        }

        public void SendOtpViaSms(string contact_number, string newOtp)
        {
            try
            {
                var to = new PhoneNumber(contact_number);
                var from = new PhoneNumber(XXXXX1830);

                var message = MessageResource.Create(
                    to: to,
                    from: from,
                    body: $"Your OTP is: {newOtp}"
                );

               
                var otpLog = new OtpLog
                {
                    ContactNumber = contact_number,
                    Otp = newOtp,
                    SentAt = DateTime.UtcNow
                };

                context.OtpLogs.Add(otpLog);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}


