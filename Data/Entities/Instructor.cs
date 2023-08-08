namespace Data.Entities;

public class Instructor : GeneralLocalizeEntity
{
    public Instructor()
    {
        Instructors = new HashSet<Instructor>();
        InstructorSubjects = new HashSet<InstructorSubject>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int InsId { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string Address { get; set; }
    public string Position { get; set; }
    public int SupervisorId { get; set; }
    public decimal? Salary { get; set; }
    public int DID { get; set; }

    [ForeignKey(nameof(DID))]
    //[InverseProperty("Instructors")]
    public Department? department { get; set; }

    //[InverseProperty("Instructor")]
    public Department? departmentManager { get; set; }



    [ForeignKey(nameof(SupervisorId))]
    //[InverseProperty("Instructors")]
    public Instructor? Supervisor { get; set; }

    //[InverseProperty("Supervisor")]
    public virtual ICollection<Instructor> Instructors { get; set; }

    //[InverseProperty("Instructor")]
    public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }

}
