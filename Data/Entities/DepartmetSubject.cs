using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;
public class DepartmetSubject
{
    [Key]
    public int DeptSubID { get; set; }
    public int DID { get; set; }
    public int SubID { get; set; }

    [ForeignKey("DID")]
    public virtual Department Department { get; set; }

    [ForeignKey("SubID")]
    public virtual Subjects Subjects { get; set; }
}
