using Data.Helpers;
using Infrastructure.Context;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Services.Interface;
using System.Security.Claims;

namespace Services.Services;

#region Interface
public interface IAuthorizationServiceAsync
{
    public Task<string> AddRoleAsync(string roleName);
    public Task<bool> IsRoleExistByName(string roleName);
    public Task<string> EditRoleAsync(EditRoleRequest request);
    public Task<string> DeleteRoleAsync(int roleId);
    public Task<bool> IsRoleExistById(int roleId);
    public Task<List<Role>> GetRolesList();
    public Task<Role> GetRoleById(int id);
    public Task<ManageUserRolesResult> ManageUserRolesData(User user);
    public Task<string> UpdateUserRoles(UpdateUserRolesRequest request);
    public Task<ManageUserClaimsResult> ManageUserClaimData(User user);
    public Task<string> UpdateUserClaims(UpdateUserClaimsRequest request);
}

#endregion
public class AuthorizationServiceAsync : IAuthorizationServiceAsync
{
    #region Fields
    private readonly RoleManager<Role> _roleManager;
    private readonly UserManager<User> _userManager;
    private readonly ApplicationDBContext _dbContext;
    private readonly IUnitOfWork _unitOfWork;
    #endregion

    #region Constructors
    public AuthorizationServiceAsync(RoleManager<Role> roleManager,
                                UserManager<User> userManager,
                                ApplicationDBContext dbContext,
                                IUnitOfWork unitOfWork)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _dbContext = dbContext;
        _unitOfWork = unitOfWork;
    }


    #endregion

    #region handle Functions

    #region List of Role
    public async Task<List<Role>> GetRolesList()
    {
        return await _roleManager.Roles.ToListAsync();
    }
    #endregion

    #region GET: Role by Id
    public async Task<Role> GetRoleById(int id)
    {
        return await _roleManager.FindByIdAsync(id.ToString());
    }

    #endregion

    #region Create Role
    public async Task<string> AddRoleAsync(string roleName)
    {
        if (string.IsNullOrWhiteSpace(roleName))
        {
            return "Role name cannot be empty.";
        }

        var identityRole = new Role { Name = roleName };
        var result = await _roleManager.CreateAsync(identityRole);

        return result.Succeeded ? "Success" : "Failed: " + string.Join(", ", result.Errors);
    }

    #endregion

    #region Edit Role

    public async Task<string> EditRoleAsync(EditRoleRequest request)
    {
        // Validate request
        if (request == null || string.IsNullOrWhiteSpace(request.Name))
        {
            return "Invalid request. Role name cannot be empty.";
        }

        // Check if the role exists
        var role = await _roleManager.FindByIdAsync(request.Id.ToString());
        if (role == null)
        {
            return "Role not found.";
        }

        // Update the role
        role.Name = request.Name;
        var result = await _roleManager.UpdateAsync(role);

        if (result.Succeeded)
        {
            return "Success";
        }

        // Return list of errors if update failed
        return $"Failed to update role: {string.Join(", ", result.Errors)}";
    }

    #endregion

    #region Delete Role
    public async Task<string> DeleteRoleAsync(int roleId)
    {
        // Chech if this role exist or not
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null) return "NotFound";

        // Chech if user has this role or not
        var users = await _userManager.GetUsersInRoleAsync(role.Name);

        // return exception 
        if (users != null && users.Any()) return "Used";

        // delete this role
        var result = await _roleManager.DeleteAsync(role);

        //success
        if (result.Succeeded) return "Success";

        //problem
        return $"Failed to delete role: {string.Join(", ", result.Errors)}";
    }

    #endregion


    #region Manage User Roles
    public async Task<ManageUserRolesResult> ManageUserRolesData(User user)
    {
        var response = new ManageUserRolesResult
        {
            UserId = user.Id,
            userRoles = new List<UserRoles>()
        };

       // Get all roles using the role service (SRP)
        var roles = await _roleManager.Roles.ToListAsync();
        foreach (var role in roles)
        {
            var hasRole = await _userManager.IsInRoleAsync(user, role.Name);

            var userRole = new UserRoles
            {
                Id = role.Id,
                Name = role.Name,
                HasRole = hasRole
            };

            response.userRoles.Add(userRole);
        }

        return response;

    }
    #endregion

    #region Update User Roles
    public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
    {
        await _unitOfWork.BeginTransactionAsync();

        try
        {
            // Get the user
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return "UserNotFound";
            }

            // Get user's current roles
            var currentRoles = await _userManager.GetRolesAsync(user);

            // Remove all existing roles
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeRolesResult.Succeeded)
            {
                await _unitOfWork.RollbackAsync();
                return "FailedToRemoveOldRoles";
            }

            // Add new roles where HasRole = true
            var newRoles = request.userRoles
                .Where(x => x.HasRole)
                .Select(x => x.Name);

            var addRolesResult = await _userManager.AddToRolesAsync(user, newRoles);
            if (!addRolesResult.Succeeded)
            {
                await _unitOfWork.RollbackAsync();
                return "FailedToAddNewRoles";
            }

            await _unitOfWork.CommitAsync();
            return "Success";

        }
        catch (Exception)
        {
            await _unitOfWork.RollbackAsync();
            return "FailedToUpdateUserRoles";
        }
    }
    #endregion

    #region Manage User Claim

    public async Task<ManageUserClaimsResult> ManageUserClaimData(User user)
    {
        var response = new ManageUserClaimsResult();
        var usercliamsList = new List<UserClaims>();
        response.UserId = user.Id;
        //Get USer Claims
        var userClaims = await _userManager.GetClaimsAsync(user); //edit
                                                                  //create edit get print
        foreach (var claim in ClaimsStore.claims)
        {
            var userclaim = new UserClaims();
            userclaim.Type = claim.Type;
            if (userClaims.Any(x => x.Type == claim.Type))
            {
                userclaim.Value = true;
            }
            else
            {
                userclaim.Value = false;
            }
            usercliamsList.Add(userclaim);
        }
        response.userClaims = usercliamsList;
        //return Result
        return response;
    }
    #endregion

    #region Update User Claims
    public async Task<string> UpdateUserClaims(UpdateUserClaimsRequest request)
    {
        var transact = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return "UserIsNull";
            }
            //remove old Claims
            var userClaims = await _userManager.GetClaimsAsync(user);
            var removeClaimsResult = await _userManager.RemoveClaimsAsync(user, userClaims);
            if (!removeClaimsResult.Succeeded)
                return "FailedToRemoveOldClaims";
            var claims = request.userClaims.Where(x => x.Value == true).Select(x => new Claim(x.Type, x.Value.ToString()));

            var addUserClaimResult = await _userManager.AddClaimsAsync(user, claims);
            if (!addUserClaimResult.Succeeded)
                return "FailedToAddNewClaims";

            await transact.CommitAsync();
            return "Success";
        }
        catch (Exception ex)
        {
            await transact.RollbackAsync();
            return "FailedToUpdateClaims";
        }
    }
    #endregion
  
    #region Helper

    #region Check Role is Exists By Name
    public async Task<bool> IsRoleExistByName(string roleName)
    {
        //var role=await _roleManager.FindByNameAsync(roleName);
        //if(role == null) return false;
        //return true;
        return await _roleManager.RoleExistsAsync(roleName);
    }

    #endregion

    #region Check Role is Exists By Id
    public async Task<bool> IsRoleExistById(int roleId)
    {
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null) return false;
        else return true;
    }

    #endregion

    #endregion

    #endregion


}
