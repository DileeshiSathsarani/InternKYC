using internKYC.DTOs.Requests;
using internKYC.DTOs.Responses;
using internKYC.Models;
using internKYC.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class ContactNumberController : ControllerBase
{
    private readonly IinternKYCService InternKYCService;

    public ContactNumberController(IinternKYCService InternKYCService)
    {
        this.InternKYCService = InternKYCService;
    }

    [HttpPost("ContactNumber")]
    public BaseResponse ContactNumber(ContactNumberRequest request)
    {
        return InternKYCService.ContactNumber(request);
    }
    

    [HttpPost("VerifyOTP")]
    public BaseResponse VerifyOTP(VerifyOTPRequest request)
    {
        return InternKYCService.VerifyOtp(request);
    }

    [HttpPost("ResendOTP")]
    public BaseResponse ResendOTP(ResendOTPRequest request)
    {
        return InternKYCService.ResendOtp(request);
        
    }
}