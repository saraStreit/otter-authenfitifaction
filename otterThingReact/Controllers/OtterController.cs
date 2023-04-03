using Microsoft.AspNetCore.Mvc;

namespace otterThingReact.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OtterController : ControllerBase
    {
        public OtterController(ILogger<OtterController> logger)
        {
        }

        [HttpGet]
        public Otter Get(Otter otter)
        {
            return new Otter();
        }
           
    }
}