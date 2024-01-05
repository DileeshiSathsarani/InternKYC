using System.Net;

namespace internKYC.DTOs.Responses
{
    public class BaseResponse
    {
        public int status_code { get; set; }
        public object data { get; set; }

        ///<summery> 
        ///</summery>
        ///<param name="statusCode"></param>
        ///<param name="data"></param>

        public void CreateResponse(HttpStatusCode statusCode, Object data)
        {
            status_code = (int)statusCode;
            this.data = data;
        }
    }
}
