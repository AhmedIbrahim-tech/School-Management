namespace Core.Features.Students.Queries.Results;

public record GetStudentPaginationListResponse(int StudID, string? Name, string? Address, string? Phone, string? DepartmentName);
