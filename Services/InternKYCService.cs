using System.Net.NetworkInformation;
using internKYC.Models;
using internKYC.DTOs;
using internKYC.DTOs.Requests;
using internKYC.DTOs.Responses;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Http;
using internKYC.Services;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Data;


namespace internKYC.Services
{
    public class InternKYCService : IinternKYCService
    {
        private readonly ApplicationDbContext context;
        private readonly IOtpService otpService;

        public InternKYCService(DbContextOptions<ApplicationDbContext> dbContextOptions, IOtpService otpService)
        {
            context = new ApplicationDbContext(dbContextOptions);
            this.otpService = otpService;
        }

        public BaseResponse ContactNumber(ContactNumberRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
               
                string generatedOtp = otpService.GenerateOtp(); 

                var contactNumberModel = new ContactNumberModel
                {
                    contact_number = request.contact_number,
                    otp = generatedOtp
                };

                var existingRecord = context.ContactNumbers.FirstOrDefault(cn => cn.contact_number == request.contact_number);

                if (existingRecord != null)
                {
                    existingRecord.otp = generatedOtp;
                }
                else
                {
                    context.ContactNumbers.Add(contactNumberModel);
                }

                context.SaveChanges();

                response.status_code = StatusCodes.Status200OK;
                response.data = new { message = "OTP is successfully generated." };
            }
            catch (Exception ex)
            {
                response.status_code = StatusCodes.Status500InternalServerError;
                response.data = new { message = "Internal server error: " + ex.Message };
            }

            return response;
        }



        public BaseResponse GenerateOTP(VerifyOTPRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (string.IsNullOrEmpty(request.otp))
                {
                    response.status_code = StatusCodes.Status400BadRequest;
                    response.data = new { message = "OTP is required." };
                }
                else
                {
                    string otp = otpService.GenerateOtp();
                    SaveOTPToDatabase(request.otp, otp);

                    response.status_code = StatusCodes.Status200OK;
                    response.data = new { message = "OTP saved successfully", otp };
                }

                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Error generating OTP: " + ex.Message }
                };
            }
        }

            public BaseResponse VerifyOtp(VerifyOTPRequest request)
            {
                BaseResponse response = new BaseResponse();

                try
                {
                    bool isOtpValid = otpService.VerifyOtp(request.contact_number, request.otp);

                    if (isOtpValid)
                    {
                        response.status_code = StatusCodes.Status200OK;
                        response.data = new { message = "OTP is valid." };
                    }
                    else
                    {
                        response.status_code = StatusCodes.Status400BadRequest;
                        response.data = new { message = "Invalid OTP." };
                    }

                    return response;
                }
                catch (Exception ex)
                {
                    return new BaseResponse
                    {
                        status_code = StatusCodes.Status500InternalServerError,
                        data = new { message = "Error verifying OTP: " + ex.Message }
                    };
                }
            }


        public BaseResponse ResendOtp(ResendOTPRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                otpService.ResendOtp(request.contact_number);

                response.status_code = StatusCodes.Status200OK;
                response.data = new { message = "OTP resent successfully." };

                return response;
            }
            catch (Exception ex)
            {
                return new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Error resending OTP: " + ex.Message }
                };
            }
        }


        private void SaveOTPToDatabase(string contactNumber, string otp)
        {
            var existingRecord = context.ContactNumbers.FirstOrDefault(cn => cn.contact_number == contactNumber);

            if (existingRecord != null)
            {
                existingRecord.otp = otp;
            }
            else
            {
                var newRecord = new ContactNumberModel
                {
                    contact_number = contactNumber,
                    otp = otp
                };

                context.ContactNumbers.Add(newRecord);
            }

            context.SaveChanges();
        }


        public BaseResponse RegisterKYCForm(RegisterKYCFormRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (string.IsNullOrEmpty(request.NICNumber) || string.IsNullOrEmpty(request.mobile_number))
                {
                    response.status_code = StatusCodes.Status400BadRequest;
                    response.data = new { message = "NICNumber and MobileNumber are required." };
                    return response;
                }

                if (context.KYCForms.Any(f => f.NICNumber == request.NICNumber))
                {
                    response.status_code = StatusCodes.Status400BadRequest;
                    response.data = new { message = "NICNumber is already registered." };
                    return response;
                }

                KYCFormModel kycForm = new KYCFormModel();
                {
                    
                    kycForm.title = request.title;
                    kycForm.full_name = request.full_name;
                    kycForm.mobile_number = request.mobile_number;
                    kycForm.email = request.email;
                    string UpdateStatus = "Submit";
                    kycForm.Status = UpdateStatus;
                    kycForm.NICNumber = request.NICNumber;
                    kycForm.nationality = request.nationality;
                    kycForm.created_at = DateTime.UtcNow;
                    kycForm.updated_at = DateTime.UtcNow;

                    using (context)
                    {
                        context.Add(kycForm);
                        context.SaveChanges();
                    }


                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status200OK,
                        data = new { message = "Successfully registered KYC form." },

                };

                    if (response.status_code == StatusCodes.Status200OK)
                    {
                        kycForm.UpdateStatus("Submit");
                    }
                    return response;
                }
            }

            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error :" + ex.Message }
                };
                return response;
            }
        }

        public BaseResponse UploadDocument(UploadDocumentRequest request)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                if (request.id <= 0 || string.IsNullOrEmpty(request.doc_type) )
                {
                    response.status_code = StatusCodes.Status400BadRequest;
                    response.data = new { message = "id, DocType, and Path are required." };
                }
                else
                {
                    var kycForm = context.KYCForms.Find(request.id);
                    if (kycForm == null)
                    {
                        response.status_code = StatusCodes.Status404NotFound;
                        response.data = new { message = "KYC form not found." };
                    }
                    else
                    {
                        var document = new DocumentModel
                        {
                            doc_type = request.doc_type,
                            path  = request.path,
                            created_at = DateTime.UtcNow,
                            updated_at = DateTime.UtcNow
                        };

                        kycForm.Documents.Add(document);
                        context.SaveChanges();

                        response.status_code = StatusCodes.Status200OK;
                        response.data = new { message = "Document uploaded successfully." };
                    }
                }
            }
            catch (Exception ex)
            {
                response.status_code = StatusCodes.Status500InternalServerError;
                response.data = new { message = "Internal server error: " + ex.Message };
            }

            return response;
        }

        public List<KYCFormResponse> GetKYCForms(int page, int pageSize)
        {
            var skipAmount = (page - 1) * pageSize;

           
            var kycForms = context.KYCForms
                .Include(f => f.Documents)
                .OrderByDescending(f => f.created_at)
                .Skip(skipAmount)
                .Take(pageSize)
                .ToList();

           
            return kycForms.Select(f => new KYCFormResponse
            {
                id = f.id,
                title = f.title,
                full_name = f.full_name,
                mobile_number = f.mobile_number,
                email = f.email,
                NICNumber = f.NICNumber,
                nationality = f.nationality,
                created_at = f.created_at,
                updated_at = f.updated_at,
                Documents = f.Documents.Select(d => new DocumentResponse
                {
                    doc_id = d.doc_id,
                    doc_type = d.doc_type,
                    path = d.path,
                    created_at = d.created_at,
                    updated_at = d.updated_at
                }).ToList()
            }).ToList();
        }

   
    public void SaveChanges()
    {
        context.SaveChanges();
    }
    }
}
