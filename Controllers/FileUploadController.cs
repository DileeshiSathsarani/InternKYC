using internKYC.DTOs.Responses;
using internKYC.Models;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.EntityFrameworkCore;

namespace internKYC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _applicationDbContext;
        public FileUploadController(IWebHostEnvironment webHostEnvironment, ApplicationDbContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _applicationDbContext = dbContext;
        }

        [HttpPost("UploadImage")]
        public BaseResponse Post([FromBody] ImageModel images)
        {
            BaseResponse response = new BaseResponse();

            try
            {
                SaveImage(images.Base64NICFrontImage, "Base64NICFrontImage" , images.AdditionalString);
                SaveImage(images.Base64NICBackImage, "Base64NICBackImage", images.AdditionalString);
                SaveImage(images.Base64SelfieImage, "Base64SelfieImage" , images.AdditionalString);

                response.CreateResponse(HttpStatusCode.OK, new { status = "Upload Success" });
            }
            catch (Exception ex)
            {
                response.CreateResponse(HttpStatusCode.InternalServerError, new { Status = false, Message = "Something went wrong. Please try again later." });
            }

            return response;
        }

        private void SaveImage(string base64Image, string imageType, string additionalString)
        {
            if (!string.IsNullOrEmpty(base64Image))
            {
                string path = Path.Combine(_webHostEnvironment.WebRootPath, "Upload");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (base64Image.Contains("data:image"))
                {
                    base64Image = base64Image.Substring(base64Image.LastIndexOf(',') + 1);
                }

                byte[] imageBytes = Convert.FromBase64String(base64Image);

                using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
                {
                    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
                    string imgPath = Path.Combine(path, $"{Guid.NewGuid()}.jpg");
                    image.Save(imgPath, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
        }
    }
}

