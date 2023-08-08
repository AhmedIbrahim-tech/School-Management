namespace Core.Features.Students.Commands.Models;

public class AddStudentCommand : IRequest<GenericBaseResponse<int>>
{
    public string NameAr { get; set; }
    public string NameEn { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string DepartmentId { get; set; }
}
