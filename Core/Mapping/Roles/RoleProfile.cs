namespace Core.Mapping.Roles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            // Mapping from Role entity to GetRolesListResult
            CreateMap<Role, GetRolesListResult>()
                .ReverseMap(); // If you want to support mapping in both directions
            
            // Mapping from Role entity to GetRoleByIdResult
            CreateMap<Role, GetRoleByIdResult>()
                .ReverseMap(); // If you need reverse mapping as well
        }
    }
}
