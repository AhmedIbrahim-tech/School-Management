using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Services.Services;

#region Interface
public interface IAuthenticationServiceAsync
{
    Task<JwtAuthResult> GetJWTToken(User user);
    JwtSecurityToken ReadJWTToken(string accessToken);
    Task<(string, DateTime?)> ValidateAndCheckJWTDetails(JwtSecurityToken jwtToken, string AccessToken, string RefreshToken);
    Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken);
    Task<string> ValidateToken(string AccessToken);
    Task<string> ConfirmEmail(int? userId, string? code);
    public Task<string> SendResetPasswordCode(string Email);
    public Task<string> ConfirmResetPassword(string code, string Email);
    public Task<string> ResetPassword(string Email, string Password);
} 
#endregion

public class AuthenticationServiceAsync : IAuthenticationServiceAsync
{
    #region Fields
    private readonly ApplicationDBContext _applicationDBContext;
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IEmailsService _emailsService;
    private readonly IEncryptionProvider _encryptionProvider;
    //private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
    #endregion 

    #region Constructors
    public AuthenticationServiceAsync(
                                ApplicationDBContext applicationDBContext,
                                UserManager<User> userManager,
                                JwtSettings jwtSettings,
                                IRefreshTokenRepository refreshTokenRepository,
                                 IEmailsService emailsService
                                 )
    {
        _applicationDBContext = applicationDBContext;
        _userManager = userManager;
        _jwtSettings = jwtSettings;
        _refreshTokenRepository = refreshTokenRepository;
        _emailsService = emailsService;
        _encryptionProvider = new GenerateEncryptionProvider("11aae3c4845c4ae79b077aef0d6b8825");
        //_userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
    }
    #endregion

    #region Handle Functions

    #region GET : JWT Token Management

    #region 1). JWT Token Generation
    public async Task<JwtAuthResult> GetJWTToken(User user)
    {
        var (jwtToken, accessToken) = await GenerateJWTToken(user);

        var refreshToken = CreateRefreshToken(user.UserName);

        await AddRefreshTokenToDB(user, jwtToken, refreshToken, accessToken);

        var response = new JwtAuthResult()
        {
            AccessToken = accessToken,
            refreshToken = refreshToken
        };
        return response;
    }
    #endregion

    #region 2). List of Claims
    public async Task<List<Claim>> GetClaims(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,user.FullName),
            new Claim(ClaimTypes.NameIdentifier,user.UserName),
            new Claim(ClaimTypes.Email,user.Email),
            new Claim(nameof(UserClaimModel.PhoneNumber), user.PhoneNumber),
            new Claim(nameof(UserClaimModel.Id), user.Id.ToString())
        };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }
        var userClaims = await _userManager.GetClaimsAsync(user);
        claims.AddRange(userClaims);
        return claims;
    }
    #endregion

    #region 3). Generate JWT Token

    private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
    {
        var claims = await GetClaims(user);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

        var jwtToken = new JwtSecurityToken(
            issuer: _jwtSettings.Issuer,
            audience: _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
            signingCredentials: creds);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return (jwtToken, accessToken);
    }

    #endregion

    #region 4). Refresh Token Management

    #region 1). Generate Random Number For Refresh Token 

    private string GenerateRefreshToken()
    {
        //return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));

        var randomNumber = new byte[32];
        var randomNumberGenerate = RandomNumberGenerator.Create();
        randomNumberGenerate.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    #endregion

    #region 2). Create Refresh Token

    private RefreshToken CreateRefreshToken(string username)
    {
        var refreshToken = new RefreshToken
        {
            ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            UserName = username,
            TokenString = GenerateRefreshToken()
        };
        return refreshToken;
    }

    #endregion

    #region 3). Add Refresh Token to DB

    private async Task AddRefreshTokenToDB(User user, JwtSecurityToken jwtToken, RefreshToken refreshToken, string accessToken)
    {
        var userRefreshToken = new UserRefreshToken
        {
            AddedTime = DateTime.Now,
            ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            IsUsed = true,
            IsRevoked = false,
            JwtId = jwtToken.Id,
            RefreshToken = refreshToken.TokenString,
            Token = accessToken,
            UserId = user.Id
        };

        await _refreshTokenRepository.AddAsync(userRefreshToken);
    }

    #endregion

    #endregion

    #endregion

    #region GET : Refresh Token
    public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
    {
        var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);

        var UserNameClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName));

        if (UserNameClaim == null)
            throw new ArgumentNullException(nameof(UserNameClaim), "User not found.");

        var refreshTokenResult = new RefreshToken()
        {
            UserName = UserNameClaim.Value ?? "",
            TokenString = refreshToken,
            ExpireAt = Convert.ToDateTime(expiryDate), 
        };
        var response = new JwtAuthResult()
        {
            AccessToken = newToken,
            refreshToken = refreshTokenResult,
        };

        return response;
    }

    #region Old Way
    public async Task<JwtAuthResult> RefreshToken(string accesstoken, string refreshToken)
    {
        var readJwtToken = ReadJWTToken(accesstoken);
        var (result, expiryDateToken) = await ValidateAndCheckJWTDetails(readJwtToken, accesstoken, refreshToken);
        var userIdClaim = readJwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id));

        if (userIdClaim == null)
            throw new ArgumentNullException(nameof(userIdClaim), "User not found.");

        var userId = userIdClaim.Value ?? "";
        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new ArgumentNullException(nameof(user), "User not active or not found.");
        if (expiryDateToken == null)
            throw new ArgumentNullException(nameof(expiryDateToken), "Expiry date is required.");

        // Generate New Token
        var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);


        var refreshTokenResult = new RefreshToken()
        {
            UserName = user.UserName ?? "",
            TokenString = refreshToken,
            ExpireAt = (DateTime)expiryDateToken
        };

        var response = new JwtAuthResult()
        {
            AccessToken = newToken,
            refreshToken = refreshTokenResult,
        };
        return response;
    }

    #endregion

    #endregion

    #region JWT Token (Read / Validation)

    #region 1). Read JWT Token

    public JwtSecurityToken ReadJWTToken(string accessToken)
    {
        if (string.IsNullOrEmpty(accessToken))
        {
            throw new ArgumentNullException(nameof(accessToken));
        }
        var handler = new JwtSecurityTokenHandler();
        var response = handler.ReadJwtToken(accessToken);
        return response;
    }

    #endregion

    #region 2). Validate | Check JWT Token Details
    public async Task<(string, DateTime?)> ValidateAndCheckJWTDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
    {
        if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
        {
            return ("AlgorithmIsWrong", null);
        }
        if (jwtToken.ValidTo > DateTime.UtcNow)
        {
            return ("TokenIsNotExpired", null);
        }

        //Get User
        var userIdClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id));
        if (userIdClaim == null)
        {
            return ("UserIdNotFound", null);
        }
        var userId = userIdClaim.Value;

        var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                         .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                 x.RefreshToken == refreshToken &&
                                                                 x.UserId == int.Parse(userId));
        if (userRefreshToken == null)
        {
            return ("RefreshTokenIsNotFound", null);
        }

        if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
        {
            userRefreshToken.IsRevoked = true;
            userRefreshToken.IsUsed = false;
            await _refreshTokenRepository.UpdateAsync(userRefreshToken);
            return ("RefreshTokenIsExpired", null);
        }
        var expirydate = userRefreshToken.ExpiryDate;
        return (userId, expirydate);
    }
    #endregion

    #region 3). Validate Token Parameters

    public async Task<string> ValidateToken(string accessToken)
    {
        var handler = new JwtSecurityTokenHandler();
        var parameters = new TokenValidationParameters
        {
            ValidateIssuer = _jwtSettings.ValidateIssuer,
            ValidIssuers = new[] { _jwtSettings.Issuer },
            ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
            ValidAudience = _jwtSettings.Audience,
            ValidateAudience = _jwtSettings.ValidateAudience,
            ValidateLifetime = _jwtSettings.ValidateLifeTime,
        };
        try
        {
            var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

            if (validator == null)
            {
                return "InvalidToken";
            }

            return "NotExpired";
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
    #endregion

    #endregion

    #region Confirm Email
    public async Task<string> ConfirmEmail(int? userId, string? code)
    {
        if (userId == null || code == null) return "ErrorWhenConfirmEmail";

        var user = await _userManager.FindByIdAsync(userId.ToString());

        var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);

        if (!confirmEmail.Succeeded) return "ErrorWhenConfirmEmail";

        return "Success";
    }
    #endregion

    #region Reset Password

    #region 1). Send Reset Password Code
    public async Task<string> SendResetPasswordCode(string Email)
    {
        var trans = await _applicationDBContext.Database.BeginTransactionAsync();
        try
        {
            // Get Cruuent User
            var cruuentuser = await _userManager.FindByEmailAsync(Email);

            // User not Exist => not found
            if (cruuentuser == null) return "UserNotFound";

            //Generate Random Number
            // Random generator = new Random();
            // string randomNumber = generator.Next(0, 1000000).ToString("D6");
            var chars = "0123456789";
            var random = new Random();
            var randomNumber = new string(Enumerable.Repeat(chars, 6)
                                                                .Select(s => s[random.Next(s.Length)]).ToArray());

            //update User In Database Code
            cruuentuser.Code = randomNumber;
            var updateResult = await _userManager.UpdateAsync(cruuentuser);

            if (!updateResult.Succeeded) return "ErrorInUpdateUser";
            var message = "Code To Reset Passsword : " + cruuentuser.Code;

            var emailobj = new EmailDto()
            {
                Subject = "Reset Password",
                Body = message,
                MailTo = cruuentuser.Email ?? ""
            };
            //Send Code To  Email 
            await _emailsService.SendEmail(emailobj);

            await trans.CommitAsync();
            return "Success";
        }
        catch (Exception ex)
        {
            await trans.RollbackAsync();
            return "Failed";
        }
    }

    #endregion

    #region 2). Confirm Reset Password
    public async Task<string> ConfirmResetPassword(string code, string Email)
    {
        // Get Cruuent User
        var user = await _userManager.FindByEmailAsync(Email);

        //user not Exist => not found
        if (user == null) return "UserNotFound";

        //Decrept Code From Database User Code
        return user.Code == code ? "Success" : "Failed";
    }

    #endregion

    #region 3). Reset Password
    public async Task<string> ResetPassword(string Email, string Password)
    {
        var trans = await _applicationDBContext.Database.BeginTransactionAsync();
        try
        {
            // Get Cruuent User
            var cruuentuser = await _userManager.FindByEmailAsync(Email);

            //cruuent user not Exist => not found
            if (cruuentuser == null) return "UserNotFound";

            await _userManager.RemovePasswordAsync(cruuentuser);

            if (!await _userManager.HasPasswordAsync(cruuentuser))
            {
                await _userManager.AddPasswordAsync(cruuentuser, Password);
            }
            await trans.CommitAsync();
            return "Success";
        }
        catch (Exception ex)
        {
            await trans.RollbackAsync();
            return "Failed";
        }
    }
    #endregion

    #endregion

    #endregion
}
