namespace Data.Entities;


public class StudentSubject
{
    //public int StudSubID { get; set; }
    [Key]
    public int StudID { get; set; }
    [Key]
    public int SubID { get; set; }
    public decimal? Grade { get; set; }

    [ForeignKey("StudID")]
    //[InverseProperty("StudentSubjects")]
    public virtual Student? Student { get; set; }

    [ForeignKey("SubID")]
    //[InverseProperty("StudentsSubjects")]
    public virtual Subjects? Subject { get; set; }

}
