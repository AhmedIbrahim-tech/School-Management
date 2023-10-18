﻿using Microsoft.AspNetCore.Identity;

namespace Data.Entities.Identities;
public class User : IdentityUser
{
    //public User()
    //{
    //    UserRefreshTokens = new HashSet<UserRefreshToken>();
    //}
    public string FullName { get; set; }
    public string? Address { get; set; }
    public string? Country { get; set; }
    //[EncryptColumn]
    //public string? Code { get; set; }
    //[InverseProperty(nameof(UserRefreshToken.user))]
    //public virtual ICollection<UserRefreshToken> UserRefreshTokens { get; set; }
}