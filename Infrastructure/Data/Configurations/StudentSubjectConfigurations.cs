using Data.Entities.Models;

namespace Infrastructure.Data.Configurations;

public class StudentSubjectConfigurations : IEntityTypeConfiguration<StudentSubject>
{
    public void Configure(EntityTypeBuilder<StudentSubject> builder)
    {
        builder
           .HasKey(x => new { x.SubID, x.StudID });


        builder.HasOne(ds => ds.Student)
                 .WithMany(d => d.StudentSubject)
                 .HasForeignKey(ds => ds.StudID);

        builder.HasOne(ds => ds.Subject)
             .WithMany(d => d.StudentsSubjects)
             .HasForeignKey(ds => ds.SubID);

    }
}