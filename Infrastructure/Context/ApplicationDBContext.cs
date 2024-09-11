using EntityFrameworkCore.EncryptColumn.Interfaces;
using Data.Entities.Identities;
using Data.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using EntityFrameworkCore.EncryptColumn.Util;

namespace Infrastructure.Context;

public class ApplicationDBContext : IdentityDbContext<User, Role, int,
    IdentityUserClaim<int>, IdentityUserRole<int>, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    private readonly IEncryptionProvider _encryptionProvider;
    public ApplicationDBContext()
    {

    }
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
    {
        _encryptionProvider = new GenerateEncryptionProvider("11aae3c4845c4ae79b077aef0d6b8825");
    }

    public DbSet<User> User { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Department> departments { get; set; }
    public DbSet<Student> students { get; set; }
    public DbSet<DepartmetSubject> departmetSubjects { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<StudentSubject> studentSubjects { get; set; }
    public DbSet<UserRefreshToken> UserRefreshToken { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        //modelBuilder.UseEncryption(_encryptionProvider);
    }
}
