using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Social_Network.API.Infrastructure.ViewModels.Edit;
using Social_Network.BLL.Infrastructure.Interfaces;
using Social_Network.BLL.ModelsDto;

namespace Social_Network.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserAccountService _userAccountService;
        private readonly IBlobStorageService _blobStorageService;

        public EditController(IMapper mapper, 
                              IUserAccountService userAccountService, 
                              IBlobStorageService blobStorageService)
        {
            this._mapper = mapper;
            this._userAccountService = userAccountService;
            this._blobStorageService = blobStorageService;
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
    }
}