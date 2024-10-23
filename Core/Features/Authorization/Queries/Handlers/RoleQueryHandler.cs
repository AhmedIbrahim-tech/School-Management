using Core.Features.Authorization.Queries.Requests;
using Microsoft.AspNetCore.Identity;

namespace Core.Features.Authorization.Queries.Handlers;

public class RoleQueryHandler : GenericBaseResponseHandler,
   IRequestHandler<GetRolesListQuery, GenericBaseResponse<List<GetRolesListResult>>>,
   IRequestHandler<GetRoleByIdQuery, GenericBaseResponse<GetRoleByIdResult>>,
   IRequestHandler<ManageUserRolesQuery, GenericBaseResponse<ManageUserRolesResult>>
{
    #region Fields
    private readonly IAuthorizationServiceAsync _authorizationService;
    private readonly IMapper _mapper;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    private readonly UserManager<User> _userManager;
    #endregion

    #region Constructors
    public RoleQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                            IAuthorizationServiceAsync authorizationService,
                            IMapper mapper,
                            UserManager<User> userManager) : base(stringLocalizer)
    {
        _authorizationService = authorizationService;
        _mapper = mapper;
        _stringLocalizer = stringLocalizer;
        _userManager = userManager;
    }
    #endregion

    #region Handle Functions
    
    #region Get List of Roles
    public async Task<GenericBaseResponse<List<GetRolesListResult>>> Handle(GetRolesListQuery request, CancellationToken cancellationToken)
    {
        var roles = await _authorizationService.GetRolesList();
        var result = _mapper.Map<List<GetRolesListResult>>(roles);
        return Success(result);
    }

    #endregion

    #region Get Role by Id
    public async Task<GenericBaseResponse<GetRoleByIdResult>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _authorizationService.GetRoleById(request.Id);
        if (role == null) return NotFound<GetRoleByIdResult>(_stringLocalizer[SharedResourcesKeys.RoleNotExist]);
        var result = _mapper.Map<GetRoleByIdResult>(role);
        return Success(result);
    }


    #endregion

    #region Manage User Roles
    public async Task<GenericBaseResponse<ManageUserRolesResult>> Handle(ManageUserRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return NotFound<ManageUserRolesResult>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
        var result = await _authorizationService.ManageUserRolesData(user);
        return Success(result);
    }

    #endregion    
    
    #endregion
}