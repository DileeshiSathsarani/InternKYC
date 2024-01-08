/*using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using internKYC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using Microsoft.EntityFrameworkCore;

namespace internKYC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;

        public ImageUploadcontroller(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpPost]
        public Task<common> Post([FromForm] FileUploadAPI objFile)
        {
            common obj = new common();
            obj.KYCForms = new List<KYCFormModel>();
            obj._fileAPI = new FileUploadAPI();

            try
            {
                if (!string.IsNullOrEmpty(objFile.KYCForms))
                {
                    if (objFile.KYCForms.StartsWith("["))
                    {
                        List<KYCFormModel> list = JsonConvert.DeserializeObject<List<KYCFormModel>>(objFile.KYCForms);
                        obj.KYCForms = list;
                    }
                    else
                    {

                        try
                        {
                            KYCFormModel singleItem = JsonConvert.DeserializeObject<KYCFormModel>(objFile.KYCForms);
                            obj.KYCForms.Add(singleItem);
                        }
                        catch (JsonSerializationException)
                        {

                        }
                    }
                }



                if (objFile.NIC_front_Image.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }

                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.NIC_front_Image.FileName))
                    {
                        objFile.NIC_front_Image.CopyTo(filestream);
                        filestream.Flush();
                    }
                }

                if (objFile.NIC_back_Image.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }

                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.NIC_back_Image.FileName))
                    {
                        objFile.NIC_back_Image.CopyTo(filestream);
                        filestream.Flush();
                    }
                }

                if (objFile.selfie_Image.Length > 0)
                {
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload"))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                    }

                    using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.selfie_Image.FileName))
                    {
                        objFile.selfie_Image.CopyTo(filestream);
                        filestream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }

            return Task.FromResult(obj);
        }

    
    }
}*/