namespace Data.Entities;

public class Subject : GeneralLocalizeEntity
{
    public Subject()
    {
        StudentsSubjects = new HashSet<StudentSubject>();
        DepartmetsSubjects = new HashSet<DepartmetSubject>();
        Ins_Subjects = new HashSet<Ins_Subject>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SubID { get; set; }
    [StringLength(500)]
    public string? SubjectNameAr { get; set; }
    public string? SubjectNameEn { get; set; }
    public int? Period { get; set; }
    [InverseProperty("Subject")]
    public virtual ICollection<StudentSubject> StudentsSubjects { get; set; }
    [InverseProperty("Subject")]
    public virtual ICollection<DepartmetSubject> DepartmetsSubjects { get; set; }
    [InverseProperty("Subject")]
    public virtual ICollection<Ins_Subject> Ins_Subjects { get; set; }
}