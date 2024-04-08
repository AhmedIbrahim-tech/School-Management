using Data.Entities.Authentication;
using Data.Entities.Identities;
using Data.Entities.ThirdParty.MailService.Dtos;
using Data.Helpers;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Services.Interface;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace Services.Services;

public class AuthenticationService : IAuthenticationService
{
    #region Fields
    private readonly ApplicationDBContext _applicationDBContext;
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IEmailsService _emailsService;
    //private readonly IEncryptionProvider _encryptionProvider;
    //private readonly ConcurrentDictionary<string, RefreshToken> _userRefreshToken;
    #endregion 

    #region Constructor (s)
    public AuthenticationService(
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
        //_encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");
        //_userRefreshToken = new ConcurrentDictionary<string, RefreshToken>();
    }
    #endregion

    #region Handle Functions

    #region GET : JWT Token
   
    #region JWT Token
    public async Task<JwtAuthResult> GetJWTToken(User user)
    {
        var (jwtToken, accessToken) = await GenerateJWTToken(user);

        var refreshToken = GetRefreshToken(user.UserName);

        #region Add Refresh Token to DB
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

        #endregion

        var response = new JwtAuthResult()
        {
            AccessToken = accessToken,
            refreshToken = refreshToken
        };
        return response;
    } 
    #endregion

    #region 1) List of Claims
    public async Task<List<Claim>> GetClaims(User user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Name,user.UserName),
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

    #region 2). Generate JWT Token

    private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
    {
        var claims = await GetClaims(user);
        var jwtToken = new JwtSecurityToken(
            _jwtSettings.Issuer,
            _jwtSettings.Audience,
            claims,
            expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                SecurityAlgorithms.HmacSha256Signature));
        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return (jwtToken, accessToken);
    }

    #endregion

    #region 3). Get Refresh Token
    private RefreshToken GetRefreshToken(string username)
    {
        var refreshToken = new RefreshToken
        {
            ExpireAt = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
            UserName = username,
            TokenString = GenerateRefreshToken()
        };
        //_userRefreshToken.AddOrUpdate(refreshToken.TokenString, refreshToken, (s, t) => refreshToken);
        return refreshToken;
    }

    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        var randomNumberGenerate = RandomNumberGenerator.Create();
        randomNumberGenerate.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
    #endregion


    #endregion

    #region GET : Refresh Token
    public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
    {
        var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
        
        var refreshTokenResult = new RefreshToken()
        {
            UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value,
            TokenString = refreshToken,
            ExpireAt = (DateTime)expiryDate
        };
        var response = new JwtAuthResult()
        {
            AccessToken = newToken,
            refreshToken = refreshTokenResult,
        };

        return response;
    }
    #endregion

    #region Read JWT Token
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

    #region Check Validate Token

    #region 1). Validate Token Parameters

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

    #region 2). Validate Details
    public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
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
        var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
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

    #endregion

    #region Confirm Email
    public async Task<string> ConfirmEmail(int? userId, string? code)
    {
        if (userId == null || code == null)
            return "ErrorWhenConfirmEmail";
        var user = await _userManager.FindByIdAsync(userId.ToString());
        var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
        if (!confirmEmail.Succeeded)
            return "ErrorWhenConfirmEmail";
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
            //user
            var user = await _userManager.FindByEmailAsync(Email);
            //user not Exist => not found
            if (user == null)
                return "UserNotFound";
            //Generate Random Number

            //Random generator = new Random();
            //string randomNumber = generator.Next(0, 1000000).ToString("D6");
            var chars = "0123456789";
            var random = new Random();
            var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            //update User In Database Code
            user.Code = randomNumber;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
                return "ErrorInUpdateUser";
            var message = "Code To Reset Passsword : " + user.Code;
            //Send Code To  Email 
            var mail = new EmailDto()
            {
                MailTo = user.Email,
                Subject = "Reset Password",
                Body = message
            };
            await _emailsService.SendEmail(mail);
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
    public async Task<string> ConfirmResetPassword(string Code, string Email)
    {
        //Get User
        //user
        var user = await _userManager.FindByEmailAsync(Email);
        //user not Exist => not found
        if (user == null)
            return "UserNotFound";
        //Decrept Code From Database User Code
        var userCode = user.Code;
        //Equal With Code
        if (userCode == Code) return "Success";
        return "Failed";
    }

    #endregion

    #region 3). Reset Password
    public async Task<string> ResetPassword(string Email, string Password)
    {
        var trans = await _applicationDBContext.Database.BeginTransactionAsync();
        try
        {
            //Get User
            var user = await _userManager.FindByEmailAsync(Email);
            //user not Exist => not found
            if (user == null)
                return "UserNotFound";
            await _userManager.RemovePasswordAsync(user);
            if (!await _userManager.HasPasswordAsync(user))
            {
                await _userManager.AddPasswordAsync(user, Password);
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
