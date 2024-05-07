using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using TomskNipi.DevelopProgress.DataAccess;
using System.Security.Principal;

namespace TomskNipi.DevelopProgress.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    [Authorize]

    public class UserController : ControllerBase
    {
        [HttpGet("userinfo")]

        public IActionResult GetUserInfo()
        {
            var userName = User.Identity.Name;
            ActiveDirectoryRepository activeDirectoryRepository = new ActiveDirectoryRepository();
            var nameParts = userName.Split('\\');
            var name = activeDirectoryRepository.FindUserFullName(nameParts[1].ToLower());

            return Ok(new { FullName = name });
        }
    }
}

