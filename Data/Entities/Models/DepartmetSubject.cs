namespace Data.Entities.Models;
public class DepartmetSubject
{
    [Key]
    public int DID { get; set; }
    [Key]
    public int SubID { get; set; }

    [ForeignKey("DID")]
    [InverseProperty("DepartmentSubjects")]
    public virtual Department? Department { get; set; }

    [ForeignKey("SubID")]
    [InverseProperty("DepartmetsSubjects")]
    public virtual Subject? Subject { get; set; }
}
