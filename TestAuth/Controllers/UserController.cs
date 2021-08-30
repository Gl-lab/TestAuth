using Microsoft.AspNetCore.Mvc;

namespace TestAuth.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        [Route("UserName")]
        public string UserName()
        {
            if (HttpContext.User.Identity != null)
            {
                if (!string.IsNullOrEmpty(HttpContext.User.Identity.Name))
                {
                    return HttpContext.User.Identity.Name;
                }
            }
            return "User not found";
        }
    }
}