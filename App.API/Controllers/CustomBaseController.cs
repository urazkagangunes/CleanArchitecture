using App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomBaseController : ControllerBase
    {
        [NonAction]
        public IActionResult CreateActionResult<T>(ServiceResult<T> serviceResult)
        {
            if (serviceResult.StatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = serviceResult.StatusCode.GetHashCode() };
            }

            return new ObjectResult(serviceResult) { StatusCode = serviceResult.StatusCode.GetHashCode() };
        }

        [NonAction]
        public IActionResult CreateActionResult(ServiceResult serviceResult)
        {
            if (serviceResult.StatusCode == HttpStatusCode.NoContent)
            {
                return new ObjectResult(null) { StatusCode = serviceResult.StatusCode.GetHashCode() };
            }

            return new ObjectResult(serviceResult) {StatusCode = serviceResult.StatusCode.GetHashCode() };
        }
    }
}
