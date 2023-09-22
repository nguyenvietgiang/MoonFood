using HotChocolate.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoonFood.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/payment")]
    [ApiVersion("2.0")]
    [Authorize]
    public class PaymentController : ControllerBase
    {

    }
}
