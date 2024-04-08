
using Data.Entities.Identities;
using Data.Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDBContext : IdentityDbContext<
        User, 
        Role, 
        int, 
        IdentityUserClaim<int>, 
        IdentityUserRole<int>, 
        IdentityUserLogin<int>, 
        IdentityRoleClaim<int>, 
        IdentityUserToken<int>
        >
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
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
        }
    }

}
