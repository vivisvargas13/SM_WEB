using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using SM_API.Entities;

namespace SM_API.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("CatchException")]

        public IActionResult CatchException()
        {
            var context = new HttpContext.Features.Get<IExceptionHandlerFeature>();

            //llamar a un procedimiento que guarde la excepcion

            return Problem(detail:"", title: "");

        }
    }
}
