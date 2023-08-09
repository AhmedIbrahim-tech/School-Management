namespace Data.Entities;


public class Student : GeneralLocalizeEntity
{
    public Student()
    {
        StudentSubjects = new HashSet<StudentSubject>();
    }
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int StudID { get; set; }
    [StringLength(200)]
    public string? NameAr { get; set; }
    [StringLength(200)]
    public string? NameEn { get; set; }

    [StringLength(500)]
    public string? Address { get; set; }
    [StringLength(500)]
    public string? Phone { get; set; }

    [Display(Name = "Department")]
    public int? DepartmentID { get; set; }

    [ForeignKey("DepartmentID")]
    //[InverseProperty("Student")]
    public virtual Department? Department { get; set; }
    [InverseProperty("Student")]
    public virtual ICollection<StudentSubject>? StudentSubjects { get; set; }
}
