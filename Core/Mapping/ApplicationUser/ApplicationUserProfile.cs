using Core.Features.ApplicationUser.Commands.Requests;
using Data.Entities.Identities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Mapping.ApplicationUser;

public class ApplicationUserProfile : Profile
{
    public ApplicationUserProfile()
    {
        // Create User
        CreateMap<AddUserCommand, User>();

        // Edit User
        CreateMap<EditUserCommand, User>();

        // Pagination User
        CreateMap<User, GetUserPaginationReponse>();

        //Get User by Id 
        CreateMap<User, GetUserByIdResponse>();
    }

}
