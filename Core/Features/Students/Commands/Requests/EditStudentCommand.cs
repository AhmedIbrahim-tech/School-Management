namespace Core.Features.Students.Commands.Requests;

public class EditStudentCommand : IRequest<GenericBaseResponse<int>>
{
    public int Id { get; set; }
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string DepartmentId { get; set; }

}
