using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social_Network.API.Infrastructure.ViewModels.Edit;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;
using System;
using System.Threading.Tasks;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        private readonly IBlobStorageService _blobStorageService;
        private readonly IEditService _editService;

        public EditController(IMapper mapper, 
                              IUserAccountService userAccountService, 
                              IBlobStorageService blobStorageService, 
                              IEditService editService)
        {
            this._mapper = mapper;
            this._userAccountService = userAccountService;
            this._blobStorageService = blobStorageService;
            this._editService = editService;
        }
        
        [HttpPost]
        [Route("user-edit")]
        public IActionResult UserEdit([FromBody] EditViewModel model )
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }
            var userMapper = this._mapper.Map<UserAccountDto>(model);
            this._userAccountService.UserChangedCreate(userMapper);
            return this.Ok();
        }

        [HttpPost]
        [Route("user-profile-edit")]
        public async Task<IActionResult> UserProfileEdit([FromForm] EditProfileViewModel model) 
        {
            var nameFile = model.Name + Guid.NewGuid().ToString();

            if (!ModelState.IsValid) 
            {
                this.BadRequest(ModelState);
            }

            if (model.Content != null) 
            {
                await this._editService.FileRemoveToBlobAsync(model.ContentName);
                var fileInfo = await this._editService.FileUploadToBlobAsync(model.Content.OpenReadStream(), model.Content.ContentType, nameFile);
                model.ImagePath = fileInfo.AbsoluteUri;
                model.ContentName = nameFile; 
            }

            var userMapper = this._mapper.Map<UserAccountDto>(model);
            this._editService.UserProfileEdit(userMapper);
            return this.Ok();
        }
    }
}