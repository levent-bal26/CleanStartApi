using Microsoft.AspNetCore.Mvc;

namespace CleanStartApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Merhaba Levent, Web API çalışıyor.";
        }
    }
}
