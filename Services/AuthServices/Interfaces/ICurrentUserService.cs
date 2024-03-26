using Data.Entities.Identities;

namespace Services.AuthServices.Interfaces;

public interface ICurrentUserService
{
    public Task<User> GetUserAsync();
    public int GetUserId();
    public Task<List<string>> GetCurrentUserRolesAsync();
}
