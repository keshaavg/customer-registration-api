using Microsoft.AspNetCore.Mvc;

namespace CustomerRegistration.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// This  Error action sends an RFC 7807-compliant payload to the client.
        /// </summary>
        /// <returns><see cref="IActionResult"/></returns>
        [Route("/error")]
        [HttpGet]
        public IActionResult Error() => Problem();
    }
}
