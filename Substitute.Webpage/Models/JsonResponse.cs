using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Substitute.Webpage.Models
{
    public class JsonResponse
    {
        public JsonResponse()
        {
        }

        public JsonResponse(object data)
        {
            Success = true;
            Data = data;
        }

        public bool Success { get; set; }
        public object Data { get; set; }
        public string ErrorMessage { get; set; }

        public static JsonResponse FromException(Exception ex)
        {
            return new JsonResponse
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
