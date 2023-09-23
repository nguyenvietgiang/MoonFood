using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace MoonFood.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/statistical")]
    [ApiVersion("2.0")]
    [Authorize(Roles = "Manager")]
    public class StatisticalV2Controller : ControllerBase
    {
    }
}
