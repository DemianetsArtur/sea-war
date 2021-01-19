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
using Microsoft.Extensions.Logging.AzureAppServices;

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
        private ILogger _log { get; }

        public UserAccountController(IConfiguration configuration, 
                                     IMapper mapper, 
                                     IUserAccountService userAccountService, 
                                     IBlobStorageService blobStorageService,
                                     ILoggerFactory loggerFactory)
        {
            this._configuration = configuration;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
            this._blobStorageService = blobStorageService;
            this._log = loggerFactory.CreateLogger("User-Account-Logger");
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
                this._log.LogInformation("Request Login");
                return this.Ok(new { token = tokenString, userDetails = userFind });
            }
            else
            {
                return this.BadRequest(model);
            }
        }

        [HttpPost]
        [Route("register")]
        [AllowAnonymous]
        public IActionResult UserAccountRegister([FromBody] RegisterViewModel model)
        {
            this._log.LogDebug("Request Register DEBUG");
            this._log.LogError("Request Register erroe");
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

        [HttpGet("user-get/{name}")]
        public IActionResult UserGet(string name)
        {
            Task.Delay(2000);
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
                this._log.LogInformation("Request Get Blob");
                return this.Ok(data);
            }
        }
    }
}
