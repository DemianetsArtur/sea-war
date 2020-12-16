using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Social_Network.API.Infrastructure.Manages.JwtManage;
using Social_Network.BLL.ModelsDto;
using AutoMapper;
using Social_Network.API.Infrastructure.ViewModels.UserAccount;
using Social_Network.BLL.Infrastructure.Interfaces;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;

        public UserAccountController(IConfiguration configuration, 
                                     IMapper mapper, IUserAccountService userAccountService)
        {
            this._configuration = configuration;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
        }
        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public IActionResult UserAccountLogin([FromBody] LoginViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userMapper = this._mapper.Map<UserAccountDto>(model);
            var userFind = this._userAccountService.UserAccountLoginFind(userMapper);
            if (userFind != null)
            {
                var tokenString = JwtManage.GenerateJwt(userFind, this._configuration);
                return this.Ok(new { token = tokenString, userDetails = userFind });
            }
            else
            {
                return this.BadRequest(model);
            }
        }

        [HttpPost]
        [Route("register")]
        public IActionResult UserAccountRegister([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userMapper = this._mapper.Map<UserAccountDto>(model);
            if (this._userAccountService.UserAccountFind(userMapper))
            {
                this._userAccountService.UserAccountCreate(userMapper);
                return this.Ok();
            }
            else
            {
                return this.BadRequest(model);
            }
        }
    }
}
