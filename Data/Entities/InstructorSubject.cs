namespace Data.Entities;

public class InstructorSubject
{
    [Key]
    public int InsId { get; set; }
    [Key]
    public int SubId { get; set; }

    [ForeignKey(nameof(InsId))]
    //[InverseProperty("InstructorSubjects")]
    public Instructor? Instructor { get; set; }

    [ForeignKey(nameof(SubId))]
    //[InverseProperty("InstructorSubjects")]
    public Subjects? Subjects { get; set; }
}