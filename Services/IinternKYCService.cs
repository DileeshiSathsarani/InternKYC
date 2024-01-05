using internKYC.DTOs.Requests;
using internKYC.DTOs.Responses;
using internKYC.DTOs;
using internKYC.Models;


    namespace internKYC.Services
    {
        public interface IinternKYCService
        {
            BaseResponse ContactNumber(ContactNumberRequest request);

            BaseResponse RegisterKYCForm(RegisterKYCFormRequest request);

            BaseResponse UploadDocument(UploadDocumentRequest request);

            BaseResponse GenerateOTP(VerifyOTPRequest request);

            BaseResponse VerifyOtp(VerifyOTPRequest request);

            BaseResponse ResendOtp(ResendOTPRequest request);

            List<KYCFormResponse> GetKYCForms(int page, int pageSize);
            
            void SaveChanges();
    }
    }
