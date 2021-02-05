using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Social_Network.API.Infrastructure.Manages.JwtManage;
using Social_Network.BLL.ModelsDto;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Social_Network.API.Infrastructure.ViewModels.UserAccount;
using Social_Network.BLL.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Social_Network.API.Infrastructure.Manages.MailSender;
using System;
using Social_Network.BLL.Models;
using System.Globalization;
using Social_Network.API.Models.RequestInfo;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IMailSender _mailSender;
        private ILogger _log { get; }

        public UserAccountController(IConfiguration configuration, 
                                     IMapper mapper, 
                                     IUserAccountService userAccountService, 
                                     IBlobStorageService blobStorageService,
                                     ILoggerFactory loggerFactory,
                                     IMailSender mailSender)
        {
            this._configuration = configuration;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
            this._blobStorageService = blobStorageService;
            this._log = loggerFactory.CreateLogger("User-Account-Logger");
            this._mailSender = mailSender;
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

            this._log.LogInformation("Request Login");
            var userMapper = this._mapper.Map<UserAccountDto>(model);
            var userFind = this._userAccountService.UserAccountLoginFind(userMapper);

            if (userFind == null)
            {
                return this.StatusCode(RequestStatusInfo.Status401);
            }
            else if (userFind != null 
                  && this._userAccountService.TokenExpired(userFind.EmailDateKey)
                  && userFind.EmailConfirmed == false)
            {
                var token = Guid.NewGuid().ToString();
                var dateFormat = DateTimeFormatInfo.InvariantInfo;
                var date = DateTime.Now.ToString(OptionsInfo.DateConfig, dateFormat);
                
                userFind.EmailKey = token;
                userFind.EmailDateKey = date;
                this._mailSender.SendEmail(userFind);
                this._userAccountService.UserChangedCreate(userFind);
                return this.StatusCode(RequestStatusInfo.Status402);
            }
            else if (userFind != null && userFind.EmailConfirmed == false)
            {
                return this.StatusCode(RequestStatusInfo.Status403);
            }
            
            else if (userFind != null && userFind.EmailConfirmed == true)
            {
                var tokenString = JwtManage.GenerateJwt(userFind, this._configuration);
                return this.Ok(new { token = tokenString, userDetails = userFind });
            }
            else {
                return this.BadRequest();
            }
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult UserAccountRegister([FromBody] RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var userMapper = this._mapper.Map<UserAccountDto>(model);
            if (!this._userAccountService.UserAccountFind(userMapper))
            {
                
                return this.BadRequest(model);
            }

            var token = Guid.NewGuid().ToString();
            userMapper.EmailKey = token;
            

            this._mailSender.SendEmail(userMapper);
            this._userAccountService.UserAccountCreate(userMapper);

            return this.Ok();
        }

        [HttpGet("user-get/{name}")]
        public IActionResult UserGet(string name)
        {
            var user = this._userAccountService.GetUser(name);
            if (user != null)
            {
                this._log.LogInformation("Request UserGet Success");
                return this.Ok(user);
            }
            else
            {
                this._log.LogInformation("Request UserGet Not Successful");
                return this.BadRequest();
            }
        }

        [HttpPost, DisableRequestSizeLimit]
        [Produces("application/json")]
        [Route("image-upload")]
        public async Task<ActionResult> FileUploadToStorage()
        {
            IFormFile file = Request.Form.Files[0];
            if (file == null)
            {
                return BadRequest();
            }
            var fileInfo = await this._blobStorageService.FileUploadToBlobAsync(file.OpenReadStream(), 
                                                                                         file.ContentType, 
                                                                                         file.FileName);
            
            this._userAccountService.UserAccountReplace(file.FileName, fileInfo.AbsoluteUri);
            this._log.LogInformation("Request Upload Image");
            return this.Ok();
        }

        [HttpGet("getfile/{fileName}")]
        public async Task<IActionResult> GetBlob(string fileName)
        {
            var data = await this._blobStorageService.GetFileFromBlobAsync(fileName);
            if (string.IsNullOrWhiteSpace(fileName) || data == null)
            {
                return this.BadRequest();
            }
            else
            {
                return this.Ok(data);
            }
        }

        [HttpGet("email-confirmation")]
        public IActionResult EmailConfirmation([FromQuery] string email, 
                                               [FromQuery] string token) 
        {
            var emailConfirm = this._userAccountService.EmailConfirmation(token);
            if (emailConfirm == null)
            {
                return this.Ok(RequestStatusInfo.UnsuccessContifmEmailText);
            }
            else 
            {
                return this.Ok(RequestStatusInfo.SuccessContifmEmailText);
            }
            
        }
    }
}
