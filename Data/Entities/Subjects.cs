namespace Data.Entities;

public class Subjects : GeneralLocalizeEntity
{
    public Subjects()
    {
        StudentsSubjects = new HashSet<StudentSubject>();
        DepartmetsSubjects = new HashSet<DepartmetSubject>();
        InstructorSubjects = new HashSet<InstructorSubject>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubID { get; set; }
    [StringLength(500)]
    public string SubjectNameAr { get; set; }
    [StringLength(500)]
    public string SubjectNameEn { get; set; }

    public int? Period { get; set; }
    //[InverseProperty("Subject")]
    public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }

    //[InverseProperty("Subject")]
    public virtual ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }

    //[InverseProperty("Subject")]
    public virtual ICollection<InstructorSubject> InstructorSubjects { get; set; }
}