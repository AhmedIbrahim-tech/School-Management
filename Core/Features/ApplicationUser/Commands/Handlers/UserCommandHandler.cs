using Azure;
using Core.Features.ApplicationUser.Commands.Requests;
using Data.Entities.Identities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.ApplicationUser.Commands.Handlers
{
    public class UserCommandHandler : GenericBaseResponseHandler,
        IRequestHandler<AddUserCommand, GenericBaseResponse<string>>,
        IRequestHandler<EditUserCommand, GenericBaseResponse<string>>,
        IRequestHandler<DeleteUserCommand, GenericBaseResponse<string>>,
        IRequestHandler<ChangeUserPasswordCommand, GenericBaseResponse<string>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        //private readonly IHttpContextAccessor _httpContextAccessor;
        //private readonly IEmailsService _emailsService;
        private readonly IApplicationUserService _applicationUserService;
        #endregion

        #region Constructors
        public UserCommandHandler(
            IStringLocalizer<SharedResources> stringLocalizer, 
            IMapper mapper, 
            UserManager<User> userManager, 
            IApplicationUserService applicationUserService) : base(stringLocalizer)
        //IHttpContextAccessor httpContextAccessor,
        //IEmailsService emailsService,
        {
            _sharedResources = stringLocalizer;
            _mapper = mapper;
            _userManager = userManager;
            //_httpContextAccessor = httpContextAccessor;
            //_emailsService = emailsService;
            _applicationUserService = applicationUserService;
        }


        #endregion

        #region Handle Functions
        
        #region Create User
        public async Task<GenericBaseResponse<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var identityUser = _mapper.Map<User>(request);
            //Create
            var createResult = await _applicationUserService.AddUserAsync(identityUser, request.Password);
            switch (createResult)
            {
                case "EmailIsExist": return BadRequest<string>(_sharedResources[SharedResourcesKeys.EmailIsExist]);
                case "UserNameIsExist": return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserNameIsExist]);
                case "ErrorInCreateUser": return BadRequest<string>(_sharedResources[SharedResourcesKeys.FaildToAddUser]);
                case "Failed": return BadRequest<string>(_sharedResources[SharedResourcesKeys.TryToRegisterAgain]);
                case "Success": return Success<string>("");
                default: return BadRequest<string>(createResult);
            }
        }
        #endregion

        #region Edit User
        public async Task<GenericBaseResponse<string>> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var oldUser = await _userManager.FindByIdAsync(request.Id.ToString());

            //if Not Exist notfound
            if (oldUser == null) return NotFound<string>();

            //mapping
            var newUser = _mapper.Map(request, oldUser);

            //if username is Exist
            var userByUserName = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == newUser.UserName && x.Id != newUser.Id);

            //username is Exist
            if (userByUserName != null) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UserNameIsExist]);

            //update
            var result = await _userManager.UpdateAsync(newUser);

            //result is not success
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.UpdateFailed]);

            //message
            return Success((string)_sharedResources[SharedResourcesKeys.Updated]);
        }
        #endregion

        #region Delete User
        public async Task<GenericBaseResponse<string>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            //if Not Exist notfound
            if (user == null) return NotFound<string>();
            //Delete the User
            var result = await _userManager.DeleteAsync(user);
            //in case of Failure
            if (!result.Succeeded) return BadRequest<string>(_sharedResources[SharedResourcesKeys.DeletedFailed]);
            return Success((string)_sharedResources[SharedResourcesKeys.Deleted]);
        }
        #endregion

        #region Change Password
        public async Task<GenericBaseResponse<string>> Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
        {
            //get user
            //check if user is exist
            var user = await _userManager.FindByIdAsync(request.Id.ToString());

            //if Not Exist notfound
            if (user == null) return NotFound<string>();

            //Change User Password
            var result = await _userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);
            //var user1=await _userManager.HasPasswordAsync(user);
            //await _userManager.RemovePasswordAsync(user);
            //await _userManager.AddPasswordAsync(user, request.NewPassword);

            //result
            if (!result.Succeeded) return BadRequest<string>(result.Errors.FirstOrDefault().Description);
            return Success((string)_sharedResources[SharedResourcesKeys.Success]);
        } 
        #endregion

        #endregion
    }
}
