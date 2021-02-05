using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Social_Network.API.Models.UsersInfo;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")] 
    public class UserController : ControllerBase
    {
        
        [HttpGet]
        [Route("get-user-data")]
        [Authorize(Policy = RoleInfo.User)]
        public IActionResult GetUserData()
        {
            return this.Ok("This is an normal user");
        }

        [HttpGet]
        [Route("get-admin-data")]
        [Authorize(Policy = RoleInfo.Admin)]
        public IActionResult GetAdminData()
        {
            return this.Ok("This is an admin user");
        }
    }
}