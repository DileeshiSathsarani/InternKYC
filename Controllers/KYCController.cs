using System;
using internKYC.DTOs.Requests;
using internKYC.DTOs.Responses;
using internKYC.Models;
using internKYC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Twilio.TwiML.Voice;

namespace internKYC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KYCController : ControllerBase
    {
        private readonly IinternKYCService InternKYCService;

        public KYCController(IinternKYCService InternKYCService)
        {
            this.InternKYCService = InternKYCService;
        }


        [HttpPost("RegisterKYCForm")]
        public BaseResponse RegisterKYCForm(RegisterKYCFormRequest request)
        {
            return InternKYCService.RegisterKYCForm(request);
        }



        [HttpGet("GetKYCForms")]
        public List<KYCFormResponse> GetKYCForms(int page = 1, int pageSize = 10)
        {
            var kycForms = InternKYCService.GetKYCForms(page, pageSize);
            return kycForms;
        }

    }
}

