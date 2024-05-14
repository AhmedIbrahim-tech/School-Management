using Azure;
using Core.Features.Authorization.Queries.Requests;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.Authorization.Queries.Handlers;

public class ClaimsQueryHandler : GenericBaseResponseHandler,
    IRequestHandler<ManageUserClaimsQuery, GenericBaseResponse<ManageUserClaimsResult>>
{
    #region Fileds
    private readonly IAuthorizationService _authorizationService;
    private readonly UserManager<User> _userManager;
    private readonly IStringLocalizer<SharedResources> _stringLocalizer;
    #endregion
    #region Constructors
    public ClaimsQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                              IAuthorizationService authorizationService,
                              UserManager<User> userManager) : base(stringLocalizer)
    {
        _authorizationService = authorizationService;
        _userManager = userManager;
        _stringLocalizer = stringLocalizer;
    }
    #endregion
    #region Handle Functions
    public async Task<GenericBaseResponse<ManageUserClaimsResult>> Handle(ManageUserClaimsQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.UserId.ToString());
        if (user == null) return NotFound<ManageUserClaimsResult>(_stringLocalizer[SharedResourcesKeys.UserIsNotFound]);
        var result = await _authorizationService.ManageUserClaimData(user);
        return Success(result);
    }
    #endregion
}
