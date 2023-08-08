namespace Infrastructure.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {

        }
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<Department> departments { get; set; }
        public DbSet<Student> students { get; set; }
        public DbSet<DepartmetSubject> departmetSubjects { get; set; }
        public DbSet<Subjects> subjects { get; set; }
        public DbSet<StudentSubject> studentSubjects { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DepartmetSubject>().HasKey(x => new { x.DID, x.SubID });
            modelBuilder.Entity<StudentSubject>().HasKey(x => new { x.StudID, x.SubID });
            modelBuilder.Entity<InstructorSubject>().HasKey(x => new { x.SubId, x.InsId });

            modelBuilder.Entity<Instructor>().HasOne(x => x.Supervisor).WithMany(x => x.Instructors).HasForeignKey(x => x.SupervisorId).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Department>().HasOne(x => x.Instructor).WithOne(x => x.departmentManager).HasForeignKey<Department>(x => x.InsManager).OnDelete(DeleteBehavior.Restrict);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }

}
