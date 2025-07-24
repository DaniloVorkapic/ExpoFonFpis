using Backend.Http;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Utils
{
    public class ResultHandler
    {
        public static IActionResult HandleResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }

            return new BadRequestObjectResult(result.Error);
        }
    }
}
