using System.ComponentModel.DataAnnotations;

namespace Core.Features.Students.Commands.Models;

public class AddStudentCommand : IRequest<GenericBaseResponse<string>>
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Address { get; set; }
    public string Phone { get; set; }
    public string DepartmentId { get; set; }
}
