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

        public UserAccountController(IConfiguration configuration, 
                                     IMapper mapper, 
                                     IUserAccountService userAccountService, 
                                     IBlobStorageService blobStorageService)
        {
            this._configuration = configuration;
            this._mapper = mapper;
            this._userAccountService = userAccountService;
            this._blobStorageService = blobStorageService;
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
                //ImageUpload.ImgUpload(model);
                return this.Ok();
            }
            else
            {
                return this.BadRequest(model);
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
            return this.Ok();
        }

        [HttpGet("getfile/{fileName}")]
        public async Task<IActionResult> GetBlob(string fileName)
        {
            var data = await this._blobStorageService.GetFileFromBlobAsync(fileName);
            //return File(data.Content, data.ContentType);
            if (string.IsNullOrWhiteSpace(fileName) || data == null)
            {
                return this.BadRequest();
            }
            else
            {
                return this.Ok(data);
            }
        }

    }
}
