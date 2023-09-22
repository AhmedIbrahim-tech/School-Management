namespace Data.Entities;


public class StudentSubject
{
    [Key]
    public int StudID { get; set; }
    [Key]
    public int SubID { get; set; }
    public decimal? grade { get; set; }

    [ForeignKey("StudID")]
    [InverseProperty("StudentSubject")]
    public virtual Student? Student { get; set; }

    [ForeignKey("SubID")]
    [InverseProperty("StudentsSubjects")]
    public virtual Subject? Subject { get; set; }

}
