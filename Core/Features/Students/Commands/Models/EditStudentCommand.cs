﻿namespace Core.Features.Students.Commands.Models;

public class EditStudentCommand : IRequest<GenericBaseResponse<int>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string DepartmentId { get; set; }

}