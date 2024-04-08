namespace Core.Mapping.Roles;

public class RoleProfile : Profile
{
    public RoleProfile()
    {
        CreateMap<Role, GetRolesListResult>();
        CreateMap<Role, GetRoleByIdResult>();

    }
}
