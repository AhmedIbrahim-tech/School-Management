namespace Infrastructure.Data.Configurations;

public class InstructorConfigurations : IEntityTypeConfiguration<Instructor>
{
    public void Configure(EntityTypeBuilder<Instructor> builder)
    {
        builder
           .HasOne(x => x.Supervisor)
           .WithMany(x => x.Instructors)
           .HasForeignKey(x => x.SupervisorId)
           .OnDelete(DeleteBehavior.Restrict);
    }
}
