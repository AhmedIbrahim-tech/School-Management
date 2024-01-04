using Core.Features.ApplicationUser.Queries.Requests;
using Data.Entities.Identities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Features.ApplicationUser.Queries.Handlers
{
    public class UserQueryHandler : GenericBaseResponseHandler, 
        IRequestHandler<GetUserPaginationQuery, PaginationResult<GetUserPaginationReponse>>,
        IRequestHandler<GetUserByIdQuery, GenericBaseResponse<GetUserByIdResponse>>
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResources> _sharedResources;
        private readonly UserManager<User> _userManager;
        #endregion

        #region Constructors
        public UserQueryHandler(IStringLocalizer<SharedResources> stringLocalizer,
                                  IMapper mapper,
                                  UserManager<User> userManager) : base(stringLocalizer)
        {
            _mapper = mapper;
            _sharedResources = stringLocalizer;
            _userManager = userManager;
        }
        #endregion                                                         

        #region Handle Functions

        #region Return List Pagination of User
        public async Task<PaginationResult<GetUserPaginationReponse>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
        {
            var users = _userManager.Users.AsQueryable();
            var paginatedUser = await _mapper.ProjectTo<GetUserPaginationReponse>(users)
                                                                .ToPaginationListAsync(request.PageNumber , request.PageSize);
            return paginatedUser;
        }
        #endregion

        #region Return User By Id
        public async Task<GenericBaseResponse<GetUserByIdResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            //var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id==request.Id);
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null) return NotFound<GetUserByIdResponse>(_sharedResources[SharedResourcesKeys.NotFound]);
            var result = _mapper.Map<GetUserByIdResponse>(user);
            return Success(result);

        }

        #endregion

        #endregion
    }
}
