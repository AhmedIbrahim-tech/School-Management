namespace Core.Features.Department.Queries.Results;
public class GetSingleDepartmentResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? ManagerName { get; set; }
    public PaginationResult<StudentResponse>? StudentsList { get; set; }
    public List<SubjectResponse>? SubjectsList { get; set; } = new List<SubjectResponse>();
    public List<InstructorResponse>? InstructorsList { get; set; } = new List<InstructorResponse>();
}

public class StudentResponse
{
    public StudentResponse(int id, string? name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
}


public class SubjectResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class InstructorResponse
{
    public int Id { get; set; }
    public string? Name { get; set; }
}