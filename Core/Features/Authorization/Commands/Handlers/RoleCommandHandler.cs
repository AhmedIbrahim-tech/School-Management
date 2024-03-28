namespace Core.Features.Authorization.Commands.Handlers;

public class RoleCommandHandler : GenericBaseResponseHandler,
    IRequestHandler<AddRoleCommand, GenericBaseResponse<string>>,
    IRequestHandler<EditRoleCommand, GenericBaseResponse<string>>,
    IRequestHandler<DeleteRoleCommand, GenericBaseResponse<string>>,
    IRequestHandler<UpdateUserRolesCommand, GenericBaseResponse<string>>
{
    #region Fields
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly IAuthorizationService _authorizationService;

    #endregion

    #region Constructors
    public RoleCommandHandler(IStringLocalizer<SharedResources> stringLocalizer,
                              IAuthorizationService authorizationService) : base(stringLocalizer)
    {
        _stringLocalizer = stringLocalizer;
        _authorizationService = authorizationService;
    }

    #endregion

    #region Handle Functions
    
    #region Create Role
    public async Task<GenericBaseResponse<string>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.AddRoleAsync(request.RoleName);
        if (result == "Success") return Success("");
        return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.AddFailed]);
    }
    #endregion

    #region Edit Role
    public async Task<GenericBaseResponse<string>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.EditRoleAsync(request);
        if (result == "notFound") return NotFound<string>();
        else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Updated]);
        else
            return BadRequest<string>(result);
    }
    #endregion

    #region Delete Role
    public async Task<GenericBaseResponse<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.DeleteRoleAsync(request.Id);
        if (result == "NotFound") return NotFound<string>();
        else if (result == "Used") return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.RoleIsUsed]);
        else if (result == "Success") return Success((string)_stringLocalizer[SharedResourcesKeys.Deleted]);
        else
            return BadRequest<string>(result);
    }
    #endregion

    #region Update User Roles
    public async Task<GenericBaseResponse<string>> Handle(UpdateUserRolesCommand request, CancellationToken cancellationToken)
    {
        var result = await _authorizationService.UpdateUserRoles(request);
        switch (result)
        {
            case "UserIsNull": return NotFound<string>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
            case "FailedToRemoveOldRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToRemoveOldRoles]);
            case "FailedToAddNewRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToAddNewRoles]);
            case "FailedToUpdateUserRoles": return BadRequest<string>(_stringLocalizer[SharedResourcesKeys.FailedToUpdateUserRoles]);
        }
        return Success<string>(_stringLocalizer[SharedResourcesKeys.Success]);
    } 
    #endregion

    #endregion

}
