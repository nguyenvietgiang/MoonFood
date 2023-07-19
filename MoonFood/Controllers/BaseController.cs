using Microsoft.AspNetCore.Mvc;

namespace MoonFood.Controllers
{ 
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetUserIdFromClaim()
        {
            var userIdClaim = User.FindFirst("Id");
            if (userIdClaim == null || !Guid.TryParse(userIdClaim.Value, out Guid Id))
                throw new UnauthorizedAccessException();
            return Id;
        }
    }
}
