using Data.Helpers;
using Infrastructure.Context;
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
    #endregion

    #region Constructors
    public AuthorizationServiceAsync(RoleManager<Role> roleManager,
                                UserManager<User> userManager,
                                ApplicationDBContext dbContext)
    {
        _roleManager = roleManager;
        _userManager = userManager;
        _dbContext = dbContext;
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
        var role = await _roleManager.FindByIdAsync(roleId.ToString());
        if (role == null) return "NotFound";

        //Chech if user has this role or not
        var users = await _userManager.GetUsersInRoleAsync(role.Name);

        //return exception 
        if (users != null && users.Any()) return "Used";

        //delete
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
        var response = new ManageUserRolesResult();
        var rolesList = new List<UserRoles>();
        //Roles
        var roles = await _roleManager.Roles.ToListAsync();
        response.UserId = user.Id;
        foreach (var role in roles)
        {
            var userrole = new UserRoles();
            userrole.Id = role.Id;
            userrole.Name = role.Name;
            if (await _userManager.IsInRoleAsync(user, role.Name))
            {
                userrole.HasRole = true;
            }
            else
            {
                userrole.HasRole = false;
            }
            rolesList.Add(userrole);
        }
        response.userRoles = rolesList;
        return response;
    }
    #endregion

    #region Update User Roles
    public async Task<string> UpdateUserRoles(UpdateUserRolesRequest request)
    {
        var transact = await _dbContext.Database.BeginTransactionAsync();
        try
        {
            //Get User
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null)
            {
                return "UserIsNull";
            }
            //get user Old Roles
            var userRoles = await _userManager.GetRolesAsync(user);
            //Delete OldRoles
            var removeResult = await _userManager.RemoveFromRolesAsync(user, userRoles);
            if (!removeResult.Succeeded)
                return "FailedToRemoveOldRoles";
            var selectedRoles = request.userRoles.Where(x => x.HasRole == true).Select(x => x.Name);

            //Add the Roles HasRole=True
            var addRolesresult = await _userManager.AddToRolesAsync(user, selectedRoles);
            if (!addRolesresult.Succeeded)
                return "FailedToAddNewRoles";
            await transact.CommitAsync();
            //return Result
            return "Success";
        }
        catch (Exception ex)
        {
            await transact.RollbackAsync();
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
